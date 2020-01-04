using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


/// <summary>
/// 本代码由黑夜de骑士编写
/// 对应教学视频：b站搜索：面向工程人员的revit二次开发
/// qq交流群：711844216
/// 个人博客: https://blog.csdn.net/birdfly2015
/// contact me:1056291511@qq.com
/// </summary>

namespace FamilyInstanceDemo
{
    [Transaction(TransactionMode.Manual)]
    public class JustForFun : IExternalCommand
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; } //isReadonly=true
            set { myVar = value; } //isReadonly=false;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //【2】获取族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Columns).OfClass(typeof(FamilySymbol))
                          .FirstOrDefault(x => x.Name == "457 x 475mm");
            FamilySymbol columnType = ele as FamilySymbol;

            columnType.Activate();

           //【3】获取标高
           //通过链式编程，一句话搞定
           Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;

            //【4】创建放置点

            List<XYZ> xyzList = new List<XYZ>();
            for (int i = 0; i < 72; i++)
            {
                double x = 10 * (2 * Math.Cos(2 * Math.PI / 72 * i)-Math.Cos(2* 2 * Math.PI / 72 * i));
                double y = 10 * (2 * Math.Sin(2 * Math.PI / 72 * i) - Math.Sin(2 * 2 * Math.PI / 72 * i));
                XYZ start = new XYZ(x, y, 0);
                xyzList.Add(start);
            }
        

          
            //无连接高度 英尺进入 然后15/0.3048,变为实际的米4.572
            double height = 15 / 0.3048;
            double offset = 0;

            //【5】创建事务
            List<FamilyInstance> familyInstances = new List<FamilyInstance>();
            Transaction trans = new Transaction(doc, "创建柱子");

            foreach (XYZ item in xyzList)
            {  
                trans.Start();
                FamilyInstance column = doc.Create.NewFamilyInstance(item, columnType, level, StructuralType.NonStructural);
                trans.Commit();
                //刷新界面，出现动态效果
                //System.Windows.Forms.Application.DoEvents();
                ////如果想控制时间，可以使用线程来控制
                //Thread.Sleep(100);
                familyInstances.Add(column);
            }

            Transaction transRotate = new Transaction(doc, "旋转柱子");
            for (int k = 0; k < 100; k++)
            {
                transRotate.Start();
                for (int i = 0; i < xyzList.Count; i++)
                {
                    Line line = Line.CreateBound(xyzList[i], new XYZ(xyzList[i].X, xyzList[i].Y, 10));
                    ElementTransformUtils.RotateElement(doc, familyInstances[i].Id, line, Math.PI / 6.0);
                }
                transRotate.Commit();

                System.Windows.Forms.Application.DoEvents();
            }



            return Result.Succeeded;
        }
    }
}
