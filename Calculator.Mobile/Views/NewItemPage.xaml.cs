using Calculator.Mobile.Models;
using Calculator.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}