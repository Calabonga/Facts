using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Calabonga.Facts.Web.Infrastructure;

namespace Calabonga.Facts.Web.ViewModels
{
    public class FactCreateViewModel : IHaveTags
    {
        /// <summary>
        /// Content for editing
        /// </summary>
        [Display(Name = "Содержание факта")]
        public string? Content { get; set; }

        public List<string>? Tags { get; set; }

        [Range(1, 8, ErrorMessage = "Требуется от 1-8 меток для факта")]
        public int TotalTags { get; set; }
    }
}