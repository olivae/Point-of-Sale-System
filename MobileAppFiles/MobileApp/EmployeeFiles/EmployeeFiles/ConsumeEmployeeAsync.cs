using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
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

                foreach (var emp in employee)
                {
                    emp.Password = Decrypt(emp.Password);
                }
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

               employee.Password = Decrypt(employee.Password); 

                return employee;
            }
        }

        public string CreateEmployee(string name, string password, bool isAdmin)
        {
            var EnryptedString = Encrypt(password);
            using (var client = new WebClient())
            {
                var employee = new PostEmployee();
                employee.Name = name;
                employee.Password = EnryptedString;
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


        private string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        private string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                cipherText = cipherText.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
                    {
                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            } catch (Exception e)
            { }
            return cipherText;
        }

    }
}
