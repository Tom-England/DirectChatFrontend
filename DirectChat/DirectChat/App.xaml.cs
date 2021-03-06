using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;

namespace DirectChat
{
    public partial class App : Application
    {
        public static Network.User user;
        public static Network.Client c = new Network.Client();
        public static cryptography.CryptoHelper crypto = new cryptography.CryptoHelper();
        public static bool can_send = true;
        public static bool new_user = false;
        public static Thread thread = new Thread(start: App.run_requests);
        public static string target_ip { get; set; } = "not set";
        public App()
        {
            user = new Network.User("Tom");
            c.setup_client();
            c.setup_id(user, crypto);
            MainPage = new NavigationPage(new MainPage());
            InitializeComponent();
        }

        void run_requests()
        {
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            c.setup_id(user, crypto);
            if (App.target_ip != "not set")
            {
                Console.WriteLine("Resume");
                if (!thread.IsAlive)
                {
                    Console.WriteLine("Starting");
                    thread.Start();
                }
            }
            Console.WriteLine("Not set");
        }
        internal static void run_requests(object obj)
        {
            MessageController messageController = new MessageController();  
            while (true)
            {
                try
                {
                    while (!can_send) { }
                    can_send = false;
                    (bool, bool) result = c.check_messages(c, user.Id);
                    if (result.Item1)
                    {
                        // User was added
                        messageController.new_user_msg();
                    }
                    if (result.Item2)
                    {
                        // Message was received
                        messageController.new_message_msg();
                    }
                    can_send = true;
                    Thread.Sleep(1000);
                } catch (System.IO.IOException e)
                {
                    App.target_ip = "not set";
                    break;
                }
                
            }
        }
    }
}
