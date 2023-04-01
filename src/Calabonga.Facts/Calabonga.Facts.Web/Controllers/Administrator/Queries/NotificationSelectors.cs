using Calabonga.Facts.Web.Data.Entities;
using Calabonga.Facts.Web.ViewModels;
using System;
using System.Linq.Expressions;

namespace Calabonga.Facts.Web.Controllers.Administrator.Queries
{
    /// <summary>
    /// Selectors
    /// </summary>
    public static class NotificationSelectors
    {
        /// <summary>
        /// Default Selector for Notification entity
        /// </summary>
        public static Expression<Func<Notification, NotificationViewModel>> Default => s => new NotificationViewModel
        {
            AddressFrom = s.AddressFrom,
            AddressTo = s.AddressTo,
            Content = s.Content,
            CreatedAt = s.CreatedAt,
            CreatedBy = s.CreatedBy,
            Id = s.Id,
            IsCompleted = s.IsCompleted,
            Title = s.Subject,
            UpdatedAt = s.UpdatedAt,
            UpdatedBy = s.UpdatedBy
        };
    }
}
