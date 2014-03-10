using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;


namespace Galleriet.Model
{   
    public class Gallery
    {
       
        private Regex ApprovedExtensions;
        private String physicalUplodedImagesPath;  // är en privat statisk sträng som innehåller den fysiska sökvägen till 
                                                // katalogen där uppladdade filer sparas.
        static string invalidChars = new string(Path.GetInvalidFileNameChars());
        private Regex SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        //För att sedan ersätta otillåtna tecken använd metoden Regex.Replace().
        //Fältet initieras i den statiska konstruktorn.
      
        public string Name { get; set; }
        public string Class { get; set; }

       
        private Gallery()
        {
            ApprovedExtensions = new Regex("(.gif|.jpg|.png)", RegexOptions.IgnoreCase);
            physicalUplodedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(),"~/Content/files");
        }

        public IEnumerable<string> GetImageNames()
        {
            /*  GetImagesNames returnerar en referens av typen IEnumerable<string> till ett List-objekt 
                innehållande bildernas filnamn sorterade i bokstavsordning. Klassen DirectoryInfo med metoden 
                GetFiles är användbar. Det kan vara en god idé att se till att bara filer med filändelserna gif, jpg och 
                png finns i listan.*/
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Content/files"));

            IEnumerable<string> imagesOrdered = di.GetFiles().Select(file => file.Name).OrderBy(item => item);

            return imagesOrdered;
        }
               //namndeklarationen på variablen som används i "loopen"
     

        public bool ImageExists(string name)
        {

            if (File.Exists(name))
            {
                return true;
            }

            return false;
        }

        private bool IsValidImage(Image image)
        {
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                return true;

            else
                return false;
            // Create image.
         //   Image newImage = Image.FromFile("SampImag.jpg");

            // Create Point for upper-left corner of image.
          //  Point ulCorner = new Point(100, 100);

            // Draw image to screen.
           // e.Graphics.DrawImage(newImage, ulCorner);
         
        }

        public string SaveImage(Stream stream, string fileName)
        {   

            if (!ImageExists(fileName))
                throw new ArgumentException("Filen är inte rätt filformat!");

            if(ImageExists(fileName))
            {
               
               string withoutE = Path.GetFileNameWithoutExtension(fileName);
               string withE = Path.GetExtension(fileName);

               fileName = withoutE + 1 + withE;  
            }


            Image image = Image.FromFile(fileName);
            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            thumb.Save(Path.ChangeExtension(fileName, "thumb"));


            return "hej";
        }

    }
}