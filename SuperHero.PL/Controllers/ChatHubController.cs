using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using System;

namespace SuperHero.PL.Controllers
{
    public class ChatHubController : Controller
    {
        private readonly IServiesRep serviesRep;
        private readonly SignInManager<Person> signInManager;
        private readonly UserManager<Person> userManager;
        private readonly IBaseRepsoratory<Group> Group;
        public ChatHubController(IServiesRep serviesRep, SignInManager<Person> signInManager, IBaseRepsoratory<Group> Group, UserManager<Person> userManager)
        {
            this.serviesRep = serviesRep;
            this.signInManager = signInManager;
            this.Group = Group;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            Random randomNumber = new Random();
            int RnadomSession = randomNumber.Next(0, 955121135);
            HttpContext.Session.SetInt32("UserId", RnadomSession);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            var FindIn = await serviesRep.FindById(PersonProfile.Id, id);
            var data = await serviesRep.FindAllGroupById(PersonProfile.Id);
            if (data.Count() != 0)
            {
                if (FindIn != null)
                {
                    var Chat = await serviesRep.GetAllChatGroup(id);
                    var GroupName = await Group.GetByID(id);
                    TempData["GroupName"] = GroupName.Name;
                    TempData["GroupID"] = GroupName.ID;
                    ListGroupVM listGroupVM = new ListGroupVM()
                    {
                        Chat = Chat,
                        Groups = data
                    };
                    return View(listGroupVM);
                }
            }
            return RedirectToAction("GetAll", "Person");

        }
        public async Task<IActionResult> Index1(Message message)
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.currentUser = currentUser.UserName;
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var person = await userManager.GetUserAsync(User);
                message.PersonID = person.Id;
                await serviesRep.GetBYUser(message.UserName);

                var messageVM = await serviesRep.GetBYUser(message.UserName);


                return View(messageVM);
            }
            return View();
        }
        public async Task<IActionResult> create(Message message)
        {
           
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var person = await userManager.GetUserAsync(User);
                message.PersonID = person.Id;
                var messageVM= await serviesRep.GetBYUser(message.UserName);

                
                return View(messageVM);
            }
            return View();
        }

    }
}
