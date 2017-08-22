using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Areas.Admin.Models
{
    public class SliderModel
    {
        public int SlideID { get; set; }
        public HttpPostedFileBase Resim { get; set; }
        public string Baslik { get; set; }
        public string Fiyat { get; set; }
        public string Mesaj { get; set; }
        public Nullable<int> link { get; set; }
        public Nullable<bool> aktif { get; set; }
    }
}
