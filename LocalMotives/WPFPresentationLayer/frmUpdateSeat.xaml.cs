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
    /// Interaction logic for frmUpdateSeat.xaml
    /// </summary>
    public partial class frmUpdateSeat : Window
    {
        private ISeatManager _seatManager;
        private SeatVM _seat;
        private List<SeatType> _seatTypes;

        public frmUpdateSeat(ISeatManager seatManager,SeatVM seat)
        {
            InitializeComponent();
            _seatManager = seatManager;
            _seat = seat;
            lblSeatID.Content = seat.SeatID.ToString();
            lblDescription.Content = seat.SeatDescription;
            fillComboBox();

        }

        private void fillComboBox()
        {
            try
            {
                _seatTypes = _seatManager.GetAllSeatTypes();
                for(int i = 0; i < _seatTypes.Count; i++)
                {
                    cboDescriptions.Items.Add(_seatTypes.ElementAt(i).SeatDescription);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error "+ ex.InnerException);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!cboDescriptions.SelectedIndex.Equals(-1))
            {
                try
                {
                    Seat oldSeat = new Seat()
                    {
                        SeatID = _seat.SeatID,
                        SeatListID = _seat.SeatListID,
                        SeatTypeID = _seat.SeatTypeID,
                        Active = _seat.Active,
                        Available = _seat.Available
                    };
                    Seat newSeat = new Seat()
                    {
                        SeatID = _seat.SeatID,
                        SeatListID = _seat.SeatListID,
                        SeatTypeID = _seatTypes.ElementAt(cboDescriptions.SelectedIndex).SeatTypeID,
                        Active = _seat.Active,
                        Available = _seat.Available
                    };

                    if(_seatManager.EditSeat(oldSeat, newSeat))
                    {
                        MessageBox.Show("Seat Updated");
                        Close();
                    }
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Update failed. "+ex.InnerException);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
