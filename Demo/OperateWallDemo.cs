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
    public class OperateWallDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //【2】获取当前墙

            Wall wall= new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall)).FirstOrDefault(x => x.Name == "CW 102-50-100p") as Wall;

            //【3】获取墙的属性
            //【3-1】获取墙的高度
            //double wallHeight = 0;

            //wallHeight=wall.LookupParameter("无连接高度").AsDouble()*0.3048;
            ////整型
            //int roomBand = wall.LookupParameter("房间边界").AsInteger();
            ////string类型
            //string beiZhu=wall.LookupParameter("备注").AsString();
            //TaskDialog.Show("显示信息", $"墙的高度为{wallHeight},墙的房间边界值是{roomBand.ToString()},墙的备注是{beiZhu}");

            //【4】修改墙的属性
            Transaction trans = new Transaction(doc, "修改墙的高度");
            trans.Start();
            wall.LookupParameter("无连接高度").Set(30 / 0.3048);
            trans.Commit();
          
            double wallHeightChanged = wall.LookupParameter("无连接高度").AsDouble()*0.3048;
            TaskDialog.Show("显示信息", $"墙修后的高度为{wallHeightChanged}");

            return Result.Succeeded;
        }
    }
}
