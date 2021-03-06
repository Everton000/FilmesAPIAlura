using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Logradouro é obrigatório!")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O campo Bairro é obrigatório!")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "O campo Numero é obrigatório!")]
        public int Numero { get; set; }
        [JsonIgnore]
        public virtual Cinema Cinema { get; set; }
    }
}