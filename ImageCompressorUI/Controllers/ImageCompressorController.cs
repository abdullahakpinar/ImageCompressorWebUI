using ImageCompressorUI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageCompressorUI.Controllers
{
    public class ImageCompressorController : Controller
    {
        // GET: ImageCompressor
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FileModel fileModel)
        {
            if(ModelState.IsValid) 
            {
                if(fileModel.Files.Count() == 1)
                {
                    bool isImageFile = true;
                    foreach(HttpPostedFileBase file in fileModel.Files)
                    {
                        if(file != null)
                        {
                            if(!ImageFileExtensions().Contains(Path.GetExtension(file.FileName).ToUpper()))
                            {
                                isImageFile = false;
                                ViewBag.UploadStatus = "There are files other than image files in the files to be uploaded.";
                            } 
                        }
                    }
                    if(isImageFile)
                    {
                        foreach(HttpPostedFileBase file in fileModel.Files)
                        {
                            if(file != null)
                            { 
                                using(Bitmap bMap = new Bitmap(file.InputStream))
                                {
                                    ImageCodecInfo imgEncoder = GetEncoder(ImageFormat.Jpeg);
                                    System.Drawing.Imaging.Encoder qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                                    EncoderParameters encoderParameters = new EncoderParameters(1);
                                    EncoderParameter encoderParameter = new EncoderParameter(qualityEncoder, fileModel.CompressSize);
                                    encoderParameters.Param[0] = encoderParameter;
                                    MemoryStream fileMemoryStream = new MemoryStream();
                                    bMap.Save(fileMemoryStream, imgEncoder, encoderParameters);
                                    ViewBag.UploadStatus = fileModel.Files.Count().ToString() + " files compressed successfully.";
                                    return File(fileMemoryStream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Octet, DateTime.Now.ToString("ddMMyyyyhhmm") + file.FileName);
                                }                               
                            }
                        }
                    }
                }
                else if(fileModel.Files.Count() > 10)
                {
                    ViewBag.UploadStatus = "You can upload up to 10 images";
                }
            }
            return View();
       
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach(ImageCodecInfo codec in codecs)
            {
                if(codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private List<string> ImageFileExtensions()
        {
           return new List<string>()
                {
                    ".BMP",
                    ".EMF",
                    ".EXİF",
                    ".GİF",
                    ".GUİD",
                    ".ICON",
                    ".JPG",
                    ".JPEG",
                    ".MEMORYBMP",
                    ".PNG",
                    ".TİFF",
                    ".TIFF",
                    ".TİF",
                    ".TIF",
                    ".WMF"
                };
        }
    } 
}