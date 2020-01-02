using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PPSL.Web.Controllers
{
  public class HelloWorldController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Welcome(string name, int numTimes = 1)
    {
      //使用 HtmlEncoder.Default.Encode 防止恶意输入（即 JavaScript）损害应用
      //return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {ID}");
      ViewData["Message"] = "Hello " + name;
      ViewData["NumTimes"] = numTimes;
      return View();
    }
  }
}