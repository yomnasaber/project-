using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SuperHero.DAL.Database;

namespace SuperHero.PL.Hubs
{
    public class Chat:Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("receiveMessage", message);
            
        }
    }
}
