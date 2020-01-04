using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    public class CreateWallDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {   
            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //【2】获取  CW 102-50-100p 类型的墙
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(WallType))
                         .FirstOrDefault(x => x.Name == "CW 102-50-100p");
            WallType wallType = ele as WallType;

            //【3】获取标高
            //链式编程
            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;

            //【4】创建线
            XYZ start = new XYZ(0, 0, 0);
            XYZ end = new XYZ(10, 10, 0);
            XYZ ceshiPoint = new XYZ(0, 0, 10);
            Line geomline = Line.CreateBound(start, end);

            //【5】墙的高度
            double height = 15/0.3048;
            double offset = 0;

            //【6】创建墙
            // Wall wall = Wall.Create(doc, geomline, wallType.Id, level.Id, height, offset, false, false);
            //【7】事务的使用
            Transaction trans = new Transaction(doc,"创建墙");
            trans.Start();

            //第一个墙
            Wall wall = Wall.Create(doc, geomline, wallType.Id, level.Id, height, offset, false, false);

            //第二个墙
            //Line geomlineCeshi = Line.CreateBound(start, ceshiPoint);

            //Wall wallCeshi = Wall.Create(doc, geomlineCeshi, wallType.Id, level.Id, height, offset, false, false);
            trans.Commit();

            return Result.Succeeded;
        }
    }
}
