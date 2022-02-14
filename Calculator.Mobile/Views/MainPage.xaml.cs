using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator.Mobile.Views
{
    public partial class MainPage : ContentPage
    {
        private double width;
        private double height;
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width < height)
                {
                    mainGrid.RowDefinitions.Clear();
                    mainGrid.ColumnDefinitions.Clear();
                    mainGrid.RowDefinitions.Add(new RowDefinition());
                    mainGrid.RowDefinitions.Add(new RowDefinition());
                    mainGrid.Children.Add(resultsGrid, 0, 0);
                    mainGrid.Children.Add(controlGrid, 0, 1);

                }
                else
                {
                    mainGrid.RowDefinitions.Clear();
                    mainGrid.ColumnDefinitions.Clear();
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    mainGrid.Children.Add(resultsGrid, 0, 0);
                    mainGrid.Children.Add(controlGrid, 1, 0);
                }
            }
        }
    }
}