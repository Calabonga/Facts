using System;
using Calabonga.Facts.Web.Mediatr.Base;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Calabonga.Facts.Web.Mediatr
{
    public class ErrorNotification : NotificationBase
    {
        public ErrorNotification(string content, Exception? exception = null)
            : base("ERROR on jfacts.ru", content, "dev@calabonga.net", "noreply@jfacts.ru", exception)
        {
        }
    }

    public class ErrorNotificationHandler : NotificationHandlerBase<ErrorNotification>
    {
        public ErrorNotificationHandler(IUnitOfWork unitOfWork, ILogger<ErrorNotification> logger)
            : base(unitOfWork, logger)
        {
        }
    }
}