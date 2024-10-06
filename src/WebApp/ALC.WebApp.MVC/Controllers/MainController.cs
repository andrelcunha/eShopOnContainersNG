using ALC.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALC.WebApp.MVC.Controllers;

public abstract class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult response)
    {
        if (response != null && response.Errors?.Messages != null && response.Errors.Messages.Count != 0)
        {
            foreach (var mensagem in response.Errors.Messages)
            
            {
                ModelState.AddModelError(string.Empty, mensagem);
            }
            return true;
        }
        return false;
    }
}       

