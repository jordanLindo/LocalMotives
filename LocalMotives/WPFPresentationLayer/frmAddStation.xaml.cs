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
    /// Interaction logic for frmAddStation.xaml
    /// </summary>
    public partial class frmAddStation : Window
    {
        IStationManager _stationManager;

        public frmAddStation(IStationManager stationManager)
        {
            InitializeComponent();
            _stationManager = stationManager;
            cboStationType.ItemsSource = _stationManager.GetStationTypes();
            
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(!txtStationName.Text.Equals(""))
            {
                if (_stationManager.Validate(txtStationName.Text))
                {
                    try
                    {
                        int stationID =_stationManager.AddStation(new Station()
                        {
                            StationName = txtStationName.Text,
                            StationType = cboStationType.SelectedItem.ToString()
                        });
                        if (stationID != 0)
                        {
                            MessageBox.Show("Station added.");
                            Close();
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Failed to add. "+ex.InnerException);
                    }
                }
            }
        }

        private void CboStationType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSubmit.IsEnabled = true;
        }
    }
}
