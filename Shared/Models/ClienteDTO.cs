﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectos.Shared.Models
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }

        public virtual ICollection<ProyectoDTO> Proyectos { get; set; } = new List<ProyectoDTO>();
    }
}