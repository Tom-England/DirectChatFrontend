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
            Guid id = _id;
            List<Network.Message> msg_list = App.c.dbh.get_all_messages_from_user(id);
            for (int i = 0; i < msg_list.Count; i++)
            {
                byte[] user_key = App.c.dbh.get_user_key(id);
                byte[] key = App.crypto.generate_shared_secret(App.crypto.private_key, user_key);
                string text = App.crypto.decrypt(msg_list[i].text, key, App.crypto.AES.IV);
                MessageFake msg = new MessageFake(text, id.ToString());
                Messages.Add(msg);
            }
        }
    }
}
