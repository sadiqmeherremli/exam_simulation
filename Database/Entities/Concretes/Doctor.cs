using Database.Entities.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities.Concretes
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string? ImgUrl { get; set; }

        [NotMapped]

        public IFormFile ImgFile { get; set; }
    }
}
