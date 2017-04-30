namespace Ganymede.GanymedeUI
{
    using GanymedeUI.BaseStations;
    using GanymedeUI.Pod;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Threading;
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
    /// Interaction logic for Pods.xaml
    /// </summary>
    public partial class Pods : Page
    {
        private ObservableCollection<IPod> Podies { get; set; }

        private PodModel currentSelectedPod = null;

        public Pods()
        {
            InitializeComponent();
            PodList.ItemsSource = Podies;
        }

        System.Timers.Timer timer;

        protected override void OnInitialized(EventArgs args)
        {
            base.OnInitialized(args);
            var baseStation = Application.Current.Properties["BaseStationId"] as string;
            Podies = new ObservableCollection<IPod>();

            timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += (s, e) =>
            {
                var comm = CommunicationAccessor.GetCommScope();
                var baseS = comm.GetABaseStation(baseStation);
                if(baseS == null)
                {
                    var exception = new Exception("BaseStation's list was null. ");
                    Logger.Logger.Write(exception.Message + "\n " + exception.StackTrace);
                    throw exception;
                }

                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    Podies.Clear();
                    foreach(var pod in baseS.PodsConnectedToBase)
                    {
                        Podies.Add(new PodModel()
                        {
                            Id = pod.Id,
                            Light = pod.Light,
                            Moisture = pod.Moisture,
                            Temperature = pod.Temperature,
                            Voltage = (pod.Voltage / 1000)
                        });
                    }
                }));
            };

            timer.AutoReset = true;
            timer.Start();
        }

        //protected override void OnInitialized(EventArgs args)
        //{
        //    base.OnInitialized(args);
        //    var baseStation =  Application.Current.Properties["BaseStation"] as IBaseStation;
        //    Podies = new ObservableCollection<IPod>();

        //    foreach(var pod in baseStation.PodsConnectedToBase)
        //    {
        //        Podies.Add(new PodModel()
        //        {
        //            Id = pod.Id,
        //            Light = pod.Light,
        //            Moisture = pod.Moisture,
        //            Temperature = pod.Temperature,
        //            Voltage = (pod.Voltage/1000)
        //        });
        //    }
        //}

        private void onTextSelectionChanged(object sender, RoutedEventArgs args)
        {

        }

        private void onPodUpdateClicked(object sender, RoutedEventArgs args)
        {
            if(currentSelectedPod != null)
            {
                //send updates to server for 
            }
        }

        private void onPodSelected(object sender,
            System.Windows.Controls.SelectionChangedEventArgs args)
        {
            currentSelectedPod = PodList.SelectedItem as PodModel;
        }
    }
}