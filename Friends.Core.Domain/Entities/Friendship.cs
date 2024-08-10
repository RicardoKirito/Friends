using Friends.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Domain.Entities
{
    public class Friendship : Auditable
    { 
        public int User1 { get; set; }
        public int User2 { get; set; }

        //Nav Prop
        public User User { get; set; }
    }
}
