using System;

namespace Calabonga.Facts.Web.ViewModels
{
    /// <summary>
    /// // Calabonga: update summary (2021-05-14 11:36 TagCloud)
    /// </summary>
    public class TagCloud
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? CssClass { get; set; }

        public int Total { get; set; }
    }
}