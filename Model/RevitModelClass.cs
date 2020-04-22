 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using AddSharedParametersToFamily.ViewModel;
using AddSharedParametersToFamily.Model;
using System.Linq;
using AddSharedParametersToFamily.View;
using AddSharedParametersToFamily.MVVM;

namespace AddSharedParametersToFamily.Model
{ 
    public  class RevitModelClass :ModelBase
    {
        private UIApplication _uiApplication;
        private Autodesk.Revit.ApplicationServices.Application _application;
        private Document _document;

        //Вывод сообщения о совпадениях
        public string isMessage;
        public string IsMessage
        {
            get { return isMessage; }
            set { isMessage = value; OnPropertyChanged(); }
        }

        private readonly UIDocument _uiDocument;
        //Метод добавления общих параметров
        public bool AddParametersToFamily(List<MyParameter> pars, ObservableCollection<ElementId> setfamIds)
        {
          
            //Словарь о совпадениях параметров в семейтсвах
            foreach (ElementId familyId in setfamIds)
            {
                Element family = _document.GetElement(familyId);
                if (familyId == null) return false;
                if (family is FamilyInstance fi)
                {
                    var famDoc = _document.EditFamily(fi.Symbol.Family);
                    using (var tr = new Transaction(famDoc, "AddParameters"))
                    {
                        tr.Start();
                        foreach (var par in pars)
                        {
                            //Добавление параметра
                            try
                            {
                                BuiltInParameterGroup gr = BuiltInParameterGroup.PG_GENERAL;
                                #region SWITCH группирование
                                switch (par.def.Name)
                                {
                                    case "ADSK_Позиция":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Наименование":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Наименование краткое":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Обозначение":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Марка":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Код изделия":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Завод-изготовитель":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Примечание":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Масса":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Масса_Текст":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Единица измерения":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;

                                    case "ADSK_Расход жидкости":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;

                                    case "ADSK_Группирование":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Номер секции":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    case "ADSK_Этаж":
                                        gr = BuiltInParameterGroup.PG_DATA;
                                        break;
                                    ////////////////////////////////////////////
                                    case "ADSK_Диаметр условный":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Ширина":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Высота":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Глубина":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Диаметр":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_ДиаметрИзделия":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Радиус":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Толщина":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Толщина основы":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Толщина полки":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_Толщина стенки":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;

                                    case "ADSK_Размер_УголПоворота":
                                        gr = BuiltInParameterGroup.PG_GEOMETRY;
                                        break;
                                    ////////////////////////////////////////////
                                    case "ADSK_Классификация нагрузок":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;
                                    case "ADSK_Количество фаз":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;

                                    case "ADSK_Количество фаз числовое":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;

                                    case "ADSK_Коэффициент мощности":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;

                                    case "ADSK_Напряжение":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;

                                    case "ADSK_Номинальная мощность":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;

                                    case "ADSK_Полная мощность":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;
                                    case "ADSK_Ток":
                                        gr = BuiltInParameterGroup.PG_ELECTRICAL_LOADS;
                                        break;
                                    //////////////////////////////////////////////////
                                    case "ADSK_Код лотка":
                                        gr = BuiltInParameterGroup.PG_TEXT;
                                        break;
                                    //////////////////////////////////////////////////
                                    case "ADSK_Расход воздуха":
                                        gr = BuiltInParameterGroup.PG_MECHANICAL_AIRFLOW;
                                        break;
                                    case "ADSK_Свободный напор воздуха":
                                        gr = BuiltInParameterGroup.PG_MECHANICAL_AIRFLOW;
                                        break;
                                }
                                #endregion
                                famDoc.FamilyManager.AddParameter(par.def, gr, !par.IsCheckedType);
                            }
                            catch
                            {
                                //Сообщение о наличие параметра в таком семействе
                                isMessage += "Family \u0022" + fi.Symbol.FamilyName + "\u0022 already has parameter \u0022" + par.Name + "\u0022" + "\n";
                            }
                        }
                        tr.Commit();
                    }
                    //Загрузка семейства в проект
                    famDoc.LoadFamily(_document, new FamilyLoadOptions());
                    famDoc.Close(false);
                }
            }
            if (!String.IsNullOrEmpty(isMessage))
            {
                MessageVewModel MessVewModel = new MessageVewModel(this);
                MessageWindow MessWindow = new MessageWindow(MessVewModel);
                MessWindow.ShowDialog();
            }
            return true;
        }
           

