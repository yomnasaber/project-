using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class MessageVM
    {
        public int Id { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Text { get; set; }
        public DateTime? When { get; set; }
        public string? PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person? Person { get; set; }
        public IEnumerable<Message>? Chat { get; set; }
        
    }
}
