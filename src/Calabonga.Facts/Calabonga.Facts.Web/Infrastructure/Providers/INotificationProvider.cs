using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calabonga.Facts.Web.Data.Entities;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Calabonga.Facts.Web.Infrastructure.Providers
{
    /// <summary>
    /// // Calabonga: update summary (2021-06-06 10:31 INotificationProvider)
    /// </summary>
    public interface INotificationProvider
    {
        /// <summary>
        /// // Calabonga: update summary (2021-06-06 10:31 INotificationProvider)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ProcessAsync(CancellationToken token);
    }

    public class NotificationProvider : INotificationProvider
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public NotificationProvider(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IEmailService emailService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        /// <summary>
        /// // Calabonga: update summary (2021-06-06 10:31 INotificationProvider)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task ProcessAsync(CancellationToken token)
        {
            var repository = _unitOfWork.GetRepository<Notification>();

            var items = repository
                .GetAll(predicate: x => !x.IsCompleted, orderBy: o => o.OrderBy(x => x.CreatedAt))
                .ToList();

            if (!items.Any())
            {
                return;
            }

            var emails = _mapper.Map<IEnumerable<EmailMessage>>(items);
            foreach (var email in emails)
            {
                var isSent = await _emailService.SendAsync(email, token);
                if (isSent)
                {
                    NotificationCompleted(Guid.Parse(email.Id));
                }
            }

        }

        private void NotificationCompleted(Guid id)
        {
            var repository = _unitOfWork.GetRepository<Notification>();
            var notification = repository.GetFirstOrDefault(predicate: x => x.Id == id, disableTracking: false);
            if (notification == null)
            {
                return;
            }
            notification.IsCompleted = true;
            repository.Update(notification);
            _unitOfWork.SaveChanges();
        }
    }

    public class EmailMessage
    {
        public string Id { get; set; } = null;

        public string Author { get; set; } = null!;

        public string Recipient { get; set; } = null!;

        public string AddressTo { get; set; } = null!;

        public string AddressFrom { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public bool IsHtml { get; set; }

        public string Body { get; set; } = null!;
    }
}
