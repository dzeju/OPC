using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace OPC
{
    public class DataCont : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _z;
        private ChartValues<double> _xChart { get; set; }
        private ChartValues<double> _yChart { get; set; }
        private ChartValues<double> _zChart { get; set; }

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

        public ChartValues<double> xChart
        {
            get { return _xChart; }
        }
        public ChartValues<double> yChart
        {
            get { return _yChart; }
        }
        public ChartValues<double> zChart
        {
            get { return _zChart; }
        }

        public void AddToxChart(double x)
        {
            _xChart.Add(x);
            if (_xChart.Count > 60)
                _xChart.RemoveAt(0);
        }
        public void AddToyChart(double y)
        {
            _yChart.Add(y);
            if (_yChart.Count > 60)
                _yChart.RemoveAt(0);
        }
        public void AddTozChart(double z)
        {
            _zChart.Add(z);
            if (_zChart.Count > 60)
                _zChart.RemoveAt(0);
        }

        public DataCont()
        {
            _xChart = new ChartValues<double>() { 0 };
            _yChart = new ChartValues<double>() { 0 };
            _zChart = new ChartValues<double>() { 0 };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
