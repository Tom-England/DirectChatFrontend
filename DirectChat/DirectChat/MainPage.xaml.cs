using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DirectChat
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            BindingContext = new UserListModel();

        }

        async private void ListView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            var data_item = e.Item as UserFake;
            await Navigation.PushAsync(new DirectChat.MessagePage(data_item.Name));
        }
    }
}
