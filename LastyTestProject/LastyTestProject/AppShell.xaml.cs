using System;
using System.Collections.Generic;
using LastyTestProject.ViewModels;
using LastyTestProject.Views;
using Xamarin.Forms;

namespace LastyTestProject
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
