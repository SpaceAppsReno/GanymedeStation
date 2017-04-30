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
    /// Interaction logic for Pods.xaml
    /// </summary>
    public partial class Pods : Page
    {
        private ObservableCollection<IPod> Podies { get; set; }

        private PodModel currentSelectedPod = null;

        private string currentBaseStationId;

        public Pods()
        {
            InitializeComponent();

            Podies = new ObservableCollection<IPod>();
            Podies.Add(new PodModel()
            {
                Id = "Pod1",
                Light = 3,
                Moisture = 4,
                Temperature = 9.0,
                Voltage = 909.0
            });

            PodList.ItemsSource = Podies;
        }

        protected override void OnInitialized(EventArgs args)
        {
            base.OnInitialized(args);
            currentBaseStationId = Application.Current.Properties["BaseStationId"].ToString();
        }

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