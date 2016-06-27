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
    public class CategoryControllerTest
    {
        [TestMethod]
        public void GetAllCategory()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            // Act
            foreach (var item in controller.GetAllCategory())
            {
                Console.WriteLine("{0}",item.ProductCategoryName);
            }
        }

        [TestMethod]
        public void GetCategoryByID()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            // Act
            foreach (var item in controller.GetCategoryByID("1"))
            {
                Console.WriteLine("{0} {1}", item.ProductCategoryID, item.ProductCategoryName);
            }
        }

        [TestMethod]
        public void InsertNewCategory()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9853/Category/InsertNewCategory");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"ProductCategoryName\": \"Ball\"}";
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

        [TestMethod]
        public void DeleteCategory()
        {
            // Arrange
            CategoryController controller = new CategoryController();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:9853/Category/DeleteCategory");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"id\": \"1\"}";

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
