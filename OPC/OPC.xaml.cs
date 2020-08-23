using LiveCharts;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using TitaniumAS.Opc.Client;
using TitaniumAS.Opc.Client.Common;
using TitaniumAS.Opc.Client.Da;

namespace OPC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool _continue = true;
        private static Thread opcThread;
        private DataCont dataCont = new DataCont();

        public MainWindow()
        {
            InitializeComponent();
            Bootstrap.Initialize();

            DataContext = dataCont;
        }

        private void OpcConnect()
        {
            Uri url = UrlBuilder.Build("Matrikon.OPC.Simulation.1");
            using (var server = new OpcDaServer(url))
            {
                server.Connect();

                OpcDaGroup group = server.AddGroup("MyGroup");
                group.IsActive = true;

                var definition1 = new OpcDaItemDefinition
                {
                    ItemId = "Saw-toothed Waves.Int1",
                    IsActive = true
                };
                var definition2 = new OpcDaItemDefinition
                {
                    ItemId = "Saw-toothed Waves.Int2",
                    IsActive = true
                };
                var definition3 = new OpcDaItemDefinition
                {
                    ItemId = "Saw-toothed Waves.Int3",
                    IsActive = true
                };

                OpcDaItemDefinition[] definitions = { definition1, definition2, definition3 };
                OpcDaItemResult[] results = group.AddItems(definitions);

                foreach (OpcDaItemResult result in results)
                {
                    if (result.Error.Failed)
                    {
                        //"Error adding items: {0}", result.Error
                    }
                }

                while (_continue)
                {
                    try
                    {
                        OpcDaItemValue[] values = group.Read(group.Items, OpcDaDataSource.Device);
                        Dispatcher.Invoke((Action)(() =>
                        {
                            dataCont.x = Convert.ToInt32(values[0].Value);
                            dataCont.y = Convert.ToInt32(values[1].Value);
                            dataCont.z = Convert.ToInt32(values[2].Value);
                            ChartUpdate((double)dataCont.x, (double)dataCont.y, (double)dataCont.z);
                        }));
                        Thread.Sleep(80);
                    }
                    catch (NotSupportedException en)
                    {
                        _continue = false;
                        MessageBox.Show(en.Message);
                    }
                }
                server.Disconnect();
            }
        }

        private void ChartUpdate(double x, double y, double z)
        {
            dataCont.AddToxChart(x);
            dataCont.AddToyChart(y);
            dataCont.AddTozChart(z);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (opcThread == null || !opcThread.IsAlive)
            {
                _continue = true;
                opcThread = new Thread(OpcConnect);
                opcThread.Start();
                statusLbl.Content = "Nawiązano połączenie";
                StartBtn.Content = "Zamknij";
            }
            else if (opcThread.IsAlive)
            {
                _continue = false;
                opcThread.Join();
                statusLbl.Content = "Zamknięto połączenie";
                StartBtn.Content = "Start";
            }
        }

        private void OpenGLBtn_Click(object sender, RoutedEventArgs e)
        {
            using (OpenGLWindow open = new OpenGLWindow(600, 500, "Test", dataCont))
            {
                open.Run(60.0);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (opcThread == null || !opcThread.IsAlive) { }
            else if (opcThread.IsAlive)
            {
                _continue = false;
                opcThread.Join();
                statusLbl.Content = "Zamknięto połączenie";
                this.Close();
            }
        }
    }
}