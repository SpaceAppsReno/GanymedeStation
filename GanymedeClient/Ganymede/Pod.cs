namespace Ganymede.GanymedeUI.Pod
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    

    public class PodModel : INotifyPropertyChanged, IPod
    {
        private string id;
        private int moisture;
        private double temperature;
        private int light;
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

        public int Moisture
        {
            get
            {
                return moisture;
            }

            set
            {
                if(moisture != value)
                {
                    moisture = value;
                    NotifyPropertyChanged("Moisture");
                }
            }
        }

        public double Temperature
        {
            get
            {
                return temperature;
            }

            set
            {
                if(temperature != value)
                {
                    temperature = value;
                    NotifyPropertyChanged("Temperature");
                }
            }
        }

        public int Light
        {
            get
            {
                return light;
            }

            set
            {
                if(light != value)
                {
                    light = value;
                    NotifyPropertyChanged("Light");
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