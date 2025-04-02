using System.ComponentModel.DataAnnotations;

namespace Agenda.Models
{
    public class Utilizator
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele este necesar.")]
        public string? Nume { get; set; }
        public string? Prenume { get; set; }

        [RegularExpression(@"^\+40[0-9]{9}$", ErrorMessage = "Numarul de telefon trebuie sa aiba formatul +40 urmat de 9 cifre.")]
        public string? Telefon { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Observatie { get; set; }
    }
}
