using System.ComponentModel;
using LastyTestProject.ViewModels;
using Xamarin.Forms;

namespace LastyTestProject.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}