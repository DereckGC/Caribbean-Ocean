using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CaribbeanOcean.Application.DTOs.Add
{
    public class AddDTO
    {
        public int Id { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string TargetURL { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public bool Active { get; set; } = true;
    }
    public class CreateAddRequestDTO
    {
        public string TargetURL { get; set; } = string.Empty;
    }

    // Request para PUT — TargetURL y Active, la imagen es opcional
    public class UpdateAddRequestDTO
    {
        public string TargetURL { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }

}
