﻿using System;
using System.Collections.Generic;
using System.Text;

namespace API_consume.EmployeeFiles
{
    public  class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
