using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tProduct x)
        {
            dbDemoEntities2 en = new dbDemoEntities2();
            en.tProduct.Add(x);
            en.SaveChanges();
            return RedirectToAction("List");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                dbDemoEntities2 en = new dbDemoEntities2();
                tProduct pro = en.tProduct.FirstOrDefault(p => p.fId == id);
                if (pro != null)
                {
                    en.tProduct.Remove(pro);
                    en.SaveChanges();
                }

            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            dbDemoEntities2 en = new dbDemoEntities2();
            tProduct pro = en.tProduct.FirstOrDefault(p => p.fId == id);
            if (pro == null)
                return RedirectToAction("List");
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(tProduct pln)
        {
            dbDemoEntities2 en = new dbDemoEntities2();
            tProduct pDb = en.tProduct.FirstOrDefault(p => p.fId == pln.fId);
            if (pDb != null)
            {
                pDb.fQty = pln.fQty;
                pDb.fName = pln.fName;
                pDb.fCost = pln.fCost;
                pDb.fPrice = pln.fPrice;
                en.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public ActionResult List()
        {
            string keyword = Request.Form["txtKeyword"];
            IEnumerable<tProduct> datas= null;
            dbDemoEntities2 x = new dbDemoEntities2();
            if (string.IsNullOrEmpty(keyword))
            {
               datas = from p in x.tProduct
                            select p;
            }
            else
                datas =x.tProduct.Where(p=>p.fName.Contains(keyword));
            return View(datas);
        }
    }
}