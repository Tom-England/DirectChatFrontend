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
            Users.Add(new UserFake("Tom")) ;
            Users.Add(new UserFake("Mark"));
            Users.Add(new UserFake("Artur"));
            Users.Add(new UserFake("Tracy"));
            Users.Add(new UserFake("Joel"));
            Users.Add(new UserFake("Nicki"));
            Users.Add(new UserFake("Harley"));
            Users.Add(new UserFake("Jarmo"));
            Users.Add(new UserFake("Ilysa"));
            Users.Add(new UserFake("Varjhon"));
            Users.Add(new UserFake("Toso"));
        }
    }
}
