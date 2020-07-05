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
    /// Interaction logic for frmEditSeatType.xaml
    /// </summary>
    public partial class frmEditSeatType : Window
    {
        bool _addMode;
        List<SeatType> _seatTypes;
        ISeatManager _seatManager;

        public frmEditSeatType(ISeatManager seatManager)
        {
            InitializeComponent();
            _addMode = false;
            _seatManager = seatManager;
            _seatTypes = _seatManager.GetAllSeatTypes();
            fillCbo();
        }

        private void setLblPrice()
        {
            lblCurrentPrice.Content = _seatTypes.ElementAt(cboSeatTypes.SelectedIndex).Price.ToString("C");
        }

        private void fillCbo()
        {
            for (int i = 0; i < _seatTypes.Count; i++)
            {
                cboSeatTypes.Items.Add(_seatTypes.ElementAt(i).SeatDescription);
            }
            cboSeatTypes.SelectedIndex = 0;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            grdNewSeatType.Visibility = Visibility.Hidden;
            grdUpdate.Visibility = Visibility.Visible;
            _addMode = false;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            grdUpdate.Visibility = Visibility.Hidden;
            grdNewSeatType.Visibility = Visibility.Visible;
            _addMode = true;
        }

        private void CboSeatTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setLblPrice();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_addMode)
            {
                decimal price;
                if (Decimal.TryParse(txtPrice.Text,out price)&&_seatManager.Validate(txtDescription.Text))
                {
                    SeatType seatType = new SeatType
                    {
                        SeatDescription = txtDescription.Text,
                        Price = price
                    };
                    if (_seatManager.AddSeatType(seatType))
                    {
                        MessageBoxResult result = MessageBox.Show("Seat type added", "Confirm", MessageBoxButton.OK);
                        if(result == MessageBoxResult.OK)
                        {
                            Close();
                        }
                    }
                }
            }
            else
            {
                SeatType oldSeatType = _seatTypes.ElementAt(cboSeatTypes.SelectedIndex);
                SeatType newSeatType;
                decimal price;
                if(Decimal.TryParse(txtNewPrice.Text,out price))
                {
                    newSeatType = new SeatType()
                    {
                        SeatTypeID = oldSeatType.SeatTypeID,
                        SeatDescription = oldSeatType.SeatDescription,
                        Active = oldSeatType.Active,
                        Price = price
                    };
                    if (_seatManager.EditSeatType(oldSeatType, newSeatType))
                    {
                        MessageBoxResult result = MessageBox.Show("Seat type updated", "Confirm", MessageBoxButton.OK);
                        if (result == MessageBoxResult.OK)
                        {
                            Close();
                        }
                    }
                }

            }
        }
    }
}
