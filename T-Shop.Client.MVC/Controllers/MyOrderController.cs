﻿using Microsoft.AspNetCore.Mvc;

namespace T_Shop.Client.MVC.Controllers;
public class MyOrderController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}