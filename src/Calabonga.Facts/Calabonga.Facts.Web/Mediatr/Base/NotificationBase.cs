using System;
using MediatR;

namespace Calabonga.Facts.Web.Mediatr.Base
{
    public abstract class NotificationBase : INotification
    {
        protected NotificationBase(string subject, string content, string addressTo, string addressFrom, Exception? exception = null)
        {
            Subject = subject;
            Content = content;
            AddressTo = addressTo;
            AddressFrom = addressFrom;
            Exception = exception;
        }

        public string Subject { get; }

        public string Content { get; }

        public string AddressFrom { get; }

        public Exception? Exception { get; }

        public string AddressTo { get; }

    }
}
