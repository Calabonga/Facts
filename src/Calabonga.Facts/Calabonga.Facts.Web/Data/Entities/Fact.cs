using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Facts.Web.Data.Entities
{
    public class Fact : Auditable
    {
        public string Content { get; set; } = null!;

        public ICollection<Tag>? Tags { get; set; }
    }
}
