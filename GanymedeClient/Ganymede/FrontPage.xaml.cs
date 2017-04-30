namespace Ganymede.GanymedeUI
{
    using GanymedeUI.BaseStations;
    using GanymedeUI.Pod;
    using Interfaces;
    using System;
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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for FrontPage.xaml
    /// </summary>
    public partial class FrontPage : Page
    {
        private ObservableCollection<IBaseStation> BaseStations { get; set; }

        private IBaseStation currentBaseStation = null;

        public FrontPage()
        {
            InitializeComponent();

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

            BaseStations = new ObservableCollection<IBaseStation>();
            foreach(var baseStation in baseStations)
            {
                BaseStations.Add(baseStation);
            }

            BaseStations.Add(new BaseStationModel()
            {
                Id = "Fake1",
                FlowRate = 30,
                Voltage = 50,
                PodsConnectedToBase = new List<IPod>()
                {
                    new PodModel()
                    {
                        Id = "FakePod1"
                    },

                    new PodModel()
                    {
                        Id = "FakePod2"
                    },

                    new PodModel()
                    {
                        Id = "FakePod3"
                    }
                }
            });

            BaseStations.Add(new BaseStationModel()
            {
                Id = "Fake2",
                FlowRate = 10,
                Voltage = 100,
                PodsConnectedToBase = new List<IPod>()
                {
                    new PodModel()
                    {
                        Id = "FakePod1"
                    },

                    new PodModel()
                    {
                        Id = "FakePod2"
                    },

                    new PodModel()
                    {
                        Id = "FakePod3"
                    }
                }
            });

            BaseStationsList.ItemsSource = BaseStations;
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