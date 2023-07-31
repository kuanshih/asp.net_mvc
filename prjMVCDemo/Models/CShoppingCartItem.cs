using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        [DisplayName("數量")]
        public int count { get; set; }
        [DisplayName("單價")]
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
        public tProduct product { get; set; }
    }
}