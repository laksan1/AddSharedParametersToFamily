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
using AddSharedParametersToFamily.View;
using AddSharedParametersToFamily.ViewModel;
using AddSharedParametersToFamily.Model;
using System.IO;
using Autodesk.Revit.UI.Selection;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Windows;

namespace AddSharedParametersToFamily.Model
{
    [Transaction(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class AddSharedParametersToFamily : IExternalCommand
    {

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
            {
            var uiApplication = commandData.Application;
            var uidoc = uiApplication.ActiveUIDocument;
            var doc = uidoc.Document;
            var vm = new MainWindowViewModel();
            vm.RevitModel = new RevitModelClass(uiApplication);

            //Id выбранных элементов
            vm.SelectedFamilyIds = RevitModelClass.SelectElement(uidoc, doc);
            if (vm.SelectedFamilyIds == null)
            {
                return Result.Failed;
            }
            // Получение всех групп из ФОП
            vm.List_Group = RevitModelClass.RetriveGroup(doc);

            //Получение названий параметров
            vm.ListParameterCollection = RevitModelClass.GenerateParametersList(doc);
            using (var view = new MainWindow(vm))
            {
                if (view.ShowDialog() == true) return Result.Succeeded;
            }

            return Result.Succeeded;
          }

    }
}

