using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


namespace ReadAndWrite
{
    [Transaction(TransactionMode.Manual)]
    public class CreateWallDemo : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //类作为数据的载体，在整个流程中，传递数据
            #region 从sqlserver中获取数据
            //【1】编写连接字符串与sql语句

            string connString = @"Server=.;DataBase=WallDB;Uid=sa;Pwd=hwz1234";

            string sql = $"select * from WallCreate where WallId=2";

            //【2】建立连接

            SqlConnection conn = new SqlConnection(connString);

            //【3】打开连接

            conn.Open();

            //【4】执行命令

            SqlCommand cmd = new SqlCommand(sql, conn);

            //【5】读取返回值
            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            WallCreate wallCreate = new WallCreate();

            if (sqlDataReader.Read())
            {
                wallCreate.WallId = Convert.ToInt32(sqlDataReader["WallId"]);
                wallCreate.WallHeight = Convert.ToDouble(sqlDataReader["WallHeight"]);
                wallCreate.StartPointX = Convert.ToDouble(sqlDataReader["StartPointX"]);
                wallCreate.StartPointY = Convert.ToDouble(sqlDataReader["StartPointY"]);
                wallCreate.StartPointZ = Convert.ToDouble(sqlDataReader["StartPointZ"]);
                wallCreate.EndPointX = Convert.ToDouble(sqlDataReader["EndPointX"]);
                wallCreate.EndPointY = Convert.ToDouble(sqlDataReader["EndPointY"]);
                wallCreate.EndPointZ = Convert.ToDouble(sqlDataReader["EndPointZ"]);
            }

            sqlDataReader.Close();

            conn.Close();


            #endregion


            //【1】获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;
            //目标
            // Wall wall = Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);
            //【2】获取CW 102-50-100墙的族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(WallType))
                         .FirstOrDefault(x => x.Name == "CW 102-50-100p");

            WallType wallType = ele as WallType;

            //【3】获取标高
            Level level = new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;

            //【4】创建线
            XYZ start = new XYZ(wallCreate.StartPointX, wallCreate.StartPointY, wallCreate.StartPointZ);
            XYZ end = new XYZ(wallCreate.EndPointX, wallCreate.EndPointY, wallCreate.EndPointZ);
            Line geomLine = Line.CreateBound(start, end);


            //无连接高度
            double height = wallCreate.WallHeight / 0.3048;
            double offset = 0;
            //【5】创建事务
            Transaction trans = new Transaction(doc, "创建CW 102-50-100p墙");
            trans.Start();
            Wall wall = Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);

            trans.Commit();


            WallInformation wallInformation = new WallInformation();
            wallInformation.WallId = wallCreate.WallId;
            wallInformation.Length=wall.LookupParameter("长度").AsDouble() * 0.3048;
            wallInformation.Area = wall.LookupParameter("面积").AsDouble() * 0.3048 * 0.3048;

            #region 写入数据库

            //【1】编写写入的sql语句
            string sqlInsert = $"insert into WallInformation (WallId,Length,Area) values ({wallInformation.WallId},{ wallInformation.Length},{wallInformation.Area})";

            //【2】建立连接
            SqlConnection connInsert = new SqlConnection(connString);
            //【3】打开连接
            connInsert.Open();

            //【4】执行命令
            SqlCommand cmdInsert = new SqlCommand(sqlInsert, connInsert);


            int result = cmdInsert.ExecuteNonQuery();

            //【5】读取返回值
            connInsert.Close();

            #endregion





            return Result.Succeeded;

        }
    }
}


////【1】编写连接字符串与sql语句

//string sqlInsert = $"insert into WallInformation (WallId,Length,Area) values ({wallInformation.WallId},{ wallInformation.Length},{wallInformation.Area})";

////【2】建立连接
//SqlConnection connInsert = new SqlConnection(connString);
////【3】打开连接
//connInsert.Open();

//            //【4】执行命令
//            SqlCommand cmdInsert = new SqlCommand(sqlInsert, connInsert);
//int result = cmdInsert.ExecuteNonQuery();

////【5】读取返回值
//connInsert.Close();