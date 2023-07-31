using prjMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer

        //從流程開始設計->寫方法 ->使用強行別 ->Create View
        public ActionResult List()
        {

            string keyword = Request.Form["txtKeyword"];
            List<CCustomer> datas = null;
            if (string.IsNullOrEmpty(keyword))
                datas = (new CCustomerFactory()).queryall();
            else
                datas = (new CCustomerFactory()).queryByKeyword(keyword);
            return View(datas);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save()
        {
            CCustomer x = new CCustomer();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).create(x);
            return RedirectToAction("List"); //回到指定的Action中(對應的頁面)
        }
        public ActionResult Delete(int? id)
        {
            (new CCustomerFactory()).delete((int)id);
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            CCustomer x = (new CCustomerFactory()).queryById((int)id);
            return View(x);
        }
        [HttpPost]//Action 不同狀態的的寫法，這樣兩個同樣叫Edit的Action，在運作時就知道要先呼叫哪一個 
        public ActionResult Edit(CCustomer x) //新的寫法的兩大重點: 1. 在View 中的name 名稱要等於CCustomer中的名稱 2. 要將類別物件變成參數
        {
            //舊的寫法太過重複
            //CCustomer x = new CCustomer();
            //x.fId = Convert.ToInt32(Request.Form["fId"]);
            //x.fName = Request.Form["txtName"];
            //x.fPhone = Request.Form["txtPhone"];
            //x.fEmail = Request.Form["txtEmail"];
            //x.fAddress = Request.Form["txtAddress"];
            //x.fPassword = Request.Form["txtPassword"];
            (new CCustomerFactory()).update(x);
            return RedirectToAction("List");
        }
    }
}