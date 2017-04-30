namespace Ganymede.GanymedeUI
{
    using Ganymede.Communications;
    using Logger;
    using System;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private CommunicationScope comm;

        public MainWindow()
        {
            InitializeComponent();
            var status = Connect();
            if(status)
            {
                DoStuff();
            }
            else
            {
                ShowNoConnectionPage();
            }
            
        }

        bool Connect()
        {
            var success = false;
            try
            {
                CommunicationAccessor.Initialize("http://ec2-54-186-39-241.us-west-2.compute.amazonaws.com:3000/api");
                success = true;
            }
            catch(Exception e)
            {
                success = false;
                Logger.Write("MainWindow.xml.cs:Connect failed?? WTF!!!!. \n" + e.InnerException.StackTrace);
            }

            return success;
        }

        void DoStuff()
        {
            var uri = new Uri("FrontPage.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }

        void ShowNoConnectionPage()
        {
            var uri = new Uri("NoConnection.xaml", UriKind.Relative);
            this.NavigationService.Navigate(uri);
        }
    }
}