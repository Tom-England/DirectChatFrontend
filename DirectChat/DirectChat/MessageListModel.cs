using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
namespace DirectChat
{
    public class MessageListModel
    {
        public ObservableCollection<MessageFake> Messages { get; set; } = new ObservableCollection<MessageFake>();
        public MessageListModel(Guid _id)
        {
            update_list(_id);
        }
        public void update_list(Guid _id)
        {
            Guid id = _id;
            List<Network.Message> msg_list = App.c.dbh.get_all_messages_user(id);
            for (int i = 0; i < msg_list.Count; i++)
            {
                byte[] user_key = App.c.dbh.get_user_key(id);
                byte[] key = App.crypto.generate_shared_secret(App.crypto.private_key, user_key);
                string text = App.crypto.decrypt(msg_list[i].text, key, App.crypto.AES.IV);
                MessageFake msg = new MessageFake(text, id.ToString());
                if (msg_list[i].sent)
                {
                    msg.Background = "#495159";
                    msg.Margin = new Xamarin.Forms.Thickness(100, 5, 15, 5);
                }
                else
                {
                    msg.Background = "#FF9B42";
                    msg.Margin = new Xamarin.Forms.Thickness(15, 5, 100, 5);
                }
                Messages.Add(msg);
            }
        }
    }
}
