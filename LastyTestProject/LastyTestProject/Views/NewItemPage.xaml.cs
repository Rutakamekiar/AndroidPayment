using System;
using System.Collections.Generic;
using System.ComponentModel;
using LastyTestProject.Models;
using LastyTestProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LastyTestProject.Views
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