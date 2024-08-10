using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Domain.Common
{
    public class Auditable
    {
        public virtual int Id { get; set; }
        public string CreadedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

    }
}
