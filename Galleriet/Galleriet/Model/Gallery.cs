using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;


namespace Galleriet.Model
{   
    public class Gallery
    {
       
        private Regex ApprovedExtensions;
        private String physicalUplodedImagesPath;  // är en privat statisk sträng som innehåller den fysiska sökvägen till 
                                                // katalogen där uppladdade filer sparas.
        const string invalidChars = new string(Path.GetInvalidFileNameChars());
        private Regex SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
        //För att sedan ersätta otillåtna tecken använd metoden Regex.Replace().
        //Fältet initieras i den statiska konstruktorn.
      
        public string Name { get; set; }
        public string Class { get; set; }

        private Gallery()
        {
           // Konstruktorn är statisk och dess uppgift är att initiera de statiska ”readonly” fälten.
            ApprovedExtensions = new Regex("(.gif|.jpg|.png)", RegexOptions.IgnoreCase);
            physicalUplodedImagesPath = "~/Content/files";
        }

        public IEnumerable<string> GetImageNames()
        {      
            /*  GetImagesNames returnerar en referens av typen IEnumerable<string> till ett List-objekt 
                innehållande bildernas filnamn sorterade i bokstavsordning. Klassen DirectoryInfo med metoden 
                GetFiles är användbar. Det kan vara en god idé att se till att bara filer med filändelserna gif, jpg och 
                png finns i listan.*/
               
            // example


              var regex = new Regex("(.jpg|.gif)", RegexOptions.IgnoreCase);
                
            DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Content/files"));
               return (from fi in di.GetFiles()
                   select new Gallery 
                   { 
                   Name = fi.Name,
                   Class = regex.IsMatch(fi.Extension) ? fi.Extension.Substring(1) : string.Empty
                   }).AsEnumerable();   
    

            List<string> Imglist = new List<string>();

            for (int i = 0; i < Imglist.Count; i++) 
                {
                   // lägg till bilderna??  Imglist.Add(hmmm);
                }   

                 return Imglist;

        }

        public bool ImageExists(string name)
        {

            return true;
        }

        private bool IsValidImage(Image image)
        {
             // Create image.
            Image newImage = Image.FromFile("SampImag.jpg");

            // Create Point for upper-left corner of image.
            Point ulCorner = new Point(100, 100);

            // Draw image to screen.
           // e.Graphics.DrawImage(newImage, ulCorner);
            return true;
        }

        public string SaveImage(Stream stream, string fileName)
        {
            //
            return "hej";
        }

    }
}