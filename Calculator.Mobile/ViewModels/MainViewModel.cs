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
        private List<string> availableOperations = new List<string> { "+","-","/","*" };
        private bool isLastSignAnOperation;
        private DataTable dataTable = new DataTable();
        public MainViewModel()
        {
            Title = "Calculator";
            ScreenVal = "0";
            AddNumberCommand = new Command(AddNumber);
            AddOperationCommand = new Command(AddOperation, CanAddOperation);
            ClearScreenCommand = new Command(ClearScreen);
            GetResultCommand = new Command(GetResult, CanGetResult);
        }



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
        public string ScreenVal
        {
            get { return screenVal; }
            set { SetProperty(ref screenVal, value); }
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

            ScreenVal += operation;

            IsLastSignAnOperation=true;
        }

        private void ClearScreen(object obj)
        {
            ScreenVal = "0";
        }

        private void GetResult(object obj)
        {
            var result = Math.Round(Convert.ToDouble(dataTable.Compute(ScreenVal.Replace(",", "."), "")), 2);
            ScreenVal = result.ToString();
        }

        private bool CanGetResult(object arg) => !IsLastSignAnOperation;
        private bool CanAddOperation(object arg) => !IsLastSignAnOperation;
    }
}