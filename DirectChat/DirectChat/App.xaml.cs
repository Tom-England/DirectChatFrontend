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

        public static string target_ip { get; set; }
        public App()
        {
            target_ip = "not set";
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
        }

        internal static void run_requests(object obj)
        {
            while (true)
            {
                try
                {
                    can_send = false;
                    c.check_messages(c, user.Id);
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
