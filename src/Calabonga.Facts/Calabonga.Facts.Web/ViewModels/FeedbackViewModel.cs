using System.ComponentModel.DataAnnotations;

namespace Calabonga.Facts.Web.ViewModels
{
    // Calabonga: WHAT I DID
    /// <summary>
    /// Feedback Viewmodel
    /// </summary>
    public class FeedbackViewModel
    {
        [Required(ErrorMessage = "{0} - обязтельное поле")]
        [StringLength(100, ErrorMessage = "Длина {0} не должна превышать {1} символов")]
        [Display(Name = "Тема сообщения")]
        public string Subject { get; set; } = null!;

        [Required(ErrorMessage = "{0} - обязтельное поле")]
        [StringLength(50, ErrorMessage = "Длина {0} не должна превышать {1} символов")]
        [Display(Name = "Имя")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "{0} - обязтельное поле")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "{0} - неверный формат")]
        [StringLength(50, ErrorMessage = "Длина {0} не должна превышать {1} символов")]
        [Display(Name = "Email")]
        public string MailFrom { get; set; } = null!;

        [Required(ErrorMessage = "{0} - обязтельное поле")]
        [StringLength(500, ErrorMessage = "Длина {0} не должна превышать {1} символов")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Текст сообщения")]
        public string Body { get; set; } = null!;

        [Required(ErrorMessage = "{0} - обязтельное поле")]
        [Display(Name = "Результат вычисления")]
        public int HumanNumber { get; set; }


        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"Subject: {Subject} UserName: {UserName} MailFrom: {MailFrom} Body: {Body}";
    }
}
