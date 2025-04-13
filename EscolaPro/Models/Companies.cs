﻿using System.ComponentModel.DataAnnotations;

namespace EscolaPro.Models
{
    public class Companies
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string ConnectionString { get; set; }
        [Required]
        public required string CNPJ { get; set; }
        public required bool Status { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required DateTime LastUpdatedAt { get; set; }
    }
}
