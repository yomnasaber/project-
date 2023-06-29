using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Reposoratory;
using SuperHero.DAL.Database;

namespace SuperHero.PL.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<Person> _userManager;
        public ChatController(ApplicationContext context, UserManager<Person> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.currentUserName = currentUser.UserName;
            }
           
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        public async Task<IActionResult> create(Message message)
        {

            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var person = await _userManager.GetUserAsync(User);
                message.PersonID = person.Id;
               var messageUser= await _context.Messages.AddAsync(message);
                return View(messageUser);
            }

            return View();
        }
    }
}
