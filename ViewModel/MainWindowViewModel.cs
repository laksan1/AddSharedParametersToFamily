using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Interop;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using AddSharedParametersToFamily.Model;
using System.Windows.Input;
using AddSharedParametersToFamily.ViewModel;
using Autodesk.Revit.UI.Selection;
using AddSharedParametersToFamily.MVVM;
using AddSharedParametersToFamily.View;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace AddSharedParametersToFamily.ViewModel
{
    public class MainWindowViewModel : ModelBase
    {
        internal RevitModelClass RevitModel { get; set; }
        private ObservableCollection<MyParameter> _listParameterCollection;
        private ObservableCollection<MyParameter> _list_Group;
        public ObservableCollection<ElementId> SelectedFamilyIds;
        public RelayCommand CommandCheckAllParameter { get; private set; }
        public RelayCommand CommandCheckAllType { get; private set; }
        MyParameter TypeGroup;
        public RelayCommand CommandCheck { get; private set; }
        
        private ICommand _command;
        private Action _closeAction;
        private bool? isAllSelectedParameters;
        private bool? isAllSelectedTypes;
       // public string imageToolSource;//Картинка toolTip
        public bool? IsAllSelectedParameters
        {
            get { return isAllSelectedParameters; }
            set
            {
                isAllSelectedParameters = value;
                OnPropertyChanged();
            }
        }
        public bool? IsAllSelectedTypes
        {
            get { return isAllSelectedTypes; }
            set
            {
                isAllSelectedTypes = value;
                OnPropertyChanged();
            }
        }



        //Отобразить выбранную группу из CheckBox
        public void CommandCheckAction(object o)
        {
           TypeGroup = o as MyParameter;
            foreach(var el in ListParameterCollection)
            {
                if(el.def.OwnerGroup.Name == TypeGroup.Group)
                {
                    el.IsCollapsed = false;
                }
                else
                {
                    el.IsCollapsed = true;
                }
            }
        }

        //Конструктор MainWindowViewModel
        public MainWindowViewModel()
        {
            CommandCheckAllParameter = new RelayCommand(OnCheckAllParameteres);
            CommandCheckAllType = new RelayCommand(OnCheckAllTypes);
            CommandCheck = new RelayCommand(CommandCheckAction);
            isAllSelectedParameters = false;
            isAllSelectedTypes = false;
        }

        //Выбрать\Снять все (Параметры)
        private void OnCheckAllParameteres(object o)//упростить
        {
            if (isAllSelectedParameters == true)
            {

                foreach (var ch in ListParameterCollection)
                {
                    if (TypeGroup.Group == ch.def.OwnerGroup.Name)
                    {
                        ch.isCheckedParameter = true;
                    }

                }
            }
            else
            {
                foreach (var ch in ListParameterCollection)
                    if (TypeGroup.Group == ch.def.OwnerGroup.Name)
                    {
                        ch.isCheckedParameter = false;
                        ch.IsCheckedType = false;
                    }

            }
        }

        //Выбрать\Снять все (Типы)
        private void OnCheckAllTypes(object o)
        {
            if (isAllSelectedTypes == true && isAllSelectedParameters==true)
                foreach (var ch in ListParameterCollection)
                {
                    if (TypeGroup.Group == ch.def.OwnerGroup.Name)
                    {
                        ch.IsCheckedType = true;
                    }
                }
            else
            {
                foreach (var ch in ListParameterCollection)
                    if (TypeGroup.Group == ch.def.OwnerGroup.Name)
                    {
                        ch.IsCheckedType = false;
                    }
            }
        }

        public ObservableCollection<MyParameter> ListParameterCollection
        {
            get => _listParameterCollection;
            set
            {
                _listParameterCollection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<MyParameter> List_Group
        {
            get => _list_Group;
            set
            {
                _list_Group = value;
                OnPropertyChanged();
            }
        }
  
        //Команда на добавление общих параметров
        public ICommand CommandAddParameters
        {
            get
            {
                if (_command == null)
                    _command = new RelayCommand(o => 
                    {
                        var CheckedParameters = ListParameterCollection.Where(par => par.isCheckedParameter).ToList();
                        RevitModel.AddParametersToFamily(CheckedParameters, SelectedFamilyIds);
                    });
                return _command;
            }
        }

        public Action CloseAction
        {
            get => _closeAction;
            set
            {
                _closeAction = value;
                OnPropertyChanged();
            }
        }

    }
}