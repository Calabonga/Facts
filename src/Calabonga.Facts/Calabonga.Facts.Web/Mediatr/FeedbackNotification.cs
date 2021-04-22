using Calabonga.Facts.Web.Mediatr.Base;
using Calabonga.Facts.Web.ViewModels;
using Calabonga.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Calabonga.Facts.Web.Mediatr
{
    public class FeedbackNotification : NotificationBase
    {
        public FeedbackNotification(FeedbackViewModel model)
            : base("FEEDBACK from jfacts.ru", model.ToString()!, "dev@calabonga.net", "noreply@jfacts.ru")
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