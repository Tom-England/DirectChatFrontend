using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DirectChat
{
    public class UserListModel
    {
        public ObservableCollection<UserFake> Users { get; set; } = new ObservableCollection<UserFake>();
        public UserListModel()
        {
            populate();
            MessagingCenter.Subscribe<MessageController>(this, "user", (sender) =>
            {
                populate();
                // Do something whenever the "Hi" message is received
            });
        }


        public void populate()
        {
            Users.Clear();
            List<Network.User.UserTransferable> user_list = App.c.dbh.get_all_users();
            for (int i = 0; i < user_list.Count; i++)
            {
                Console.WriteLine(user_list[i].name);
                Console.WriteLine(user_list[i].id);
                UserFake user = new UserFake(user_list[i].name, user_list[i].id);
                Users.Add(user);
            }
        }
    }
}
