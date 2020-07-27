using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.EmployeeFiles
{
    public interface IConsumeEmployeeAsync
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int employeeId);
        string CreateEmployee(string name, string password, bool isAdmint);
        string UpdateEmployee(int employeeId, string name, string password, bool isAdmin);
        string UpdateEmployeeName(int employeeId, string name);
        string UpdateEmployeePassword(int employeeId, string password);
        string UpdateEmployeeIsAdmin(int employeeId, bool isAdmin);
        string DeleteEmployee(int employeeId);
    }
}
