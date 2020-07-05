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
    /// Interaction logic for frmStationList.xaml
    /// </summary>
    public partial class frmStationList : Window
    {
        IStationManager _stationManager;

        public frmStationList(IStationManager stationManager)
        {

            _stationManager = stationManager;
            InitializeComponent();
            setDgStationList();
        }

        private void setDgStationList(bool active = true)
        {
            try
            {
                dgStationList.ItemsSource = _stationManager.GetAllStationsByActive(active);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error list not found. "+ex.InnerException);
            }
        }

        private void CbActive_Unchecked(object sender, RoutedEventArgs e)
        {
            setDgStationList(false);
        }

        private void CbActive_Checked(object sender, RoutedEventArgs e)
        {
            setDgStationList();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
