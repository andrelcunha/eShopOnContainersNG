using System;
using ALC.WebApp.MVC.Models;

namespace ALC.WebApp.MVC.Services;

public interface ICatalogService
{
    Task<IEnumerable<ProductViewModel>> GetAll();
    Task<ProductViewModel> GetById(Guid id);
}
