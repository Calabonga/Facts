using System.Collections.Generic;

namespace Calabonga.Facts.Web.Infrastructure
{
    public interface IHaveTags
    {
        List<string>? Tags { get; set; }
    }
}