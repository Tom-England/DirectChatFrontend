using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using Xamarin.Essentials;

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
            var data_item = (UserFake)e.Item;
            await Navigation.PushAsync(new MessagePage(data_item.Name, Guid.Parse(data_item.Id)));
        }

        private void AddMaster_Clicked(object sender, EventArgs e)
        {
            if (App.target_ip == "not set")
            {
                AddUser.BackgroundColor = Color.Gray;
                AddUser.IsEnabled = false;
            } else
            {
                AddUser.BackgroundColor = Color.FromHex("#00A7E1");
                AddUser.IsEnabled = true;
            }
            AddUser.IsVisible = !AddUser.IsVisible;
            AddServer.IsVisible = !AddServer.IsVisible;
        }

        private void Cancel_Input(object sender, EventArgs e)
        {
            UserField.IsVisible = false;
            ServerField.IsVisible = false;
            AddServer.IsVisible = false;
            AddUser.IsVisible = false;
        }

        private void AddUser_Clicked(object sender, EventArgs e)
        {
            UserField.IsVisible = true;
        }

        private void AddServer_Clicked(object sender, EventArgs e)
        {
            ServerField.IsVisible = true;
        }

        private void UserSubmit_Clicked(object sender, EventArgs e)
        {
            Guid guid;
            if (Guid.TryParse(GuidBox.Text, out guid))
            {
                while (!App.can_send) { }
                Network.User.UserTransferable uT = App.c.request_user(guid, App.c.client);
                App.c.dbh.add_user(uT.name, uT.id, uT.key);
            }
        }

        private void ServerSubmit_Clicked(object sender, EventArgs e)
        {
            App.target_ip = ServerEntry.Text;
            App.c.create_client(App.target_ip);
            App.c.handshake(App.c.client, App.user, App.crypto);
            Thread thread = new Thread(start: App.run_requests);
            thread.Start();
            AddServer.IsVisible = false;
            AddUser.IsVisible = false;
            ServerField.IsVisible = false;
        }

        private async void GetGuid_Clicked(object sender, EventArgs e)
        {
            Toast.IsVisible = true;
            await Toast.FadeTo(1, 1000);
            await Toast.FadeTo(0, 1000);
            await Clipboard.SetTextAsync(App.user.Id.ToString());
        }
    }
}
