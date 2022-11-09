using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace PorscheMarket.Domain.Helpers
{
    public static class ConvertFileHelper
    {
        //Convert File(IFormFIle) to Array byte(byte[]).
        public static IFormFile ConvertToFile(byte[] imgArr)
        {
            IFormFile fileImg = null;
            using(var stream=new MemoryStream(imgArr))
            {
                fileImg = new FormFile(stream, 0, imgArr.Length, "name", "fileName");
                return fileImg;
            }
        }
        //Convert Array byte(byte[]) to File(IFormFIle).
        public static byte[] ConvertToByte(IFormFile fileImg)
        {
            byte[] imageporsche;
            using (var binaryReader = new BinaryReader(fileImg.OpenReadStream()))
            {
                imageporsche = binaryReader.ReadBytes((int)fileImg.Length);
            }
            return imageporsche;
        }
        //Convert File(IFormFile) to String.
        public static string ReadAsList(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            return result.ToString();
        }
    }
}
