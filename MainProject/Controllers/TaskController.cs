﻿using Microsoft.AspNetCore.Mvc;

namespace MainProject.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
