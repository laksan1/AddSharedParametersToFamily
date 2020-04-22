using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace AddSharedParametersToFamily.Model
{
    public class MyParameter
    {
        //Имя группы
        public string Group { get; set;}
        public ExternalDefinition def;

        //Имя параметра
        public string Name { get; set; }

        //Проверка checkbox parameter
        public bool isCheckedParameter;

        //Проверка checkbox type
        public bool isCheckedType;
        public bool IsCheckedParameter
        {
            get { return isCheckedParameter; }
            set { isCheckedParameter = value; OnPropertyChanged(); }
        }
        public bool IsCheckedType
        {
            get { return isCheckedType; }
            set { isCheckedType = value; OnPropertyChanged(); }
        }

        //Проверка видимости
        public bool isCollapsed = false;
        public bool IsCollapsed
        {
            get { return isCollapsed; }
            set {
                isCollapsed = value;
                OnPropertyChanged("IsCollapsed");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
       
    }
}
