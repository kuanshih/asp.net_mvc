using prjMauiDemo.NewFolder2;
using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class AController : Controller
    {

        public ActionResult DemoFileUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DemoFileUpload(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\ASP.Net\slnMVCDemo\prjMVCDemo\images\001.jpg");
            return View();
        }
        static int count = 0;

        public  ActionResult showCount() //使用Static的方式，使資料有辦法指定到同一個記憶體位置，但這樣會有異地同步的問題。
        {
            count++;
            ViewBag.COUNT = count;
            return View();
        }

        public ActionResult showBySessionCount() //使用Session 完成存取資料的累加
        {
            int count = 0;
            if (Session["COUNT"] != null)
                count = (int)Session["COUNT"];
            count++;
            Session["COUNT"] = count;
            ViewBag.COUNT = count;
            return View();
        }
        public ActionResult showByCookieCount() //使用Cookie 完成存取資料的累加
        {
            //讀取資料
            int count = 0;
            HttpCookie cookie = Request.Cookies["kk"];
            if (cookie!= null)
                count = Convert.ToInt32(cookie.Value);
            //累加
            count++;
            //存取資料
            cookie = new HttpCookie("kk");
            cookie.Value = count.ToString();
            cookie.Expires = DateTime.Now.AddSeconds(10);
            Response.Cookies.Add(cookie);
            //放到ViewBag裡面
            ViewBag.COUNT = count;
            return View();
        }

        //驅動程式的網址: https://localhost:44338/A/sayHello //後面放 A (類別)/ sayHello (類別中的方法)
        public string sayHello()
        {
            return "Hello my first asp.Net project";
        }
        public string lotto()
        {
            return (new CLotto()).getNumber();
        }
        public string demoResponse() 
        {

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\ASP.Net\slnMVCDemo\8000.jpg");
            Response.End();
            return "";

        }
        public string demoRequest() //示範使用Requst 物件，在網址後面加上類別及方法後，再加上 ? 關鍵字
                                    //，後面則是pid = 0，就換根據輸入的pid value 顯示對應的return
                                    //http: https://localhost:44338/A/demoRequest?pid=1
        {
            string id = Request.QueryString["pid"];
            if (id == "0")
                return "Xbox 加入購物車";
            else if (id == "1")
                return "PS5 加入購物車";
            else if (id == "2")
                return "switch 加入購物車";
            else
                return "尚未有商品加入購物車";
        }
        public string demoParameter(int? pid)//另一種方式，是直接傳參數，int 後面加上? 這樣就能避免當沒有輸入參數時的錯誤
                                            //另外，在打網址時可以省略? 只要將(int? pid) 改為(int? id)就可以了 (https://localhost:44338/A/demoParameter/1)
        {
            if (pid == 1)
                return "XBox 加入購物車成功";
            return "找不到該產品資料";
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
        public string queryById(int? id)
        {
            if (id == null) //如果沒有輸入任何id
                return "沒有輸入id";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            conn.Open();

            string s ="沒有任何資料符合查詢條件";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = " select * from tCustomer where fid =" + id.ToString();
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                s = reader["fName"].ToString() +"/" + reader["fPhone"].ToString();
            }
            conn.Close();

            return s;
        }

        public ActionResult querytoView(int? id) //ActionResult 是一個網頁的資料型態
        {
            //ViewBag.kk = "還沒輸入任何查詢條件"; //ViewBag 一個暫時存放資料的地方
            //ViewBag.ii = "anonymous.jpg";
            
            if (id != null) 
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                conn.Open();

                string s = "沒有任何資料符合查詢條件";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " select * from tCustomer where fid =" + id.ToString();
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    CCustomer x = new CCustomer();
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    x.fName = reader["fName"].ToString();
                    ViewBag.kk = x;
                }
                conn.Close();
            }
            
            return View();
        }
        public ActionResult BindingtoView(int? id) //ActionResult 是一個網頁的資料型態
        {
            //ViewBag.kk = "還沒輸入任何查詢條件"; //ViewBag 一個暫時存放資料的地方
            //ViewBag.ii = "anonymous.jpg";
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = "Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                conn.Open();

                string s = "沒有任何資料符合查詢條件";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " select * from tCustomer where fid =" + id.ToString();
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fName = reader["fName"].ToString();
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                    x.fName = reader["fName"].ToString();
                    ViewBag.kk = x;
                }
                conn.Close();
            }

            return View(x);
        }

        public string testinginsert()//土法練功的測試，正是需要右鍵"建立單元測試"
        {
            CCustomer x = new CCustomer();
            x.fName = "marry";
            //x.fPhone = "0924534532";
            x.fEmail = "marry@gmail.com";
           // x.fAddress = "New Taipei";
            x.fPassword = "1234";
            (new CCustomerFactory()).create(x);
            return "新增資料成功";
        }
        public string testingdelete(int? id)
        {
            if (id != null)
                (new CCustomerFactory()).delete((int)id);//int 跟int? 不同 因此要轉型
            return "刪除資料成功";
        }
        public string testingupdate()
        {
            CCustomer x = new CCustomer();
            x.fId = 5;
            x.fName = "marry";
            x.fPhone = "0946452018";
            x.fEmail = "marry@gmail.com";
            // x.fAddress = "New Taipei";
            x.fPassword = "1234";
            (new CCustomerFactory()).update(x);
            return "修改資料成功";
        }
        public string testingquery()
        {
            return"目前客戶數 : " + (new CCustomerFactory()).queryall().Count().ToString() +"\n"+queryById(2);
        }
        public ActionResult demoForm()
        {
            ViewBag.ANS = "?";
            
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a =a; 
                ViewBag.b = b; 
                ViewBag.c = c;
                double d = b * b - 4 * a * c;
                d = Math.Sqrt(d);
                ViewBag.ANS = ((-b + d) / (2 * a)).ToString("0.0#") + "or" + ((-b - d) / (2 * a)).ToString("0.0#");
            }

            return View();
        }
    }
}