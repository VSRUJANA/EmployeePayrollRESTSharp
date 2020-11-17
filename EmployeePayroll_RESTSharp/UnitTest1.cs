using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace EmployeePayroll_RESTSharp
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;
        [TestInitialize]
        public void setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse GetEmployeeList()
        {
            // Request the client by giving resource url and method to perform
            IRestRequest request = new RestRequest("/employees", Method.GET);

            // executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingAPI_RetrieveAllElementsFromJSONServer()
        {
            // Get the response
            IRestResponse response = GetEmployeeList();

            // Get all the elements into a list
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);

            // Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Assert.AreEqual(4, employeeList.Count);

            // Print all employees
            foreach (var item in employeeList)
            {
                System.Console.WriteLine("id: " + item.id + "\tName: " + item.name + "\tSalary: " + item.salary);
            }
        }
    }
}
