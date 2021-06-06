using System;

using Microsoft.Extensions.Logging;

namespace Calabonga.Facts.Web.Extensions
{
    static class EventIdentifiers
    {
        public static readonly EventId DatabaseSavingErrorId   = new(70040001, "DatabaseSavingError");
        public static readonly EventId NotificationAddedId     = new(70040002, "NotificationAdded");
        public static readonly EventId NotificationProcessedId = new(70040003, "NotificationProcessed");
    }

    /// <summary>
    /// ILogger extensions
    /// </summary>
    public static class LoggerExtensions
    {

        /// <summary>
        /// Logger extension for event NotificationProcessed
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public static void NotificationProcessed(this ILogger source, string message) => NotificationProcessedExecute(source, message, null);

        private static readonly Action<ILogger, string, Exception?> NotificationProcessedExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventIdentifiers.NotificationProcessedId, "Processing for notification started {message}");

        /// <summary>
        /// Logger extension for event NotificationAdded
        /// </summary>
        /// <param name="source"></param>
        /// <param name="subject"></param>
        public static void NotificationAdded(this ILogger source, string subject)
        {
            NotificationAddedExecute(source, subject, null);
        }

        private static readonly Action<ILogger, string, Exception?> NotificationAddedExecute =
            LoggerMessage.Define<string>(LogLevel.Information, EventIdentifiers.NotificationAddedId, "New notification created: {subject}");

        /// <summary>
        /// Logger extension for event DatabaseSavingError
        /// </summary>
        /// <param name="source">ILogger instance</param>
        /// <param name="entityName"></param>
        public static void DatabaseSavingError(this ILogger source, string entityName, Exception? exception = null)
        {
            DatabaseSavingErrorExecute(source, entityName, exception);
        }

        private static readonly Action<ILogger, string, Exception?> DatabaseSavingErrorExecute =
            LoggerMessage.Define<string>(LogLevel.Error, EventIdentifiers.DatabaseSavingErrorId, "{entityName}");
    }
}
