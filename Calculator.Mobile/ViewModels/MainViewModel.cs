using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Calculator.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string screenVal;
        private string lastCalc;
        private List<string> availableOperations = new List<string> { "+","-","/","*","^", "√" };
        private bool isLastSignAnOperation;
        private DataTable dataTable = new DataTable();
        private readonly Services.IMessageService messageService;
        public MainViewModel()
        {
            Title = "Calculator";
            ScreenVal = "0";
            AddNumberCommand = new Command(AddNumber);
            AddOperationCommand = new Command(AddOperation);
            ClearScreenCommand = new Command(ClearScreen);
            GetResultCommand = new Command(GetResult, CanGetResult);
            BackspaceCommand = new Command(BtnBack);
            messageService = DependencyService.Get<Services.IMessageService>();
        }


        public Command BackspaceCommand { get; }
        public Command AddNumberCommand { get; }
        public Command AddOperationCommand { get; }
        public Command ClearScreenCommand { get; }
        public Command GetResultCommand { get; }

        public bool IsLastSignAnOperation
        {
            get { return isLastSignAnOperation; }  
            set 
            { 
                SetProperty(ref isLastSignAnOperation, value);
                GetResultCommand.ChangeCanExecute();
                AddOperationCommand.ChangeCanExecute();
            }
        }

        public string LastCalc
        {
            get { return lastCalc; }
            set { SetProperty(ref lastCalc, value); }
        }
        public string ScreenVal
        {
            get { return screenVal; }
            set { SetProperty(ref screenVal, value); }
        }

        private void BtnBack()
        {
            if (ScreenVal.Length < 2)
            {
                ScreenVal = "0";
            }
            else if (ScreenVal != "0")
            {
                ScreenVal = ScreenVal.Remove(ScreenVal.Length - 1);
            }
        }

        private void AddNumber(object obj)
        {
            var number = obj as string;

            if (ScreenVal == "0" && number != ",")
                ScreenVal = string.Empty;
            else if (number == "," && availableOperations.Contains(ScreenVal.Substring(ScreenVal.Length - 1)))
            {
                number = "0,";
            }
            ScreenVal += number;

            IsLastSignAnOperation = false;
        }

        private void AddOperation(object obj)
        {
            var operation = obj as string;
            
            if(IsLastSignAnOperation)
                ScreenVal = ScreenVal.Remove(ScreenVal.Length - 1) + operation;
            else
                ScreenVal += operation;

            if(!ScreenVal.Contains("^2"))
                IsLastSignAnOperation=true;
            if (ScreenVal.EndsWith("0√"))
                ScreenVal = "√";
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
        }

        private void GetResult(object obj)
        {
            if(ScreenVal.EndsWith("/0"))
            {
                messageService.ShowAsync("You cannot divide by 0!!!");
                ScreenVal = "0";
            }
            else
            {
                var result = Math.Round(Services.ExpressionResolver.Compute(ScreenVal),9);
                LastCalc = ScreenVal + "=" + result.ToString();
                ScreenVal = result.ToString();
            }
        }

        private bool CanGetResult(object arg) => !IsLastSignAnOperation;
    }
}