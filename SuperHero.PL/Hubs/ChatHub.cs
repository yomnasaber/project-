﻿using Microsoft.AspNetCore.SignalR;
using SuperHero.DAL.Database;

namespace SuperHero.PL.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext _context;
        public ChatHub(ApplicationContext _context)
        {
            this._context = _context;
        }
        public async Task SendMessage(string user, string message, string Path, string ID, string GroupID, string UserID)
        {
            var group = _context.Groups.Where(a => a.ID == Convert.ToInt32(GroupID)).FirstOrDefault();

            var Person = _context.Persons.Where(a => a.Id == UserID).FirstOrDefault();
            ChatGroup chatGroup = new ChatGroup() { Message = message, group = group, groupId = group.ID, Person = Person, PersonId = Person.Id };
            _context.ChatGroups.Add(chatGroup);
            _context.SaveChanges();
            await Clients.All.SendAsync("ReceiveMessage", user, message, Path, ID);
        }

        public async Task JoinGroup(string group, string name)
        {
            await Clients.All.SendAsync("GroupMessage", name, group);
        }

        public async Task SendToGroup(string group, string name, string message)
        {
            await Clients.All.SendAsync("GroupSendToMessage", name, group, message);
        }

        public async Task SendToMessage(string message, string ID)
        {
            await Clients.All.SendAsync("ReceiveToMessage", message);
            Message message1 = new Message() { Text="dfre",When=DateTime.Now };
            _context.Messages.Add(message1);
            _context.SaveChanges();
        }
    }
}
