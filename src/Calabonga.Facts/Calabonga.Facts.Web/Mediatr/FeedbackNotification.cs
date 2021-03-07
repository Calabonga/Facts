using System;
using Calabonga.Facts.Web.Mediatr.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Calabonga.Facts.Web.Mediatr
{
    public class FeedbackNotification : NotificationBase
    {
        public FeedbackNotification(string content, Exception? exception = null)
            : base("FEEDBACK from jfacts.ru", content, "dev@calabonga.net", "noreply@jfacts.ru", exception)
        {
        }
    }
    public class FeedbackNotificationHandler : NotificationHandlerBase<FeedbackNotification>
    {
        public FeedbackNotificationHandler(IUnitOfWork unitOfWork, ILogger<FeedbackNotification> logger) 
            : base(unitOfWork, logger)
        {
        }
    }
}