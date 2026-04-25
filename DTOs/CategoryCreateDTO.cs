using System.ComponentModel.DataAnnotations;

namespace CategoryApi.DTOs;

public class CategoryCreateDTO
{
     [Required(ErrorMessage = "Nome é obrigatório")]
        public string ? Name { get; set; }
}