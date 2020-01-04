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
    class OperateWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //Wall wall = doc.GetElement(new ElementId(211845)) as Wall;
            //【2】获取当前墙
            Wall wall = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall)).FirstOrDefault(x => x.Name == "CW 102-50-100p") as Wall;
            //【3】获取这个墙的属性
            //【3-1】确定获取的参数
            double wallHeight = 0;
            //【3-2】确定获取参数的类型，看storeType 
            wallHeight = wall.LookupParameter("无连接高度").AsDouble()*0.3048;

            TaskDialog.Show("输出信息", $"当前墙的高度为：{wallHeight}");
            //【4】修改墙的属性
            Transaction trans = new Transaction(doc,"修改墙高");
            trans.Start();
            //修改高度
            wall.LookupParameter("无连接高度").Set(30 / 0.3048);
            //wall.LookupParameter("面积").Set(1500);
            trans.Commit();

            double wallHeightChanged = wall.LookupParameter("无连接高度").AsDouble()*0.3048;

            TaskDialog.Show("输出信息", $"墙修改之后的高度为{wallHeightChanged}");


            return Result.Succeeded;
        }
    }
}