        //Конструктор RevitModelClass
            public RevitModelClass(UIApplication uiapp)
            {
                _uiApplication = uiapp;
                _application = _uiApplication.Application;
                _uiDocument = _uiApplication.ActiveUIDocument;
                _document = _uiDocument.Document;
            }

        //Создания списка из общих параметров
            public static ObservableCollection<MyParameter> GenerateParametersList(Document doc)
            {
                ObservableCollection<MyParameter> definitions = new ObservableCollection<MyParameter>();
                var deffile = doc.Application.OpenSharedParameterFile();
                if (deffile != null)
                {
                    foreach (var gr in deffile.Groups)
                    {
                        foreach (ExternalDefinition df in gr.Definitions)
                        {
                            definitions.Add(new MyParameter() { Name = df.Name, def = df });
                        }
                    }
                }
                return definitions;
            }

        //Опции загрузки семейства в проект
        class FamilyLoadOptions : IFamilyLoadOptions
        {
            public bool OnFamilyFound(bool familyInUse, out bool overwriteParameterValues)
            {
                overwriteParameterValues = true;

                return true;
            }
            public bool OnSharedFamilyFound(Family sharedFamily, bool familyInUse, out FamilySource source, out bool overwriteParameterValues)
            {
                source = FamilySource.Family;

                overwriteParameterValues = true;

                return true;
            }
        }

        //Метод извлечение групп из ФОП
        public static ObservableCollection<MyParameter> RetriveGroup(Document doc)
        {
            ObservableCollection<MyParameter> L_Group = new ObservableCollection<MyParameter>();
            var deffile = doc.Application.OpenSharedParameterFile();
            if (deffile != null)
            {
                foreach (DefinitionGroup grp in deffile.Groups)
                {
                    L_Group.Add(new MyParameter { Group = grp.Name });
                }
            }
            return L_Group;
        }

        //Метод получение Id выбранных элементов
        public static ObservableCollection<ElementId> SelectElement(UIDocument _uidoc, Document _doc)
        {
            Selection selection = null;
            selection = _uidoc.Selection;
            ObservableCollection<ElementId> IdFamily = new ObservableCollection<ElementId>();
            ObservableCollection<string> ListFamilyName = new ObservableCollection<string>();
            var collector = new FilteredElementCollector(_doc).OfClass(typeof(FamilyInstance)).ToList();
            ICollection<ElementId> selectedIds = selection.GetElementIds();
            if (selectedIds.Count == 0)
            {
                TaskDialog.Show("Revit", "You haven't selected any elements.");
                return null;
            }
            foreach (ElementId el in selectedIds)
            {
            var TypeEl = _doc.GetElement(el);
                //Если Selection = FamilyInstance

                if (TypeEl is FamilyInstance)
                {
                    IdFamily.Add(el);
                }
                else
                {
                    //Если Selection = FamilySymbol
                    foreach (FamilyInstance FamIn in collector)
                    {
                        FamilySymbol Sym = _doc.GetElement(el) as FamilySymbol;
                        if (Sym != null)
                        {
                            if (FamIn.Symbol.Family.Name == Sym.FamilyName && ((ListFamilyName.Contains(FamIn.Symbol.Family.Name)) == false))
                            {
                                ListFamilyName.Add(FamIn.Symbol.Family.Name);
                                IdFamily.Add(FamIn.Id);
                            }
                        }
                    }
                }
            }
            return IdFamily;

        }
    }
}
