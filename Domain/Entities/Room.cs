using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int SeatCount { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
