using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExternalEventDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        //【1】注册外部事件
        CreateWall createWallCommand = null;
        ExternalEvent createWallEvent = null;

        CreateWallTwo createWallTwoCommand = null;
        ExternalEvent createWallTwoEvent = null;
        public MainWindow()
        {
            InitializeComponent();
            //【2】初始化
            //【2-1】第一个命令
            createWallCommand = new CreateWall();
            createWallEvent = ExternalEvent.Create(createWallCommand);
            //【2-2】第二个命令
            createWallTwoCommand = new CreateWallTwo();
            createWallTwoEvent = ExternalEvent.Create(createWallTwoCommand);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //【3】执行命令

            //【4】属性传值
            createWallCommand.WallHeight = Convert.ToDouble(this.textBox.Text);
            

            createWallEvent.Raise();
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            createWallTwoCommand.WallHeight = Convert.ToDouble(this.textBoxTwo.Text);
            createWallTwoEvent.Raise();
        }
    }
}
