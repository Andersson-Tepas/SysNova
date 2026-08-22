using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SysNova.DTO
{
    public class GoogleCodeDTO
    {
        [Required]
        public string Code { get; set; } = string.Empty;
    }
}
