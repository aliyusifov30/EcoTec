using Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SupportImage:BaseEntity
    {
        public string? ImageLeftTop { get; set; }
        public string? ImageRightTop { get; set; }
        public string? ImageLeftBottom { get; set; }
        public string? ImageRightBottom { get; set; }
        public string Text { get; set; }
        [NotMapped]
        public IFormFile? ImageFileLeftTop { get; set; }
        [NotMapped]

        public IFormFile? ImageFileRightTop { get; set; }
        [NotMapped]

        public IFormFile? ImageFileLeftBottom { get; set; }
        [NotMapped]

        public IFormFile? ImageFileRightBottom { get; set; }
    }
}
