using System;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class ReadEnderecoDto
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
    }
}