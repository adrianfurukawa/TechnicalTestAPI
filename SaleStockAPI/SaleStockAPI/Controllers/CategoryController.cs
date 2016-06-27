using SaleStockAPI.DAL;
using SaleStockAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SaleStockAPI.Controllers
{
    public class CategoryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<MsCategory> GetAllCategory()
        {
            List<MsCategory> categories = unitOfWork.CategoryRepository.Get().ToList();
            return categories;
        }

        public List<MsCategory> GetCategoryByID(string id)
        {
            List<MsCategory> categories = new List<MsCategory>();
            int value1int = Convert.ToInt32(id);
            categories = unitOfWork.CategoryRepository.Get(filter: d => d.ProductCategoryID == value1int).ToList();
            return categories;
        }

        [HttpPost]
        public int InsertNewCategory(MsCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.CategoryRepository.Insert(category);
                    unitOfWork.Save();
                    return 1;
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return 0;
            }
            return 0;
        }

        [HttpPost]
        public int DeleteCategory(string id)
        {
            try
            {
                List<MsCategory> category = GetCategoryByID(id);
                if (category.Count > 0)
                {
                    int idint = Convert.ToInt32(id);
                    unitOfWork.CategoryRepository.Delete(idint);
                    unitOfWork.Save();
                    return 1;
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to Delete changes. Try again, and if the problem persists, see your system administrator.");
                return 0;
            }
            return 0;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
