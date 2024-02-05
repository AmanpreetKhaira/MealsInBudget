using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recipe.Models;
using System.IO;
using DurableTask.Core.Common;

namespace Recipe.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private Entities db = new Entities();
        // GET: Dashboard

        #region Ingredients
        public ActionResult Ingredients()
        {
            var a = db.Ingredient_Master.ToList();
            return View(a);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Ingredients([Bind(Include = "IngredientID,IngredientName,Rate,Unit")] Ingredient_Master im)
        {
            if (im.IngredientID == 0)
            {
                db.Ingredient_Master.Add(im);
                db.SaveChanges();
            }
            else
            {
                Ingredient_Master edit = db.Ingredient_Master.Find(im.IngredientID);

                edit.IngredientName = im.IngredientName;
                edit.Rate = im.Rate;
                edit.Unit = im.Unit;

                db.SaveChanges();
            }
            return RedirectToAction("Ingredients");
        }
        #endregion

        #region Recipe
        public ActionResult Recipe()
        {
            ViewBag.Ingredient = db.Ingredient_Master.ToList();
            var b = db.Recipe_Nutrition_Total(0, 0, 0).ToList();
            return View(b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_New_Recipe([Bind(Include = "RecipeID,RecipeName,Description,PrepTime,TotalTime,Course,Cuisine,Servings,Author,Cost")] Recipe_Master rm, HttpPostedFileBase ImagePath)
        {
            if (ModelState.IsValid)
            {
                if (ImagePath != null && ImagePath.ContentLength > 0)
                {
                    string[] allowedFileTypes = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(ImagePath.FileName).ToLower();
                    if (!allowedFileTypes.Contains(fileExtension))
                    {
                        ModelState.AddModelError("File", "Only JPG or PNG files are allowed.");
                    }
                    //else if (ImagePath.ContentLength > 1024 * 1024) // 1MB limit
                    //{
                    //    ModelState.AddModelError("File", "File size must be less than 1MB.");
                    //}
                    else
                    {
                        string fileName = Path.GetFileName(ImagePath.FileName);
                        string filePath = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                        ImagePath.SaveAs(filePath);

                        // Update the ImagePath property
                        rm.ImagePath = "~/Content/Uploads/" + fileName;
                    }
                }

                if (rm.RecipeID == 0)
                {
                    db.Recipe_Master.Add(rm);
                }
                else
                {
                    Recipe_Master edit = db.Recipe_Master.Find(rm.RecipeID);
                    if (edit != null)
                    {
                        edit.RecipeName = rm.RecipeName;
                        edit.Description = rm.Description;
                        edit.PrepTime = rm.PrepTime;
                        edit.TotalTime = rm.TotalTime;
                        edit.Course = rm.Course;
                        edit.Cuisine = rm.Cuisine;
                        edit.Cost = rm.Cost;
                        edit.Servings = rm.Servings;
                        edit.Author = rm.Author;
                        edit.ImagePath = rm.ImagePath;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Recipe");
            }

            // If model state is not valid, return to the form with errors
            return View(rm);
        }

        [ValidateInput(false)]
        public ActionResult Add_Recipe_Instruction(int? InstructionID, int? RecipeID, string Instruction)
        {
            if (InstructionID == null)
            {
                Recipe_Instructions RI = new Recipe_Instructions();
                RI.RecipeID = RecipeID;
                RI.Instruction = Instruction;
                db.Recipe_Instructions.Add(RI);
                db.SaveChanges();
            }
            else
            {
                var rec = db.Recipe_Instructions.Find(InstructionID);
                rec.Instruction = Instruction;
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FetchInstruction(int InstructionID)
        {
            var instruction = db.Recipe_Instructions.Find(InstructionID);
            if (instruction != null)
            {
                return Json(instruction, JsonRequestBehavior.AllowGet);
            }
            return Json(null);
        }

        #endregion

        #region Recipe Ingredients
        [HttpPost]
        public ActionResult AddIngredient(Recipe_Ingredients ri)
        {
            db.Recipe_Ingredients.Add(ri);
            db.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Ingredients_List(int? RecipeID)
        {
            var items = db.Recipe_Ingredients.Where(p => p.RecipeID == RecipeID).ToList();
            return View(items);
        }
        public ActionResult Del_Ingredient(int ID)
        {
            Recipe_Ingredients result = db.Recipe_Ingredients.Find(ID);
            db.Recipe_Ingredients.Remove(result);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}