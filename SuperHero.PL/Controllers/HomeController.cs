using FRYMA_SuperHero.BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IBaseRepsoratory<City> city;
        private readonly IToastNotification _toastNotification;
        public HomeController(IBaseRepsoratory<City> city, IToastNotification toastNotification)
        {
            this.city = city;
            _toastNotification = toastNotification;
        }
        public IActionResult Index()
        {
            _toastNotification.AddSuccessToastMessage("Movie HomeIndex successfully");
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
      
        public IActionResult Test2()
        {
            return View();
        }
        public async Task <IActionResult> Test3()
        {
            var data = await city.GetAll();
            return View(data);
        }
    }
}
