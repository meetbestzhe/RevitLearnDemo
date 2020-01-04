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
    public class FilterWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //界面交互的doc
            UIDocument uiDoc = commandData.Application.ActiveUIDocument;
            //实际内容的doc
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //【1】创建收集器
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            //【2】过滤，获取墙元素
            //【2-1】快速过滤方法
           // collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall));
          
            //Element
            //ArePhasesModifiable
            //CanBeHidden
            //CanBeLocked


            //【2-2】通用方法
            //ElementCategoryFilter elementCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            //ElementClassFilter elementClassFilter = new ElementClassFilter(typeof(Wall));
            //collector.WherePasses(elementClassFilter).WherePasses(elementClassFilter);



            //【3】某种墙族类型下族实例的获取
            //或者大家可以通过给实例打自己的属性进行筛选，或者根据一些自带属性，
            //这个时候，需要用到lookup属性，在下一节中，族实例的创建中，进行讲解
            //【3-1】foreach获取
            List<Element> elementList = new List<Element>();
            foreach (var item in collector)
            {
                if (item.Name== "CL_W1")
                {
                    elementList.Add(item);
                }
            }
            List<Element> elementlist = collector.ToList<Element>();
            //【3-2】lingq表达式获取
            //var wallElement = from element in collector
            //                  where element.Name == "CL_W1"
            //                  select element;
            //【3-3】转为list获取
            //List<Element> elementList = wallElement.ToList<Element>();


            //【4】某个族实例的获取
            //【4-1】确定只有一个实例
            //【4-1-1】list获取
            //Element ele = elementList[0];
            //【4-1-2】IEnumberable获取
            //Element ele = wallElement.FirstOrDefault<Element>();
            //【4-1-3】lambda表达式的一种写法
            //Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall)).FirstOrDefault<Element>(x => x.Name == "CL_W1");
            //【4-2】有多个实例，但是只想获取其中一个，可以使用ElementId,或者根据一些特征
            Element ele = doc.GetElement(new ElementId(493697));
            //【5】类型判断与转换

            foreach (var item in elementList)
            {
                //先判断
                if (item is Wall)
                {
                    //再转换
                    Wall wall = item as Wall;
                    Wall wallTwo = (Wall)item;
                } 
            }
            
         
            //【6】高亮显示实例
            var sel = uiDoc.Selection.GetElementIds();

            foreach (var item in elementList)
            {
                TaskDialog.Show("查看结果", item.Name);
                sel.Add(item.Id);
            }
            //TaskDialog.Show("查看结果", ele.Name);
            //sel.Add(ele.Id);
            uiDoc.Selection.SetElementIds(sel);
            return Result.Succeeded;

        }
    }
}
