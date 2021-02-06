using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Facts.Web.Data
{
    public class Fact: Auditable
    {
        public string Content { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
