using Recipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recipe.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();
        public ActionResult Index()
        {
            //ViewBag.Recipe = db.Recipe_Nutrition_Total(0,0,0).ToList().Take(3);
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Recipes()
        {
            var d = db.Recipe_Nutrition_Total(0, 0, 0).ToList();
            return View(d);
        }

        public ActionResult Single_Recipe(int RecipeID)
        {
            ViewBag.RecipeData = db.Recipe_Master.Where(r => r.RecipeID == RecipeID).ToList();
            ViewBag.Recipe_Ingredients = db.Recipe_Ingredients.Where(i => i.RecipeID == RecipeID).ToList();
            ViewBag.Recipe_Instructioins = db.Recipe_Instructions.Where(i => i.RecipeID == RecipeID).ToList();
            ViewBag.Ingredient_Master = db.Ingredient_Master.ToList();
            var d = db.Recipe_Nutrition_Total(RecipeID, 0, 0).ToList();
            return View(d);
        }

        public ActionResult Search(decimal? Cost, int? Serving)
        {
            //ViewBag.Recipe = db.Recipe_Master.Where(c => c.Cost == Cost && c.Servings == Serving).ToList();
            var d = db.Recipe_Nutrition_Total(0, Cost, Serving).ToList();
            return View(d);
        }

        public ActionResult UpdateCount(int RecipeID)
        {
            Recipe_Master result = db.Recipe_Master.Find(RecipeID);
            result.Views = (result.Views ?? 0) + 1;
            db.SaveChanges();
            return Json(result.Views, JsonRequestBehavior.AllowGet);
        }
    }
}