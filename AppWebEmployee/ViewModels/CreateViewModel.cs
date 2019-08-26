﻿using AppWebEmployee.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppWebEmployee.ViewModels
{
    public class CreateViewModel
    {
            public string Name { get; set; }
            public string Email { get; set; }
            public Dept Department { get; set; }
            public IFormFile Photo { get; set; }
        
    }
}
