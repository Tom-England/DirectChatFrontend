using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DirectChat
{
    class MessageController
    {
        public void new_user_msg()
        {
            MessagingCenter.Send(this, "user");
        }

        public void new_message_msg()
        {
            MessagingCenter.Send(this, "msg");
        }
    }
}
