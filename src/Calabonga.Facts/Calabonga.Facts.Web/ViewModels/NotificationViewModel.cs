using Calabonga.EntityFrameworkCore.Entities.Base;
using System;

namespace Calabonga.Facts.Web.ViewModels
{
    /// <summary>
    /// Notification view model
    /// </summary>
    // Calabonga: WHAT I DID (2021-04-25 10:36 NotificationViewModel)
    public class NotificationViewModel: Identity
    {
        public string Title { get; set; } = null!;

        public string? Content { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public string AddressTo { get; set; } = null!;

        public string AddressFrom { get; set; } = null!;

    }
}