using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaleStockAPI;
using SaleStockAPI.Controllers;
using SaleStockAPI.Models;
using System.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace SaleStockAPI.Tests.Controllers
{
    [TestClass]
    public class ProductControllerTest
    {
        //get all product
        [TestMethod]
        public void GetAllProduct()
        {
            ProductController controller = new ProductController();

            String JsonResult = controller.GetAllProduct();

            // Result already get in Json.. The Result include the category. it should can display data grouping by category

        }

        //filter product
        [TestMethod]
        public void GetProductFilter()
        {
            // Arrange
            ProductController controller = new ProductController();

            // Act
            String jsonResult = controller.GetProductFilter("price", "2000", "3000");

        }

        //add new product
        [TestMethod]
        public void InsertNewProduct()
        {
            // Arrange
            ProductController controller = new ProductController();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9853/Product/InsertNewProduct");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"ProductCode\": \"AK48\",\"ProductName\": \"Jacket\",\"ProductSize\": \"XL\",\"ProductColor\": \"Blue\",\"ProductPrice\": \"50000\",\"ProductCategoryID\": \"1\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            
        }

        //delete product by ID
        [TestMethod]
        public void DeleteProduct()
        {
            // Arrange
            ProductController controller = new ProductController();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9853/Product/DeleteProduct");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"id\": \"10\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
