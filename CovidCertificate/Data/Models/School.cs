namespace CovidCertificate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    public class School
    {
        public School()
        {
            this.Users = new List<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(User))]
        public string AdminId { get; set; }
        public virtual User Admin { get; set; }
        public string CodeByAdmin { get; set; }
        public string Address { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
