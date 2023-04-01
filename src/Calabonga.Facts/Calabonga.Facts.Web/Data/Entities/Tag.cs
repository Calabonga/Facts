using System.Collections.Generic;
using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Facts.Web.Data.Entities
{
    public class Tag : Identity
    {
        public string Name { get; set; } = null!;

        public ICollection<Fact>? Facts { get; set; }
    }
}
