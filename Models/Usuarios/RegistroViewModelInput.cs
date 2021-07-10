using System.ComponentModel.DataAnnotations;

namespace curso.api.Controllers
{
    public class RegistroViewModelInput
    {
        [Required(ErrorMessage = "O Login é Obrigatório")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O Email é Obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A Senha é Obrigatório")]
        public string Senha { get; set; }
    }
}