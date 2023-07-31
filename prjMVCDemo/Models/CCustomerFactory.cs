using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace prjMVCDemo.Models
{
    public class CCustomerFactory
    {
        public void delete(int fid)
        {
            string sql = "Delete from tCustomer Where fid=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fid));
            executeSql(sql, paras);
        }
        public void create(CCustomer p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            //第一段先寫sql 的指令句
            string sql = "insert into tCustomer (";
            if (!string.IsNullOrEmpty(p.fName)) //加上判斷句，防止有空值輸入
            sql += " fName, ";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone, ";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail, ";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress, ";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword,";

            //去掉最後一個","號
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",") //Trim() 去掉前後空白
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);

            sql += ")values(";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", p.fName));//放到parameter物件中
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", p.fPassword));
            }
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);
            sql += ")";

            //第二段寫ADO.NET的語句
            executeSql(sql, paras);
        }

        public void update(CCustomer p) //修改資料
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            //第一段先寫sql 的指令句
            string sql = "update tCustomer set";
            if (!string.IsNullOrEmpty(p.fName)) //加上判斷句，防止有空值輸入
            {
                sql += " fName=@K_FNAME, ";//重點:  fName 前面需要有一個空格
                paras.Add(new SqlParameter("K_FNAME", p.fName));//放到parameter物件中
            }
            if (!string.IsNullOrEmpty(p.fPhone)) 
            {
                sql += " fPhone=@K_FPHONE, ";
                paras.Add(new SqlParameter("K_FPHONE", p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fEmail)) 
            {
                sql += " fEmail=@K_FEMAIL, ";
                paras.Add(new SqlParameter("K_FEMAIL", p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fAddress)) 
            {
                sql += " fAddress=@K_FADDRESS, ";
                paras.Add(new SqlParameter("K_FADDRESS", p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword)) 
            {
                sql += " fPassword=@K_FPASSWORD, ";
                paras.Add(new SqlParameter("K_FPASSWORD", p.fPassword));
            }
            //去掉最後的逗點
            if (sql.Trim().Substring(sql.Trim().Length - 1, 1) == ",")
                sql = sql.Trim().Substring(0, sql.Trim().Length - 1);

            //加上where的判斷句
            sql +=" Where fid = @K_FID"; //where 前面需要有一個空格
            paras.Add(new SqlParameter("K_FID", (object)p.fId));
            executeSql(sql, paras);
        }
        public CCustomer queryById(int fid)
        {
            string sql = " select * from tCustomer Where fid=@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (object)fid));
            List<CCustomer> list =queryBySql(sql, paras);
            if (list.Count == 0)
                return null; 
            return list[0];
        }
        public List<CCustomer> queryall()
        {
            string sql = " select * from tCustomer";
            

            return queryBySql(sql, null);
        }

        private static List<CCustomer> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CCustomer> list = new List<CCustomer>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());//再放到cmd裡面，並且須將集合轉陣列 
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CCustomer x = new CCustomer();
                x.fName = reader["fName"].ToString();
                x.fPhone = reader["fPhone"].ToString();
                x.fEmail = reader["fEmail"].ToString();
                x.fAddress = reader["fAddress"].ToString();
                x.fPassword = reader["fPassword"].ToString();
                x.fId = (int)reader["fid"];
                list.Add(x);
            }
            conn.Close();
            return list;
        }

        private static void executeSql(string sql, List<SqlParameter> paras)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (paras != null)
                cmd.Parameters.AddRange(paras.ToArray());//再放到cmd裡面，並且須將集合轉陣列 
                cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<CCustomer> queryByKeyword(string keyword)
        {
            string sql = " select * from tCustomer Where fName Like @K_FKEYWORD";
            sql += " or fPhone like @K_FKEYWORD";
            sql += " or fEmail like @K_FKEYWORD";
            sql += " or fAddress like @K_FKEYWORD";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FKEYWORD", "%"+(object)keyword+"%"));
            return queryBySql(sql, paras);
        }

        public CCustomer queryByEmail(string email)
        {
            string sql = " select * from tCustomer Where fEmail=@K_EMAIL";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_EMAIL", (object)email));
            List<CCustomer> list = queryBySql(sql, paras);
            if (list.Count == 0)
                return null;
            return list[0];
        }
    }
}