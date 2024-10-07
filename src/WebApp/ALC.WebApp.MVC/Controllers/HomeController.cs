using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ALC.WebApp.MVC.Models;

namespace ALC.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    [Route("system-unavailable")]
    public IActionResult SystemUnavailable()
    {
        var modelError = new ErrorViewModel
        {
            Message = "The system is temporarily unavailable, this can occur due to high traffic or maintenance.",
            Title = "System unavailable",
            ErrorCode = 500
        };

        return View("Error", modelError);
    }

    [Route("error/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelError = new ErrorViewModel();

        if (id == 500)
        {
            modelError.Message = "An error has occurred! Please try again later or contact our support.";
            modelError.Title = "An error has occurred!";
            modelError.ErrorCode = id;
        }
        else if (id == 404)
        {
            modelError.Message = "The page you are looking for does not exist! <br />If you have any questions please contact our support.";
            modelError.Title = "Ops! Page not found.";
            modelError.ErrorCode = id;
        }
        else if (id == 403)
        {
            modelError.Message = "You are not allowed to do this.";
            modelError.Title = "Access denied";
            modelError.ErrorCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View("Error", modelError);
    }
}
