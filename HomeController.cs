using SepetSon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SepetSon.Controllers
{
    public class HomeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View(db.Products.ToList());
        }

        public ActionResult SepeteEkle(int id)
        {
            Product urun = db.Products.Where(p => p.ProductID == id).FirstOrDefault();

            //Sepet var mı? kontrolü
            List<SepetUrun> mevcutListe;
            if (Session["sepet"] == null)
            {
                mevcutListe = new List<SepetUrun>();
            }
            else
            {
                mevcutListe = (List<SepetUrun>)Session["sepet"];
            }
            //Ürünü sepete ekle
            bool sepetteVarmi = mevcutListe.Count > 0 && mevcutListe.FirstOrDefault(su => su.Id == id) != null;

            if (sepetteVarmi)
            {
                SepetUrun varolan = mevcutListe.FirstOrDefault(su => su.Id == id);
                varolan.Adet++;

            }
            else
            {
                SepetUrun yeni = new SepetUrun();
                yeni.Ad = urun.ProductName;
                yeni.Id = urun.ProductID;
                yeni.Fiyat = Convert.ToDecimal(urun.UnitPrice);
                yeni.Adet = 1;
                yeni.Indirim = 0;

                mevcutListe.Add(yeni);
            }

            Session["sepet"] = mevcutListe;


            return View(mevcutListe);
        }
	}
}