using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VStudioCode.Models;
using System.Text.Encodings.Web;

namespace VStudioCode.Controllers;

public class TestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public string Welcome()
    {
        return "Test Result for test purposes";
    }

    public string WelcomeName(string name, string lastname)
    {
        string fullName = name + " " + lastname;
        return HtmlEncoder.Default.Encode($"Hello! Wish you a good day {fullName}");
    }
}
