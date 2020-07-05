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
    /// Interaction logic for EmployeeHome.xaml
    /// </summary>
    public partial class EmployeeHome : Window
    {
        private User _user;
        private IUserManager _userManager;
        private Customer _customer;
        private ICustomerManager _customerManager;
        private List<Station> _stations;
        private IStationManager _stationManager;
        private List<SeatType> _seatTypes;
        private ISeatManager _seatManager;
        private IRouteManager _routeManager;
        private ITrainScheduleManager _trainScheduleManager;
        private ITrainManager _trainManager;


        public EmployeeHome(User user, IUserManager userManager)
        {
            InitializeComponent();
            _user = user;
            _userManager = userManager;
            _customerManager = new CustomerManager();
            _stationManager = new StationManager();
            _seatManager = new SeatManager();
            _routeManager = new RouteManager();
            _trainScheduleManager = new TrainScheduleManager();
            _trainManager = new TrainManager();
            hideInnerTabs();
            hideAllTabs();
            showUserTabs();


        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Log Out", MessageBoxButton.YesNo
                , MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }

        private void showUserTabs()
        {
            foreach (var r in _user.Roles)
            {
                switch (r)
                {
                    case "Clerk":
                        //showScheduleTab();
                        showProfileTab();
                        showReservationTab();
                        break;
                    case "Admin":
                        showPersonnelTab();
                        break;
                    case "Conductor":
                        showProfileTab();
                        //showScheduleTab();
                        break;
                    case "Manager":
                        //showScheduleTab();
                        showPersonnelTab();
                        showProfileTab();
                        showTrainTab();
                        showReservationTab();
                        break;
                    default:
                        break;
                }
            }
        }

        private void showTrainTab()
        {
            calTrainSchedule.SelectedDate = DateTime.Now.AddMonths(3);
            btnTrain.Visibility = Visibility.Visible;
            btnTrain.Height = 25;
            btnTrain.Margin = new Thickness(0, 2, 0, 2);
        }

        private void showProfileTab()
        {
            btnProfile.Visibility = Visibility.Visible;
            btnProfile.Height = 25;
            btnProfile.Margin = new Thickness(0, 2, 0, 2);
        }
        
        private void showPersonnelTab()
        {
            btnPersonnel.Visibility = Visibility.Visible;
            btnPersonnel.Height = 25;
            btnPersonnel.Margin = new Thickness(0, 2, 0, 2);
        }
        /*
        private void showScheduleTab()
        {
            btnSchedule.Visibility = Visibility.Visible;
            btnSchedule.Height = 25;
            btnSchedule.Margin = new Thickness(0, 2, 0, 2);
        }
        */
        private void showReservationTab()
        {
            btnReservation.Visibility = Visibility.Visible;
            btnReservation.Height = 25;
            btnReservation.Margin = new Thickness(0, 2, 0, 2);
            tabSetReservation.Visibility = Visibility.Visible;
            showReservationTabSet();
        }

        private void showTrainTabSet()
        {
            tabSetTrain.Visibility = Visibility.Visible;
            fillTrainComboBoxes();
        }

        private void showReservationTabSet()
        {
            _stations = _stationManager.GetAllStationsByActive();
            _seatTypes = _seatManager.GetAllSeatTypes();
            fillNewReservationComboBoxes();
        }

        private void hideAllTabs()
        {
            btnPersonnel.Visibility = Visibility.Hidden;
            btnPersonnel.Height = 0;
            btnPersonnel.Margin = new Thickness(0);
            btnProfile.Visibility = Visibility.Hidden;
            btnProfile.Height = 0;
            btnProfile.Margin = new Thickness(0);
            btnSchedule.Visibility = Visibility.Hidden;
            btnSchedule.Height = 0;
            btnSchedule.Margin = new Thickness(0);
            btnReservation.Visibility = Visibility.Hidden;
            btnReservation.Height = 0;
            btnReservation.Margin = new Thickness(0);
            btnTrain.Visibility = Visibility.Hidden;
            btnTrain.Height = 0;
            btnTrain.Margin = new Thickness(0);
            hideInnerTabs();

        }

        private void hideInnerTabs()
        {
            tabSetReservation.Visibility = Visibility.Hidden;
            tabSetTrain.Visibility = Visibility.Hidden;
            tabSetProfile.Visibility = Visibility.Hidden;
            tabSetPersonnel.Visibility = Visibility.Hidden;
            //tabSet
        }

        private void TxtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {


            //"([\\w-+]+(?:\\.[\\w-+]+)*@(?:[\\w-]+\\.)+[a-zA-Z]{2,7})"
            //@"([\w+][@]([\w+])+[.]+(com|org|gov|edu))"
            if (txtEmail.Text.Length > 7 && txtEmail.Text.Contains('@') &&
                (txtEmail.Text.EndsWith(".com") ||
                txtEmail.Text.EndsWith(".org") ||
                txtEmail.Text.EndsWith(".edu") ||
                txtEmail.Text.EndsWith(".gov")))
            {
                txtFirstName.IsEnabled = true;
                txtFirstName.Background = Brushes.White;
                txtLastName.IsEnabled = true;
                txtLastName.Background = Brushes.White;
            }
            else
            {
                txtFirstName.IsEnabled = false;
                txtFirstName.Background = Brushes.LightGray;
                txtLastName.IsEnabled = false;
                txtLastName.Background = Brushes.LightGray;
            }
        }


        private void BtnCheckCustomer_Click(object sender, RoutedEventArgs e)
        {
            string lastName = "";
            string firstName = "";
            string email = null;
            if (null != txtEmail.Text && !txtEmail.Text.Trim().Equals(""))
            {
                email = txtEmail.Text;

                _customer = _customerManager.AuthenticateCustomerByEmail(email);


                if (txtFirstName.IsEnabled && _customer != null)
                {
                    firstName = txtFirstName.Text;
                    lastName = txtLastName.Text;
                    if (_customer.FirstName != null
                        && _customer.LastName != null
                        && !email.Equals("")
                        && (firstName != _customer.FirstName
                        || lastName != _customer.LastName))
                    {
                        if (!firstName.Equals("") && !lastName.Equals(""))
                        {
                            var customerUpdate = new frmUpdateCustomer(_customer, _customerManager);
                            if (customerUpdate.ShowDialog() == true)
                            {
                                Customer updatedCustomer = _customerManager.AuthenticateCustomerByEmail(email);
                                txtFirstName.Text = updatedCustomer.FirstName;
                                txtLastName.Text = updatedCustomer.LastName;
                            }
                        }
                        else if (firstName.Equals("")
                        || lastName.Equals(""))
                        {
                            Customer updatedCustomer = _customerManager.AuthenticateCustomerByEmail(email);
                            txtFirstName.Text = updatedCustomer.FirstName;
                            txtLastName.Text = updatedCustomer.LastName;
                        }


                    }
                }
            }
        }

        private void fillNewReservationComboBoxes()
        {
            foreach (Station station in _stations)
            {
                if (!station.StationType.Equals("Service"))
                {
                    cboStartStation.Items.Add(station.StationName);
                    cboEndStation.Items.Add(station.StationName);
                }
            }

            for (int i = 0; i < _seatTypes.Count; i++)
            {
                cboSeatType.Items.Add(_seatTypes.ElementAt(i).Price.ToString("C") + ", " + _seatTypes.ElementAt(i).SeatDescription);
            }
        }

        private void fillTrainComboBoxes()
        {
            fillStart();
            fillEnd();
        }

        private void fillStart()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < 12; j++)
                {
                    if (i == 0 && j != 11)
                    {
                        cboStart.Items.Add(j.ToString() + " am");
                    }
                    else if (i == 0 && j == 11)
                    {
                        cboStart.Items.Add(j.ToString() + " am");
                        cboStart.Items.Add("12 pm");
                    }
                    else
                    {
                        cboStart.Items.Add(j.ToString() + " pm");
                    }
                }
            }
        }

        private void fillEnd()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j <= 12; j++)
                {
                    if (i == 0 && j < 12)
                    {
                        cboEnd.Items.Add(j.ToString() + " am");
                    }
                    else if (i == 0 && j == 12)
                    {
                        cboEnd.Items.Add(j.ToString() + " pm");
                    }
                    else if (j < 12)
                    {
                        cboEnd.Items.Add(j.ToString() + " pm");
                    }
                    else
                    {
                        cboEnd.Items.Add(j.ToString() + " am");
                    }
                }
            }
            cboEnd.Items.RemoveAt(0);
        }

        private void BtnTrain_Click(object sender, RoutedEventArgs e)
        {
            hideInnerTabs();
            showTrainTabSet();
        }

        private void BtnReservation_Click(object sender, RoutedEventArgs e)
        {
            hideInnerTabs();
            showReservationTab();

        }

        private void CboStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboEnd.Items.Clear();
            fillEnd();
            int selection = cboStart.SelectedIndex;
            for (int i = 0; i < selection; i++)
            {
                cboEnd.Items.RemoveAt(0);
            }
            cboEnd.SelectedIndex = 0;
        }

        private void BtnGenerateTrainSchedule_Click(object sender, RoutedEventArgs e)
        {

            DateTime startDateTime = (DateTime)calTrainSchedule.SelectedDate;
            if (0 < startDateTime.CompareTo(DateTime.Now))
            {
                int hours;
                int mins;
                if (!cboEnd.SelectedItem.Equals("12 am"))
                {
                    hours = cboStart.Items.IndexOf(cboEnd.SelectedItem) - cboStart.SelectedIndex;
                }
                else
                {
                    hours = (23 - cboStart.SelectedIndex);
                }
                mins = hours * 60;

                bool success = false;
                TrainSchedule trainSchedule = new TrainSchedule();
                try
                {
                    startDateTime = startDateTime.AddHours(cboStart.SelectedIndex + 1);
                    trainSchedule = _trainScheduleManager.GetNewTrainSchedule(_user.EmployeeID, startDateTime);
                    
                    trainSchedule.TrainScheduleID = _trainScheduleManager.AddTrainSchedule(trainSchedule);

                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Schedule not added " + ex.InnerException.Message);
                }

                if (success)
                {
                    success = false;
                    try
                    {
                        List<TrainScheduleLine> lines = _trainScheduleManager.GenerateTrainSchedule(mins, trainSchedule.TrainScheduleID, startDateTime);

                        foreach (TrainScheduleLine line in lines)
                        {
                            _trainScheduleManager.AddTrainScheduleLine(line);
                        }
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Schedule not fully added " + ex.InnerException.Message);
                    }
                    if (success)
                    {
                        dgTrainSchdule.ItemsSource = _trainScheduleManager.GetTrainScheduleLinesByTrainScheduleID(trainSchedule.TrainScheduleID);
                        dgTrainSchdule.Visibility = Visibility.Visible;
                        btnCloseTrainSchedule.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a date today or later.");
            }
        }

        private void TabSeats_GotFocus(object sender, RoutedEventArgs e)
        {
            if(dgSeats.ItemsSource == null)
            {
                setDataGridSeats();
            }
        }

        private void setDataGridSeats()
        {
            try
            {
                dgSeats.ItemsSource = _seatManager.GetAllSeats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void TabRoute_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dgRoute.ItemsSource == null)
                {
                    setDgRoute();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }
        }

        private void setDgRoute()
        {
            List<RouteLineVM> routeLines = new List<RouteLineVM>();

            foreach (Route route in _routeManager.GetAllRoutesByActive())
            {
                foreach (RouteLineVM line in _routeManager.GetAllRouteLinesByRouteID(route.RouteID))
                {
                    routeLines.Add(line);
                }
            }

            dgRoute.ItemsSource = routeLines;
        }

        private void DgSeats_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgSeats.Columns.RemoveAt(3);
            dgSeats.Columns.RemoveAt(3);
            dgSeats.Columns.RemoveAt(3);
            dgSeats.Columns.RemoveAt(3);
            dgSeats.Columns.RemoveAt(3);
            dgSeats.Columns.RemoveAt(3);
            foreach (var column in dgSeats.Columns)
            {
                column.Width = 95;
            }
        }

        private void BtnEditSeat_Click(object sender, RoutedEventArgs e)
        {
            editSeat();
        }

        private void editSeat()
        {
            try
            {
                if (!dgSeats.SelectedItem.Equals(null))
                {
                    var updateSeat = new frmUpdateSeat(_seatManager, (SeatVM)dgSeats.SelectedItem);
                    updateSeat.ShowDialog();
                    setDataGridSeats();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("No seat selected.");
            }
        }

        private void DgTrainSchdule_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgTrainSchdule.Columns.RemoveAt(0);
            dgTrainSchdule.Columns.RemoveAt(0);
            dgTrainSchdule.Columns.RemoveAt(2);
            dgTrainSchdule.Columns.RemoveAt(2);
            dgTrainSchdule.Columns.RemoveAt(3);
        }

        private void BtnCloseTrainSchedule_Click(object sender, RoutedEventArgs e)
        {
            dgTrainSchdule.Visibility = Visibility.Hidden;
            btnCloseTrainSchedule.Visibility = Visibility.Hidden;
        }

        private void BtnRemoveSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!dgSeats.SelectedItem.Equals(null))
                {
                    SeatVM seat = (SeatVM)dgSeats.SelectedItem;
                    MessageBoxResult result = MessageBox.Show("Remove seat " + seat.SeatID.ToString() + " from " + seat.TrainName,"Confirm Removal",MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        _seatManager.DeacivateSeat(new Seat()
                        {
                            Active = seat.Active,
                            Available = seat.Available,
                            SeatID = seat.SeatID,
                            SeatListID = seat.SeatListID,
                            SeatTypeID = seat.SeatTypeID
                        });
                        setDataGridSeats();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No seat selected.");
            }
        }

        private void BtnEditSeatType_Click(object sender, RoutedEventArgs e)
        {
            var editSeatType = new frmEditSeatType(_seatManager);
            editSeatType.ShowDialog();
            setDataGridSeats();
            
        }

        private void DgRoute_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgRoute.Columns.RemoveAt(2);
            dgRoute.Columns.RemoveAt(2);
            dgRoute.Columns.RemoveAt(2);
        }

        private void BtnAddSeat_Click(object sender, RoutedEventArgs e)
        {
            var addSeat = new frmAddSeat(_seatManager, _trainManager);
            addSeat.ShowDialog();
            setDataGridSeats();
        }

        private void BtnAddStation_Click(object sender, RoutedEventArgs e)
        {
            var addStation = new frmAddStation(_stationManager);
            addStation.ShowDialog();
            setDgRoute();
        }

        private void BtnEditStation_Click(object sender, RoutedEventArgs e)
        {
            if (!dgRoute.SelectedIndex.Equals(-1))
            {
                try
                {
                    RouteLineVM routeLineVM = (RouteLineVM)dgRoute.SelectedItem;
                    Station station = _stationManager.GetStationByID(routeLineVM.StartStation);
                    var editStation = new frmEditStation(_stationManager, station);
                    editStation.ShowDialog();
                    setDgRoute();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Update failed. " + ex.InnerException);
                }
            }
        }

        private void BtnRemoveStation_Click(object sender, RoutedEventArgs e)
        {
            if (!dgRoute.SelectedIndex.Equals(-1))
            {
                RouteLineVM routeLineVM = (RouteLineVM)dgRoute.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you wish to deactivate " + routeLineVM.StationStartName + "?", "Confirm", MessageBoxButton.YesNo);
                if (result.Equals(MessageBoxResult.Yes))
                {
                    try
                    {
                        _stationManager.DeactivateStation(routeLineVM.StartStation);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Update failed. " + ex.InnerException);
                    }
                }
            }
        }

        private void TabTrain_GotFocus(object sender, RoutedEventArgs e)
        {

            if (dgTrain.ItemsSource == null)
            {
                fillDgTrain();
            }
        }

        private void fillDgTrain()
        {
            try
            {
                dgTrain.ItemsSource = _trainManager.GetAllTrainsActive();
            }
            catch (Exception ex)
            {

                MessageBox.Show("List not found. " + ex.InnerException);
            }
        }

        private void BtnEditTrain_Click(object sender, RoutedEventArgs e)
        {
            if (!dgTrain.SelectedIndex.Equals(-1))
            {
                Train train = (Train)dgTrain.SelectedItem;
                var editTrain = new frmEditTrain(_trainManager,train);
                editTrain.ShowDialog();
            }
            fillDgTrain();
        }

        private void TabProfile_GotFocus(object sender, RoutedEventArgs e)
        {
            tabSetProfile.Visibility = Visibility.Visible;
            txtProfileEmail.Text = _user.Email;
            txtProfileFirstName.Text = _user.FirstName;
            txtProfileLastName.Text = _user.LastName;
            txtProfilePhoneNumber.Text = _user.PhoneNumber;
        }

        private void BtnProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            if(btnProfileEdit.Content.Equals("Edit Profile"))
            {
                btnProfileEdit.Content = "Save";
                txtProfileFirstName.IsEnabled = true;
                txtProfileLastName.IsEnabled = true;
                txtProfileEmail.IsEnabled = true;
                txtProfilePhoneNumber.IsEnabled = true;
            }
            else
            {
                if (_userManager.Validate(txtProfileEmail.Text)&&_userManager.Validate(txtProfileFirstName.Text)
                    && _userManager.Validate(txtProfileLastName.Text) && _userManager.Validate(txtProfilePhoneNumber.Text)
                    && txtProfilePhoneNumber.Text.Length.Equals(11) && txtProfileEmail.Text.Length > 7 && txtProfileEmail.Text.Contains('@') &&
                    (txtProfileEmail.Text.EndsWith(".com") || txtProfileEmail.Text.EndsWith(".org") || txtProfileEmail.Text.EndsWith(".edu") || txtProfileEmail.Text.EndsWith(".gov")))
                {
                    _userManager.UpdateUser(_user, new User()
                    {
                        EmployeeID = _user.EmployeeID,
                        Email = txtProfileEmail.Text,
                        FirstName = txtProfileFirstName.Text,
                        LastName = txtLastName.Text,
                        PhoneNumber = txtProfilePhoneNumber.Text,
                        Active = _user.Active,
                        Roles = _user.Roles,
                        PasswordHash = _user.PasswordHash
                    });
                    btnProfileEdit.Content = "Edit Profile";
                    txtProfileFirstName.IsEnabled = false;
                    txtProfileLastName.IsEnabled = false;
                    txtProfileEmail.IsEnabled = false;
                }
            }
        }

        private void BtnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            var updatePassword = new frmUpdatePassword(_user, _userManager);
            updatePassword.ShowDialog();
        }

        private void BtnProfile_Click(object sender, RoutedEventArgs e)
        {
            hideInnerTabs();
            showProfileTab();
            tabSetProfile.Visibility = Visibility.Visible;
        }

        private void CboStartStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Station station = _stationManager.GetStationByName(cboStartStation.SelectedItem.ToString());
            List<TrainScheduleLineVM> trainScheduleLines = _trainScheduleManager.GetTrainScheduleLinesByStationID(station.StationID);
            for (int i = 0; i < trainScheduleLines.Count; i++)
            {
                cboLeaveTime.Items.Add(trainScheduleLines.ElementAt(i).ArrivalTime);
            }
            cboLeaveTime.IsEnabled = true;
        }

        private void BtnPersonnel_Click(object sender, RoutedEventArgs e)
        {
            hideInnerTabs();
            showPersonnelTab();
            tabSetPersonnel.Visibility = Visibility.Visible;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (_userManager.Validate(txtAddUserEmail.Text) && _userManager.Validate(txtAddUserFirstName.Text)
                    && _userManager.Validate(txtAddUserLastName.Text) && _userManager.Validate(txtAddUserPhoneNumber.Text)
                    && txtAddUserPhoneNumber.Text.Length.Equals(11) && txtAddUserEmail.Text.Length > 7 && txtAddUserEmail.Text.Contains('@') &&
                    (txtAddUserEmail.Text.EndsWith(".com") || txtAddUserEmail.Text.EndsWith(".org") || txtAddUserEmail.Text.EndsWith(".edu") || txtAddUserEmail.Text.EndsWith(".gov")))
            {
                try
                {

                    _userManager.AddUser(new User()
                    {
                        Email = txtAddUserEmail.Text,
                        FirstName = txtAddUserFirstName.Text,
                        LastName = txtAddUserLastName.Text,
                        PhoneNumber = txtAddUserPhoneNumber.Text,
                        Active = true
                    });
                    MessageBox.Show("Success");
                    txtAddUserEmail.Text = "";
                    txtAddUserFirstName.Text = "";
                    txtAddUserLastName.Text = "";
                    txtAddUserPhoneNumber.Text = "";
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Failed to add user"+ex.InnerException);
                }

            }
        }

        private void BtnStationList_Click(object sender, RoutedEventArgs e)
        {
            var stationList = new frmStationList(_stationManager);
            stationList.ShowDialog();
        }

        private void TabUserList_GotFocus(object sender, RoutedEventArgs e)
        {
            if(dgUsers.ItemsSource == null)
            {
                fillDgUsers();
            }
        }

        private void fillDgUsers(bool active = true)
        {
            try
            {
                dgUsers.ItemsSource = _userManager.RetrieveUserListByActive(active);
            }
            catch (Exception ex)
            {

                MessageBox.Show("List not found. "+ex.InnerException);
            }
        }



        private void BtnEditUser_Click(object sender, RoutedEventArgs e)
        {
            updateUser();

        }

        private void updateUser()
        {
            if (!dgUsers.SelectedIndex.Equals(-1))
            {
                User user = (User)dgUsers.SelectedItem;
                var updateUser = new frmUpdateUser(_userManager, user);
                updateUser.ShowDialog();
            }
        }

        private void CbUserListActive_Click(object sender, RoutedEventArgs e)
        {
            fillDgUsers((bool)cbUserListActive.IsChecked);
        }

        private void TabUserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            updateUser();
        }

        private void DgSeats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editSeat();
        }
    }
}
