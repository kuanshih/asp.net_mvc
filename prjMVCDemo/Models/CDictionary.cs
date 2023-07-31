using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CDictionary
    { 
        //使用CDictionary幫助我們管理Session的Key，這樣我們不用怕打錯，都用點的就可以了
        public static readonly string SK_PURCHASED_PRODUCTS_LIST = "SK_PURCHASED_PRODUCTS_LIST";
        public static readonly string SK_LOGINED_USER= "SK_LOGINED_USER";
    }
}