using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; } = 0;
        public string NotiHeader { get; set; } = "";
        public string NotiBody { get; set; } = "";
        public bool IsRead { get; set; } = false;
        public string Url { get; set; } = "";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Message { get; set; } = "";
        public string CreatedDateSt => this.CreatedDate.ToString("dd-MMM-yyyy HH:mm:ss");
        public string IsReadSt => this.IsRead ? "YES" : "NO";
        public string FromUserName { get; set; } = "";
        public string ToUserName { get; set; } = "";
        public List<Person>? Person { get; set; }
    }
}
