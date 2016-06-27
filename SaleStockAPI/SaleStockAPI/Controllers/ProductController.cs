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
    public class ProductController : Controller
    {
        //============================================
        //see ProductControllerTest for implementation
        //============================================

        private UnitOfWork unitOfWork = new UnitOfWork();

        //getting all product
        //  http://localhost:xxxx/Product/GetAllProduct

        public String GetAllProduct()
        {
            List<MsProduct> products = unitOfWork.ProductRepository.Get().ToList();
            String productsJson = JsonConvert.SerializeObject(products,Formatting.Indented);
            return productsJson;
        }

        //getting product by filtering
        //  Using Postman
        // url : http://localhost:xxxx/Product/GetProductFilter
        // Method: POST
        // Example Param (color can be changed to price, productid, size, etc) : 
        //    {
        //      "filter": "color",
        //      "value1": "Blue",
        //      "value2": ""
        //    }
       
    public String GetProductFilter(string filter, string value1, string value2)
        {
            List<MsProduct> products=new List<MsProduct>();

            //filtering price range
            if (filter == "price")
            {
                int value1int = Convert.ToInt32(value1);
                int value2int = Convert.ToInt32(value2);
                products = unitOfWork.ProductRepository.Get(filter: d => d.ProductPrice > value1int && d.ProductPrice < value2int).ToList();
            }
            //getting product based on productID
            else if (filter == "productid")
            {
                int value1int = Convert.ToInt32(value1);
                products = unitOfWork.ProductRepository.Get(filter: d => d.ProductID == value1int).ToList();
            }
            //filter product by size
            else if (filter == "size")
            {
                products = unitOfWork.ProductRepository.Get(filter: d => d.ProductSize == value1).ToList();
            }
            //filter product by category. Used for get all product, with grouping by category. implementation in Tests
            else if (filter == "category")
            {
                int value1int = Convert.ToInt32(value1);
                products = unitOfWork.ProductRepository.Get(filter: d => d.ProductCategoryID == value1int).ToList();
            }
             //filter by color
            else if (filter == "color")
            {
                products = unitOfWork.ProductRepository.Get(filter: d => d.ProductColor == value1).ToList();
            }
            else 
            {
                products = null;
            }

            if (products != null)
            {
                String productsJson = JsonConvert.SerializeObject(products, Formatting.Indented);
                return productsJson;
            }
            else
                return "Product Not Found";
        }

        //insert new product
        //Using Postman
        // URL : http://localhost:xxxx/Product/InsertNewProduct
        // Method: POST
        // Param: 
        //      {
        //    "ProductCode": "AK4866",
        //    "ProductName": "Jacket",
        //    "ProductSize": "XL",
        //    "ProductColor": "Blue",
        //    "ProductPrice": "50000",
        //    "ProductCategoryID": "1"
        //      }
        [HttpPost]
        public int InsertNewProduct(MsProduct product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.ProductRepository.Insert(product);
                    unitOfWork.Save();
                    return 1;
                }
            }
            catch (DataException /* dex */)
            {
                return 0;
            }
            return 0;
        }

        //delete product
        //Method: POST
        // URL : http://localhost:xxxx/Product/DeleteProduct
        // Param: 
        //      {
        //      "id": "15"
        //      }

        // success result =1 
        [HttpPost]
        public int DeleteProduct(string id)
        {
            try
            {
                //check if product found
                String product = GetProductFilter("productid",id,"");

                // if not found, it will return [], so length will be 2 []
                if (product.Length>2)
                {
                    int idint = Convert.ToInt32(id);
                    unitOfWork.ProductRepository.Delete(idint);
                    unitOfWork.Save();
                    return 1;
                }
            }
            catch (DataException /* dex */)
            {
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
