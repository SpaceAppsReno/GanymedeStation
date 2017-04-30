namespace Ganymede.GanymedeUI
{
    using GanymedeUI.BaseStations;
    using GanymedeUI.Pod;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        public ObservableCollection<IBaseStation> BaseStations { get; set; }

        private IBaseStation currentBaseStation = null;

        public FrontPage()
        {
            InitializeComponent();

            //var comm = CommunicationAccessor.GetCommScope();
            //if(comm == null)
            //{
            //    var exception = new Exception("No comm scope available. ");
            //    Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
            //    throw exception;
            //}

            //var baseStations = comm.GetAllBaseStations();
            //if(baseStations == null)
            //{
            //    var exception = new Exception("BaseStation's list was null. ");
            //    Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
            //    throw exception;
            //}

            //BaseStations = new ObservableCollection<IBaseStation>();
            //foreach(var baseStation in baseStations)
            //{
            //    BaseStations.Add(baseStation);
            //}

            //BaseStations.Add(new BaseStationModel()
            //{
            //    Id = "Fake1",
            //    FlowRate = 30,
            //    Voltage = 50,
            //    PodsConnectedToBase = new List<IPod>()
            //    {
            //        new PodModel()
            //        {
            //            Id = "FakePod1"
            //        },

            //        new PodModel()
            //        {
            //            Id = "FakePod2"
            //        },

            //        new PodModel()
            //        {
            //            Id = "FakePod3"
            //        }
            //    }
            //});

            //BaseStations.Add(new BaseStationModel()
            //{
            //    Id = "Fake2",
            //    FlowRate = 10,
            //    Voltage = 100,
            //    PodsConnectedToBase = new List<IPod>()
            //    {
            //        new PodModel()
            //        {
            //            Id = "FakePod1"
            //        },

            //        new PodModel()
            //        {
            //            Id = "FakePod2"
            //        },

            //        new PodModel()
            //        {
            //            Id = "FakePod3"
            //        }
            //    }
            //});

            BaseStations = new ObservableCollection<IBaseStation>();
            //this.DataContext = BaseStations;

            //Application.Current.Dispatcher.BeginInvoke(new Action(() => this.MyObservableCollection.Add(myItem)));

            BaseStationsList.ItemsSource = BaseStations;

            //WorkInBackground();
        }

        void WorkInBackground()
        {
            var results = new List<IBaseStation>();

            var comm = CommunicationAccessor.GetCommScope();
            if(comm == null)
            {
                var exception = new Exception("No comm scope available. ");
                Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
                throw exception;
            }

            var baseStations = comm.GetAllBaseStations();
            if(baseStations == null)
            {
                var exception = new Exception("BaseStation's list was null. ");
                Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
                throw exception;
            }

            // feed UI in packages no more than 100 items
            while(results.Count > 0)
            {
                Application.Current.MainWindow.Dispatcher.BeginInvoke(
                    new Action<List<IBaseStation>>(FeedUI),
                    DispatcherPriority.Background,
                    results.GetRange(0, Math.Min(results.Count, 100)));
                results.RemoveRange(0, Math.Min(results.Count, 100));
            }
        }
        void FeedUI(List<IBaseStation> items)
        {
            // items.Count must be small enough to keep UI looks alive
            foreach(var item in items)
            {
                BaseStations.Add(item);
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            Thread th = new Thread(() =>
            {
                var comm = CommunicationAccessor.GetCommScope();
                if(comm == null)
                {
                    var exception = new Exception("No comm scope available. ");
                    Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
                    throw exception;
                }

                var baseStations = comm.GetAllBaseStations();
                if(baseStations == null)
                {
                    var exception = new Exception("BaseStation's list was null. ");
                    Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
                    throw exception;
                }

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach(var station in baseStations)
                    {
                        BaseStations.Add(station);
                    }
                }));
            });

            th.Start();
        }


        private void onListBoxItemSelected(object sender,
            System.Windows.Controls.SelectionChangedEventArgs args)
        {
            currentBaseStation = BaseStationsList.SelectedItem as IBaseStation;
            if(currentBaseStation != null)
            {
                CurrentBaseStationFlowMeter.Text = currentBaseStation.FlowRate.ToString();
                CurrentBaseStationPodCount.Text = currentBaseStation.PodsConnectedToBase.Count.ToString();
                CurrentBaseStationVolatge.Text = currentBaseStation.Voltage.ToString();
            }
        }

        private void onViewBaseStatationClicked(object sender,
            RoutedEventArgs args)
        {
            var uri = new Uri("Pods.xaml", UriKind.Relative);
            if(currentBaseStation != null)
            {
                Application.Current.Properties["BaseStationId"] = currentBaseStation.Id;
                this.NavigationService.Navigate(uri);
            }
        }

        private void onUpdateBaseStationDataClicked(object sender,
            RoutedEventArgs args)
        {
            //Send data to server
        }
    }
}