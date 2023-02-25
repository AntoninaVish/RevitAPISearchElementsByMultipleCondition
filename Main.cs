using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPISearchElementsByMultipleCondition
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            //собираем все экземпляры семейств окон без их типов, FamilyInstance -это экземпляр семейства
            ElementCategoryFilter windowsCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Windows);// по категории
            ElementClassFilter windowsInstancesFilter = new ElementClassFilter(typeof(FamilyInstance));// по экземпляру семейства

            //LogicalAndFilter выполнять поиск элементов по двум условиям одновременно
            LogicalAndFilter windowsFilter = new LogicalAndFilter(windowsCategoryFilter, windowsInstancesFilter);

            //ищем окна в текущем документе
            var windows = new FilteredElementCollector(doc)
                .WherePasses(windowsFilter) //указываем путь где проходить(искать)
                .Cast<FamilyInstance>() // Cast - забрасывает в список
                .ToList();

            TaskDialog.Show("Windows info" , windows.Count.ToString());//выводим инфор.окон, посчетать окна, в cтроку

            return Result.Succeeded;
        }
    }
}
