﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserService
    {
        string hello();
        ReusltApiMsg GetUsers();
        ReusltApiMsg GetUser(string name, string password);
    }
}
