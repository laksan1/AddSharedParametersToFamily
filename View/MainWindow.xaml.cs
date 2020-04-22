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
using MahApps.Metro.Controls;
using AddSharedParametersToFamily.ViewModel;
using AddSharedParametersToFamily.Model;
using System.ComponentModel;
using AddSharedParametersToFamily.MVVM;

namespace AddSharedParametersToFamily.View
{
  
    public partial class MainWindow : /*Window,*/ MetroWindow, IDisposable
    {
        public RelayCommand SelectedComboxCommand { get; private set; }
        ICollectionView FilterCollectionPar;
        MainWindowViewModel viewModel;
        public MainWindow(MainWindowViewModel vm_)
        {
            InitializeComponent();
             DataContext = vm_;
            viewModel = vm_;
            comboBox.ItemsSource = vm_.List_Group;
            comboBox.SelectedIndex = 0;
            FilterCollectionPar = CollectionViewSource.GetDefaultView(vm_.ListParameterCollection);
            DataGrid1.ItemsSource = FilterCollectionPar;

        }
        //public bool TextFilter(object o)
        //{     
        //    SelGroupItem = (comboBox.SelectedItem as MyParameter).Group;
        //    MyParameter p = (o as MyParameter);
        //    if (p == null && (p.Name != null))

        //        return false;

        //    if (p.def.OwnerGroup.Name == SelGroupItem.ToString() && SelGroupItem != null)
        //    {

        //        return true;
        //    }
        //    else
        //        return false;
        //}
        public void Dispose()
        {
            this.Close();
        }
      
        private void Perfom_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCombo = comboBox.SelectedItem;
            SelectedComboxCommand = viewModel.CommandCheck;
            SelectedComboxCommand.Execute(selectedCombo);
            DataGrid1.Items.Refresh();

        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
          
            //FilterCollectionPar.Filter = TextFilter;
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RelayCommand commChecckbo1 = viewModel.CommandCheckAllParameter;
            commChecckbo1.Execute(null);
            DataGrid1.Items.Refresh();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RelayCommand commChecckbo1 = viewModel.CommandCheckAllParameter;
            commChecckbo1.Execute(null);
            DataGrid1.Items.Refresh();
        }

        private void Check1_Checked(object sender, RoutedEventArgs e)
        {
            RelayCommand commChecckbo2 = viewModel.CommandCheckAllType;
            commChecckbo2.Execute(null);
            DataGrid1.Items.Refresh();
        }

        private void Check1_Unchecked(object sender, RoutedEventArgs e)
        {
            RelayCommand commChecckbo2 = viewModel.CommandCheckAllType;
            commChecckbo2.Execute(null);
            DataGrid1.Items.Refresh();
        }

    }
}
