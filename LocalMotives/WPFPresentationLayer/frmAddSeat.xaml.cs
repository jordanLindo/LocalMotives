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
    /// Interaction logic for frmAddSeat.xaml
    /// </summary>
    public partial class frmAddSeat : Window
    {
        ISeatManager _seatManager;
        ITrainManager _trainManager;
        int _seatTypeID;
        int _seatListID;
        List<Train> _trains = new List<Train>();
        List<TrainCarVM> _trainCarVMs = new List<TrainCarVM>();
        List<SeatType> _seatTypes;




        public frmAddSeat(ISeatManager seatManager,ITrainManager trainManager)
        {
            InitializeComponent();
            _seatManager = seatManager;
            _trainManager = trainManager;
            fillCbos();

        }

        private void fillCbos()
        {
            try
            {
                _trains = _trainManager.GetAllTrainsActive();
                fillComboBoxCount();
                fillComboBoxTrain();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to retrieve list. " + ex.InnerException);
            }
        }

        private void fillComboBoxTrain()
        {

            for (int i = 0; i < _trains.Count; i++)
            {
                cboTrain.Items.Add(_trains.ElementAt(i).TrainName);
            }

        }

        private void fillComboBoxCount()
        {
            int max = 30;
            int min = 1;
            
            for (int i = min; i <= max; i++)
            {
                cboCount.Items.Add(i);
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (cboSeatType.SelectedItem != null && cboCount.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Add " + cboCount.SelectedItem +
                    " " + cboSeatType.SelectedItem,
                    "Is this correct?",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.None);
                if (result.ToString().Equals("OK"))
                {
                    _seatTypeID = _seatTypes.ElementAt(cboSeatType.SelectedIndex).SeatTypeID;
                    for (int i = 0; i < (int)cboCount.SelectedItem; i++)
                    {
                        try
                        {
                            Seat seat = new Seat()
                            {
                                SeatTypeID = _seatTypeID,
                                SeatListID = _seatListID
                            };
                            _seatManager.AddSeat(seat);
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Failed to add. "+ex.InnerException);
                        }
                    }
                    MessageBoxResult addMore = MessageBox.Show("Are there more seats to add?"
                        , "Continue"
                        , MessageBoxButton.YesNo);
                    if (addMore.ToString().Equals("No"))
                    {
                        Close();
                    }
                    else
                    {
                        cboCount.SelectedIndex = -1;
                        cboSeatType.SelectedIndex = -1;
                    }
                }
            }
        }

        private void CboTrain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboCar.IsEnabled = false;
            cboSeatType.IsEnabled = false;
            int trainID = _trains.ElementAt(cboTrain.SelectedIndex).TrainID;
            try
            {

                _trainCarVMs = _trainManager.GetAllTrainCarVMsByTrainID(trainID);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to get train cars. "+ex.InnerException);
            }
            cboCar.Items.Clear();
            if (_trainCarVMs.Count > 0)
            {
                for (int i = 0; i < _trainCarVMs.Count; i++)
                {
                    TrainCarVM trainCar = _trainCarVMs.ElementAt(i);
                    cboCar.Items.Add(trainCar.TrainCarID.ToString() + ", " + trainCar.TrainCarDescription);
                }
                cboCar.IsEnabled = true;
                cboCar.SelectedIndex = 0;
                int trainCarID = _trainCarVMs.ElementAt(cboCar.SelectedIndex).TrainCarID;
                _seatListID = _trainCarVMs.ElementAt(cboCar.SelectedIndex).SeatListID;
                try
                {
                    _seatTypes = _seatManager.GetAllSeatTypesByActive();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Failed to retrieve seat types. "+ex.InnerException);
                }
                cboSeatType.Items.Clear();
                for (int i = 0; i < _seatTypes.Count; i++)
                {
                    cboSeatType.Items.Add(_seatTypes.ElementAt(i).SeatDescription);
                }

                cboSeatType.IsEnabled = true;

            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CboSeatType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnCreate.IsEnabled = true;
        }
    }
}
