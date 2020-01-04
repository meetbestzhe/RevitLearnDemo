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

namespace UIButtonDemo
{
    [Transaction(TransactionMode.Manual)]
    public class HelloRevitDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //显示HelloWorld
            TaskDialog.Show("Demo", "你好呀，Revit");
            return Result.Succeeded;
        }
    }

}
