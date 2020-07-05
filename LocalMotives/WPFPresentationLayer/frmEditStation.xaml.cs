using DataObject;
using LogicLayer;
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
using System.Windows.Shapes;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmEditStation.xaml
    /// </summary>
    public partial class frmEditStation : Window
    {
        IStationManager _stationManager;
        Station _station;

        public frmEditStation(IStationManager stationManager,Station station)
        {
            InitializeComponent();
            _stationManager = stationManager;
            _station = station;
            lblStation.Content = station.StationID;
            lblName.Content = station.StationName;
            lblStationType.Content = station.StationType;
            cboStationType.ItemsSource = _stationManager.GetStationTypes();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string stationName;
            string stationType;

            if (txtStationName.Text.Equals(""))
            {
                stationName = lblName.Content.ToString();
                if (cboStationType.SelectedIndex.Equals(-1))
                {
                    stationType = lblStationType.Content.ToString();
                }
                else
                {
                    stationType = cboStationType.SelectedItem.ToString();
                }
                try
                {
                    _stationManager.EditStation(_station, new Station()
                    {
                        StationName = stationName,
                        StationType = stationType
                    });
                    MessageBox.Show("Update successful.");
                    Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Update failed. "+ex.InnerException);
                }
            }
            else
            {
                if (_stationManager.Validate(txtStationName.Text))
                {
                    stationName = txtStationName.Text;
                    if (cboStationType.SelectedIndex.Equals(-1))
                    {
                        stationType = lblStationType.Content.ToString();
                    }
                    else
                    {
                        stationType = cboStationType.SelectedItem.ToString();
                    }
                    _stationManager.EditStation(_station, new Station()
                    {
                        StationName = stationName,
                        StationType = stationType
                    });
                    MessageBox.Show("Update successful.");
                    Close();
                }
                else
                {
                    txtStationName.Text = "";
                    MessageBox.Show("That was an invalid Station name","Error",MessageBoxButton.OK);
                }
            }
            


        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
