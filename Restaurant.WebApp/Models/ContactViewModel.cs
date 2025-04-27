using System.ComponentModel.DataAnnotations;

namespace Restaurant.WebApp.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Моля, въведете имейл адрес.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Моля, въведете съобщение.")]
        [StringLength(1000, ErrorMessage = "Съобщението трябва да е до 1000 символа.")]
        public string Message { get; set; }
    }
}