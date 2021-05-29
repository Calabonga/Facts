using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calabonga.Facts.Web.ViewModels
{
    public class FactEditViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Content for editing
        /// </summary>
        [Display(Name = "Содержание факта")]
        public string Content { get; set; } = null!;

        public string ReturnUrl { get; set; } = null!;

        [Display(Name = "Метки для факта")]
        public List<string> Tags { get; set; }

        [Range(1, 8, ErrorMessage = "Требуется от 1 до 8 меток")]
        public int TotalTags { get; set; }
    }
}