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

namespace AddSharedParametersToFamily
{
    [Transaction(TransactionMode.Manual)]
    [RegenerationAttribute(RegenerationOption.Manual)]
    public class WPFexample : IExternalCommand //IExternalApplication,
    {
        //public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)

        //{
        //    application.CreateRibbonTab("CISP");
        //    var ribbonPanel1 = application.CreateRibbonPanel("CISP", "Shared parameters");
        //    String pathDll1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)+ "\\AddSharedParametersToFamily.dll";
        //    PushButtonData ButtonData1 = new PushButtonData("But1", "Add parameters", pathDll1, "AddSharedParametersToFamily.WPFexample");
        //    PushButton pushButton1 = ribbonPanel1.AddItem(ButtonData1) as PushButton;
        //    pushButton1.Enabled = true;
        //    pushButton1.ToolTip = "In the dialog box, select the parameters for adding to family";
        //    pushButton1.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddSharedParametersToFamily;component/Resources/Addpar.png"));

        //    #region second button
        //    var ribbonPanel2 = application.CreateRibbonPanel("CISP", "Export");
        //    String pathDll2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ExportVStoExcel.dll";
        //    PushButtonData pushButtonData2 = new PushButtonData("But2", "Export specifications", pathDll2, "ExportVStoExcel.Model.MainExportSpec");
        //    pushButtonData2.ToolTip = "Export specifications to Excel";
        //    pushButtonData2.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddSharedParametersToFamily;component/Resources/Excel1.png"));
        //    #endregion

        //    #region third button
        //    String pathDll3 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Pivot.dll";
        //    PushButtonData pushButtonData3 = new PushButtonData("But3", "Pivot table", pathDll3, "Pivot.Model.PivotTable");
        //    pushButtonData3.ToolTip = "Export pivot table to Excel";
        //    pushButtonData3.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddSharedParametersToFamily;component/Resources/Excel2.png"));
        //    #endregion
        //    ///
        //    #region fourth button
        //    String pathDll4 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ExportExcel.dll";
        //    PushButtonData pushButtonData4 = new PushButtonData("But4", "Export parameteres", pathDll4, "Exc.ExportExcel");
        //    pushButtonData4.ToolTip = "Export parameteres to Excel";
        //    pushButtonData4.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddSharedParametersToFamily;component/Resources/Excel3.png"));
        //    #endregion
        //    ////
        //    SplitButtonData sb1 = new SplitButtonData("splitButton1", "Split");
        //    SplitButton sb = ribbonPanel2.AddItem(sb1) as SplitButton;
        //    sb.AddPushButton(pushButtonData2);
        //    sb.AddPushButton(pushButtonData3);
        //    sb.AddPushButton(pushButtonData4);

        //    #region sixth button
        //    var ribbonPanel3 = application.CreateRibbonPanel("CISP", "Abs");
        //    String pathDll5 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ABS_point.dll";
        //    PushButtonData ButtonData5 = new PushButtonData("Laksan4", "ABS", pathDll4, "ABS_point.Set_ABS");
        //    PushButton pushButton5 = ribbonPanel3.AddItem(ButtonData5) as PushButton;
        //    pushButton5.Enabled = true;
        //    pushButton5.ToolTip = "To all families with the name \u0022Отверстие_Стена\u0022 \r\nsets the absolute height to the parameer \r\n\u0022О_Отметка расположения\u0022";
        //    pushButton5.LargeImage = new BitmapImage(new Uri("pack://application:,,,/AddSharedParametersToFamily;component/Resources/Point.png"));
        //    ribbonPanel3.AddSeparator();
        //    #endregion

        //    return Result.Succeeded;
        //}
        //    public Result OnShutdown(UIControlledApplication uiApplication)
        //    {
        //        return Result.Succeeded;
        //    }
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

