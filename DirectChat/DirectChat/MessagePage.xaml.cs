using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DirectChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        Guid id;
        MessageListModel mlm;
        struct Data
        {
            public string text { get; set; }
            public Guid id { get; set; }
        }
        public MessagePage(string name, Guid _id)
        {
            id = _id;
            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
            mlm = new MessageListModel(id);
            BindingContext = mlm;
            welcome.Text = name;
            if (App.target_ip == "not set") { 
                submit.IsEnabled = false;
                submit.BackgroundColor = Color.Red;
                
            }

            object lastItem = MessageList.ItemsSource.Cast<object>().LastOrDefault();

            MessageList.ScrollTo(lastItem, ScrollToPosition.End, false);
        }

        internal static void send_message(object data)
        {
            Data d = (Data)data;
            while (!App.can_send) { }
            Network.User.UserTransferable user_info = App.c.request_user(d.id, App.c.client);
            byte[] key = App.crypto.generate_shared_secret(App.crypto.private_key, user_info.key);
            byte[] enc_str = App.crypto.encrypt(d.text.PadRight(Network.Constants.MESSAGE_SIZE), key, user_info.iv);
            App.c.send(enc_str, d.id, App.user.Id);
            // Encrypt again using local IV for local copy
            byte[] local_enc_str = App.crypto.encrypt(d.text.PadRight(Network.Constants.MESSAGE_SIZE), key, App.crypto.AES.IV);
            App.c.dbh.add_message(Convert.ToBase64String(local_enc_str), d.id, true);
            MessageController messageController = new MessageController();
            messageController.new_message_msg();
        }
        private void submit_Clicked(object sender, EventArgs e)
        {
            if (entry.Text != "")
            {
                Data data = new Data();
                data.text = entry.Text;
                data.id = id;
                Thread t = new Thread(send_message);
                t.Start(data);
            }
            entry.Text = "";
        }

    }
}