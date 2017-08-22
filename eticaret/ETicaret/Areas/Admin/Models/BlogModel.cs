using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Areas.Admin.Models
{
    public class BlogModel
    {
        public int BlogID { get; set; }
        public Nullable<int> UrunID { get; set; }
        public string Baslik { get; set; }
        public string Metin { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public HttpPostedFileBase Resim { get; set; }

        public virtual Urunler Urunler { get; set; }

    }


}
