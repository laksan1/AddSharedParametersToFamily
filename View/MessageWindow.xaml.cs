using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using MahApps.Metro.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AddSharedParametersToFamily.Model;
using AddSharedParametersToFamily.ViewModel;

namespace AddSharedParametersToFamily.View
{
    /// <summary>
    /// Логика взаимодействия для СoincidenceOfParameters.xaml
    /// </summary>
    public partial class MessageWindow : MetroWindow
    {
       MessageVewModel cv;
        public MessageWindow(MessageVewModel _CP)
        {
            InitializeComponent();
            cv = _CP;
            coincidencesBox.Text = cv.RevitModel.isMessage;
            DataContext = _CP;

        }
        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
