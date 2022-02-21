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


namespace AirportClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxICAO.Text=="" || textBoxIATA.Text=="" || textBoxMinima.Text=="")
            {
                statusBar.Foreground = Brushes.Red;
                statusBar.Text="Please fill all box!";
                return;
            }
            AirportService.AirportServiceClient client = new AirportService.AirportServiceClient();
            AirportService.Airport airport = new AirportService.Airport
            {
                AirportICAOCode =textBoxICAO.Text,
                AirportIATACode=textBoxIATA.Text,
                Minima=Convert.ToInt32(textBoxMinima.Text)
            };
            client.SaveAirport(airport);
            textBoxICAO.Text = "";
            textBoxIATA.Text = "";
            textBoxMinima.Text = "";
            statusBar.Foreground = Brushes.Green;
            statusBar.Text = "Airport saved/updated...";
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxICAO.Text.Length != 4 )
            {
                statusBar.Foreground = Brushes.Red;
                statusBar.Text = "Please fill ICAO code!";
                return;
            }
            AirportService.AirportServiceClient client = new AirportService.AirportServiceClient();
            AirportService.Airport airport= client.GetAirport(textBoxICAO.Text);
            if (airport.AirportICAOCode!=null)
            {
                textBoxIATA.Text = airport.AirportIATACode;
                textBoxMinima.Text = airport.Minima.ToString();
                statusBar.Foreground = Brushes.Green;
                statusBar.Text = "Airport loaded...";
            }
            else
            {
                statusBar.Foreground = Brushes.Red;
                statusBar.Text = "Airport not found...";
            }
            
        }
    }
}
