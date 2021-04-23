using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ImageCompressorUI.Models
{
    public class FileModel
    {
        [Required(ErrorMessage ="Please select file.")]
        [Display(Name ="Browse File")]
        public HttpPostedFileBase[] Files { get; set; }

        [Range(0,100,ErrorMessage ="Please set Compress Size.")]
        [Display(Name = "Compress Size")]
        public long CompressSize { get; set; }
    }
}