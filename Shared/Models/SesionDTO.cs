﻿namespace GestionProyectos.Shared.Models
{
    public class SesionDTO
    {
        public int IdUsuario { get; set; }

        public string? NombreCompleto { get; set; }

        //public string? Correo { get; set; }

        public string Rol { get; set; }
        public DateTime? FechaEliminacion { get; set; }
    }
}
