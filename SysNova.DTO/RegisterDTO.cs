using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SysNova.DTO
{
    public class RegisterDTO
    {
        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;


        [Required(
            ErrorMessage =
                "El correo electrónico es obligatorio.")]

        [EmailAddress(
            ErrorMessage =
                "Ingresa un correo electrónico válido.")]

        public string Correo { get; set; } =
            string.Empty;


        [Required(
            ErrorMessage =
                "La contraseña es obligatoria.")]

        [MinLength(
            8,
            ErrorMessage =
                "La contraseña debe tener mínimo 8 caracteres.")]

        [RegularExpression(
            @"^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",
            ErrorMessage =
                "La contraseña debe incluir una mayúscula, un número y un símbolo.")]

        public string Password { get; set; } =
            string.Empty;


        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Departamento { get; set; }

        public string? Municipio { get; set; }
    }
}
