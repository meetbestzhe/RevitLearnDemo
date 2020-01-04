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

namespace RevitWpfDemo
{
    [Transaction(TransactionMode.Manual)]
    public class CreateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //实例化主窗口类
            MainWindow wpf = new MainWindow();
            //以模态形式显示窗体
            wpf.ShowDialog();
            //wpf.Show;//非模态

            //设置退出事件的响应
            if (!wpf.IsClosed==true)
            {
                return Result.Cancelled;
            }

            double height = Convert.ToDouble(wpf.textBox.Text) / 0.3048;
            

            //【2】获取CW 102-50-100墙的族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(WallType))
                         .FirstOrDefault(x => x.Name == "CW 102-50-100p");

            WallType wallType = ele as WallType;
            
            //【3】获取标高
            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;

            //【4】创建线
            XYZ start = new XYZ(0, 0, 0);
            XYZ end = new XYZ(10, 10, 0);
            Line geomLine = Line.CreateBound(start, end);


            XYZ ceshiPoint = new XYZ(0, 0, 0);

            //无连接高度
            //double height = 15 / 0.3048;
            double offset = 0;
            //【5】创建事务
            Transaction trans = new Transaction(doc, "创建CW 102-50-100p墙");
            trans.Start();
            Wall wall = Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);



            trans.Commit();
            return Result.Succeeded;
        }
    }
}
