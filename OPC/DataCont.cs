using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Configurations;

namespace OPC
{
    class DataCont : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _z;
        private List<double> _xChart;

        public int x
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        public int y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }
        public int z
        {
            get { return _z; }
            set
            {
                if (_z != value)
                {
                    _z = value;
                    OnPropertyChanged();
                }
            }
        }

        public void AddToxChart(double x)
        {
            _xChart.Add(x);
            OnPropertyChanged();
        }

        public DataCont()
        {
            _xChart = new List<double>()
            {
                0
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
