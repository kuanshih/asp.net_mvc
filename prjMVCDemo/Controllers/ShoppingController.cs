using prjMVCDemo.Models;
using prjMVCDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMVCDemo.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult List()
        {
            dbDemoEntities2 db = new dbDemoEntities2();
            var datas = from p in db.tProduct
                        select p;
            return View(datas);
        }
        public ActionResult AddToCart(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(CAddToCartViewModel vm) 
        { 
            dbDemoEntities2 db = new dbDemoEntities2 ();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtfId);
            if (prod == null)
                return RedirectToAction("List");
            tShopingCart x = new tShopingCart ();
            x.fCount = vm.txtCount;
            x.fCustomerId = 1;
            x.fDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            x.fProductId = vm.txtfId;
            x.fPrice = prod.fPrice;
            db.tShopingCart.Add(x);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult AddToSession(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddToSession(CAddToCartViewModel vm)
        {
            dbDemoEntities2 db = new dbDemoEntities2();
            tProduct prod = db.tProduct.FirstOrDefault(p => p.fId == vm.txtfId);
            if (prod == null)
                return RedirectToAction("List");
          
            List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;
            if(cart == null)
            {
                cart = new List<CShoppingCartItem>();
                Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] = cart;
            }
            CShoppingCartItem item = new CShoppingCartItem();
            item.price = (decimal)prod.fPrice;
            item.count = vm.txtCount;
            item.productId = vm.txtfId;
            item.product = prod; //在CShoppingCartItem裡面建一個 tProduct屬性，這邊可以就可以把上面的prod放到屬性中，這樣view就可以點tProduct裡面的欄位
            cart.Add(item);
            return RedirectToAction("List");
        }
        public ActionResult CartView()
        {
            List<CShoppingCartItem> cart =
                Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List <CShoppingCartItem>;
            if (cart == null)
            {
                return RedirectToAction("List");
                
            }
            return View(cart);
        }
    }
}