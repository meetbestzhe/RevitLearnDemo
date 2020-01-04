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

namespace ExternalEventDemo
{
    class CreateWallTwo : IExternalEventHandler
    {
        public double WallHeight { get; set; }

        public void Execute(UIApplication app)
        {
            //【1】获取当前文档
            //Document doc = commandData.Application.ActiveUIDocument.Document;
            Document doc = app.ActiveUIDocument.Document;
            double height = WallHeight / 0.3048;

            //【2】获取CW 102-50-100墙的族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(WallType))
                         .FirstOrDefault(x => x.Name == "CW 102-50-100p");

            WallType wallType = ele as WallType;

            //【3】获取标高
            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;

            //【4】创建线
            XYZ start = new XYZ(20, 20,0);
            XYZ end = new XYZ(30, 30, 0);
            Line geomLine = Line.CreateBound(start, end);


            XYZ ceshiPoint = new XYZ(0, 0, 0);

            //无连接高度
            // double height = 15 / 0.3048;

            double offset = 0;
            //【5】创建事务
            Transaction trans = new Transaction(doc, "创建CW 102-50-100p墙");
            trans.Start();
            Wall wall = Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);



            trans.Commit();


        }

        public string GetName()
        {
            return "CreateWall";
        }
    }
}
