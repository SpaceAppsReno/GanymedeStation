using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganymede.Interfaces;

namespace Ganymede.GanymedeUI.BaseStations
{
    using Pod;
    using System.ComponentModel;

    public class BaseStationModel : INotifyPropertyChanged, IBaseStation
    {
        private string id;
        private IList<IPod> pods = new List<IPod>();
        private IList<string> valves = new List<string>();
        private double flowRate;
        private double voltage;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if(id != value)
                {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        public IList<IPod> PodsConnectedToBase
        {
            get
            {
                return pods;
            }

            set
            {
                pods = value;
                NotifyPropertyChanged("PodsConnectedToBase");
            }
        }


        public double FlowRate
        {
            get
            {
                return flowRate;
            }
            set
            {
                if(flowRate != value)
                {
                    flowRate = value;
                    NotifyPropertyChanged("flowRate");
                }
            }
        }

        public double Voltage
        {
            get
            {
                return voltage;
            }

            set
            {
                if(voltage != value)
                {
                    voltage = value;
                    NotifyPropertyChanged("Voltage");
                }
            }
        }

        public IList<string> ValvesConnectedToBase
        {
            get
            {
                return valves;
            }
            set
            {
                valves = value;
                NotifyPropertyChanged("ValvesConnectedToBase");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}