﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers
{
    public class ChatHubController : Controller
    {
        private readonly IServiesRep serviesRep;
        private readonly SignInManager<Person> signInManager;
        private readonly IBaseRepsoratory<Group> Group;
        private readonly IBaseRepsoratory<Person> _userManager;
        private readonly IToastNotification _toastNotification;
        public ChatHubController(IServiesRep serviesRep, IBaseRepsoratory<Person> _userManager, SignInManager<Person> signInManager, IBaseRepsoratory<Group> Group, IToastNotification toastNotification) 
        { 
            this.serviesRep = serviesRep;
            this.signInManager = signInManager;
            this.Group = Group;
            this._userManager = _userManager;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult>Index()
        {
            _toastNotification.AddSuccessToastMessage("Movie NewMessageGroup successfully");
            Random randomNumber = new Random();
            int RnadomSession = randomNumber.Next(0, 955121135); 
            HttpContext.Session.SetInt32("UserId", RnadomSession);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
           
            var data = await serviesRep.FindAllGroupById(PersonProfile.Id);
            if (data.Count() != 0)
            {
                var FindIn = await serviesRep.FindById(PersonProfile.Id, Convert.ToInt32(data.FirstOrDefault().Group));
                if (FindIn != null)
                {
                    var Chat = await serviesRep.GetAllChatGroup(Convert.ToInt32(FindIn.Group));
                    var GroupName = await Group.GetByID(Convert.ToInt32(FindIn.Group));
                    TempData["GroupName"] = GroupName.Name;
                    TempData["GroupID"] = GroupName.ID;
                    ListGroupVM listGroupVM = new ListGroupVM()
                    {Chat = Chat,
                    Groups =data
                    };
                    return View(listGroupVM);
                   
                }
            }
            
            return View(null);

        }
        public async Task<IActionResult> GetMessage(int id)
        {
            _toastNotification.AddSuccessToastMessage("Movie GetMessage successfully");
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
                    return View("Index", listGroupVM);
                }
            }
            
            return RedirectToAction("GetAll", "Person");
        }
        public IActionResult Index2(string Id)
        {
            TempData["PersonId"] = Id;
            return View();
        }
        

        public async Task<IActionResult> Chat(string Id)
        {
            _toastNotification.AddSuccessToastMessage("Movie PrivateChat successfully");
            var user = await _userManager.GetByID(Id);
            var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
            
            var Chat = await serviesRep.GetAllPrivateChat(PersonProfile.Id, Id);
            if (user == null)
                return NotFound();

            PrivateChatVM privateChatVM = new PrivateChatVM() { 
                Chats = Chat,
                Reciver = user
            };
            
            return View(privateChatVM);
        }
    }
}
