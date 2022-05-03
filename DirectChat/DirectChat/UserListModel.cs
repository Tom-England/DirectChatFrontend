using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
namespace DirectChat
{
    public class UserListModel
    {
        public ObservableCollection<UserFake> Users { get; set; } = new ObservableCollection<UserFake>();
        public UserListModel()
        {
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
