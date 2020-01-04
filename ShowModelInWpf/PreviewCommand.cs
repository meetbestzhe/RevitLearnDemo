using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


/// <summary>
/// 本代码由黑夜de骑士编写
/// 对应教学视频：b站搜索：面向工程人员的revit二次开发
/// qq交流群：711844216
/// 个人博客: https://blog.csdn.net/birdfly2015
/// contact me:1056291511@qq.com
/// </summary>

namespace ShowModelInWpf
{
    [Transaction(TransactionMode.Manual)]
    public class PreviewCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            MainWindow wpf = new MainWindow();
            //【1】preview控件
            PreviewControl pc = new PreviewControl(doc, commandData.Application.ActiveUIDocument.ActiveGraphicalView.Id);
            wpf.MainGrid.Children.Add(pc);

            wpf.ShowDialog();


            return Result.Succeeded;
        }
    }
}
