using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Mobile.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
    }
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            await App.Current.MainPage.DisplayAlert("ACHTUNG!", message, "Ok");
        }
    }
}
