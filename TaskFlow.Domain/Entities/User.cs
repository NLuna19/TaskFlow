using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskFlow.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Guardaremos el hash, no la contraseña en texto plano
        public string PasswordHash { get; set; } = string.Empty;

        // Relación con los proyectos que posee o crea
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}