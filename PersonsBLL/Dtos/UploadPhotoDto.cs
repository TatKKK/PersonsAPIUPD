using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class UploadPhotoDto
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
