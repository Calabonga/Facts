using Calabonga.EntityFrameworkCore.Entities.Base;

namespace Calabonga.Facts.Web.Data
{
    /// <summary>
    /// Notification entity
    /// </summary>
    public class Notification: Auditable
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public bool IsCompleted { get; set; }

        public string AddressFrom { get; set; }

        public string AddressTo { get; set; }

    }
}
