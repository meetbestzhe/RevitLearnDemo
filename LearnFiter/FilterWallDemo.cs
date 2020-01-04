using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFiter
{
    [Transaction(TransactionMode.Manual)]
    class FilterWallDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //界面交互的doc
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            //实际内容的doc
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //【1】创建收集器
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            FilteredElementCollector collectorTwo = new FilteredElementCollector(doc);
            //【2】过滤，获取墙元素
            //【2-1】快速过滤方法
            collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall));//FaimilyInstance
            //【2-2】通用过滤方法
            //ElementCategoryFilter elementCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            //ElementClassFilter elementClassFilter = new ElementClassFilter(typeof(Wall));
            //collector.WherePasses(elementClassFilter).WherePasses(elementClassFilter);

            //【3】某种墙族类型下族实例的获取

            //【3-1】foreach获取
            List<Element> elementList = new List<Element>();

            foreach (var item in collector)
            {
                if (item.Name== "CL_W1")
                {
                    elementList.Add(item);
                }
            }


            //【3-2】转为list处理
        

            //【3-3】linq表达式

            var wallElement = from element in collector
                              where element.Name == "CL_W1"
                              select element;
            IEnumerable<Element> elementsList = wallElement;
            List<Element> elementListTwo = wallElement.ToList<Element>();
            //Element wallInstacne = wallElement.LastOrDefault<Element>();

            //【4】某个族实例的获取
            //【4-1】确定只有一个实例
            //【4-1-1】list获取
            //Element wallInstance = elementList[0];
            //【4-1-2】IEnumberable获取
            // Element wallInstance = wallElement.FirstOrDefault<Element>();
            //【4-1-3】lambda表达式的一种写法
            // Element wallInstance = collectorTwo.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall)).FirstOrDefault<Element>(y => y.Name == "CL_W1");
            //【4-2】有多个实例，但是只想获取其中一个，可以使用ElementId,或者根据一些特征
            Element wallInstance = doc.GetElement(new ElementId(243274));

            //【5】类型装换与判断




            //【6】高亮显示实例
            var sel = uiDoc.Selection.GetElementIds();

            //foreach (var item in elementList)
            //{
            //    //TaskDialog.Show("查看结果", item.Name);
            //    sel.Add(item.Id);
            //}
            TaskDialog.Show("查看结果", wallInstance.Name);
            sel.Add(wallInstance.Id);
            uiDoc.Selection.SetElementIds(sel);
            return Result.Succeeded;

        }
    }
}
