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
    /// Interaction logic for frmEditTrain.xaml
    /// </summary>
    public partial class frmEditTrain : Window
    {
        Train _train;
        ITrainManager _trainManager;

        public frmEditTrain(ITrainManager trainManager, Train train)
        {
            InitializeComponent();
            _train = train;
            _trainManager = trainManager;
            lblTrain.Content = _train.TrainID;
            lblName.Content = _train.TrainName;

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string trainName;
            if (!txtTrainName.Text.Equals(""))
            {
                if (_trainManager.Validate(txtTrainName.Text))
                {
                    trainName = txtTrainName.Text;
                }
                else
                {
                    txtTrainName.Text = "";
                    MessageBox.Show("That is not a valid name.");
                    return;
                }
            }
            else
            {
                trainName = _train.TrainName;
            }
            try
            {
                _trainManager.EditTrain(_train, new Train()
                {
                    Active = (bool)cbActive.IsChecked,
                    RouteID = _train.RouteID,
                    TrainID = _train.TrainID,
                    TrainName = trainName
                });
                MessageBox.Show("Train edited.");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to update. "+ex.InnerException);
            }
        }
    }
}
