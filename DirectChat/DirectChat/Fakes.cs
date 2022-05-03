using System;
using System.Collections.Generic;
using System.Text;

namespace DirectChat
{
    public class UserFake
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public UserFake(string _name, Guid _id)
        {
            Name = _name;
            Id = _id.ToString();
        }
    }

    public class MessageFake
    {
        public string Text { get; set; }
        public string Id { get; set; }
        public MessageFake(string _text, string _id)
        {
            Text = _text;
            Id = _id.ToString();
        }
    }
}
