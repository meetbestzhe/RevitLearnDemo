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

namespace ExternalEventNormal
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //注册外部事件
        CreateWall createWallCommand = null;
        ExternalEvent createWallHander = null;

        public MainWindow()
        {
            InitializeComponent();
            createWallCommand = new CreateWall();
            createWallHander = ExternalEvent.Create(createWallCommand);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createWallHander.Raise();
        }
    }
}
