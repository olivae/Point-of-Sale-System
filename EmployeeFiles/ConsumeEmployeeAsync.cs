using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace API_consume.EmployeeFiles
{
    public class ConsumeEmployeeAsync : IConsumeEmployeeAsync
    {
        private string EmployeeAPIString = "http://45.79.193.33:5000/api/Employee";

        public IEnumerable<Employee> GetEmployees()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(EmployeeAPIString);
                var employee = JsonConvert.DeserializeObject<List<Employee>>(result);

                return employee;
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString(EmployeeAPIString + "/" + employeeId);
                var employee = JsonConvert.DeserializeObject<Employee>(result);

                return employee;
            }
        }

        public string CreateEmployee(string name, string password, bool isAdmin)
        {
            using (var client = new WebClient())
            {
                var employee = new PostEmployee();
                employee.Name = name;
                employee.Password = password;
                employee.IsAdmin = isAdmin;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(EmployeeAPIString, JsonConvert.SerializeObject(employee));
                return result;
            }
        }

        public string UpdateEmployee(int employeeId, string name, string password, bool isAdmin)
        {
            using (var client = new WebClient())
            {
                var employee = new PutEmployee();
                employee.Name = name;
                employee.Password = password;
                employee.IsAdmin = isAdmin;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(EmployeeAPIString + "/" + employeeId, "PUT", JsonConvert.SerializeObject(employee));
                return result;
            }
        }
        public string UpdateEmployeeName(int employeeId, string name)
        {
            using (var client = new WebClient())
            {
                var employeeInfo = GetEmployee(employeeId);
                var employee = new PutEmployee();
                employee.Name = name;
                employee.Password = employeeInfo.Password;
                employee.IsAdmin = employeeInfo.IsAdmin;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(EmployeeAPIString + "/" + employeeId, "PUT", JsonConvert.SerializeObject(employee));
                return result;
            }
        }
        public string UpdateEmployeePassword(int employeeId, string password)
        {
            using (var client = new WebClient())
            {
                var employeeInfo = GetEmployee(employeeId);
                var employee = new PutEmployee();
                employee.Name = employeeInfo.Name;
                employee.Password = password;
                employee.IsAdmin = employeeInfo.IsAdmin;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(EmployeeAPIString + "/" + employeeId, "PUT", JsonConvert.SerializeObject(employee));
                return result;
            }
        }
        public string UpdateEmployeeIsAdmin(int employeeId, bool isAdmin)
        {
            using (var client = new WebClient())
            {
                var employeeInfo = GetEmployee(employeeId);
                var employee = new PutEmployee();
                employee.Name = employeeInfo.Name;
                employee.Password = employeeInfo.Password;
                employee.IsAdmin = isAdmin;
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                var result = client.UploadString(EmployeeAPIString + "/" + employeeId, "PUT", JsonConvert.SerializeObject(employee));
                return result;
            }
        }

        public string DeleteEmployee(int employeeId)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type:application/json");
                client.Headers.Add("Accept:application/json");
                client.UploadString(EmployeeAPIString + "/" + employeeId, "DELETE", "");
                return ($"Customer {employeeId} has been deleted");
            }
        }
    }
}
