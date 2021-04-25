using Calabonga.Facts.Web.Mediatr.Base;

namespace Calabonga.Facts.Web.Mediatr
{
    /// <summary>
    /// Custom manual notification
    /// </summary>
    // Calabonga: WHAT I DID (2021-04-25 10:35 ManualMessageNotification)
    public class ManualMessageNotification : NotificationBase
    {
        public ManualMessageNotification(string title, string content, string addressFrom, string addressTo)
            : base(title, content, addressFrom, addressTo, null)
        {
        }
    }
}