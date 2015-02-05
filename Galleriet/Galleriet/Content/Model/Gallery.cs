using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
//using System.Web.UI.HtmlControls;
using System.Diagnostics;


namespace Galleriet.Model
{
    public class Gallery
    {

        private Regex ApprovedExtensions; //Regex som bestämmer de tillåtna filändelserna
        private String physicalUplodedImagesPath; //Den fysiska sökvägen för bilderna
        static string invalidChars = new string(Path.GetInvalidFileNameChars()); // En sträng med otillåtna tecken.
        private Regex SanitizePath;
        private string _message = "";
        private bool nameChange = false;

        public string Message
        {
            get { return _message; }

            set
            {
                Message = _message;
            }
        }

        public Gallery()
        {   
            //Filändelser som är tillåtna
            ApprovedExtensions = new Regex("(.gif|.jpg|.jpeg|.png)", RegexOptions.IgnoreCase);

            //Sätter till vilken mapp/plats bilderna kommer laddas upp
            physicalUplodedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content\\files\\");

            //Används för att ta bort/rensa otillåtna tecken. 
            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));   //För att sedan ersätta otillåtna tecken använd metoden Regex.Replace().    (SanitizePath.Replace())
            
        }


        /*
         * GetImageNames() Hämtar bilderna och returnerar en sorterad IEnumerable av typen sträng.
         */
        public IEnumerable<string> GetImageNames()
        {
            DirectoryInfo di = new DirectoryInfo(physicalUplodedImagesPath);
            IEnumerable<string> imagesOrdered = di.GetFiles().Select(file => file.Name).OrderBy(item => item);

            return imagesOrdered;
        }

        /*
         * ImageExists - Kollar ifall en bild redan existerar
         * return true/false
         */
        public bool ImageExists(string name)
        {
            if (File.Exists(physicalUplodedImagesPath + name))
            {
                return true;
            }

            return false;
        }

        /*
        * IsValidImage - Kollar ifall filnamnet på den uppladdade filen är ett giltigt filformat
        * return true/false
        */
        private bool IsValidImage(Image image, string fileName)
        {   
            //Ta reda på filändelsen
            string extension = Path.GetExtension(fileName);
    
           Match match =  this.ApprovedExtensions.Match(extension);

            if (match.Success) {
                return true;
            }
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                return true;

            else
                return false;
        }

        /*
         * SaveImage -  Sparar bilden på servern
         * return filename
         */
        public string SaveImage(Stream stream, string fileName)
        {
            nameChange = false;

            //Ta bort/rensa otillåtna tecken i filnamnet
            fileName = SanitizePath.Replace(fileName, "1");

            //Verifiera att filen är av rätt MIME-typ och kasta ett undantag om den inte är det
            if (!IsValidImage(Image.FromStream(stream), fileName))
                {
                    throw new ArgumentException("Filen är inte rätt filformat!");
                }

            //Säkerställ att filnamnet är unikt 
            fileName = renameFileIfExists(fileName);

            Image image = Image.FromStream(stream);
            image.Save(physicalUplodedImagesPath + fileName);

            //GENERERA THUMBNAIL
            Image thumbnail = image.GetThumbnailImage(120, 90, null, System.IntPtr.Zero);
            thumbnail.Save(physicalUplodedImagesPath + "/thumbs/thumb" + fileName);
            stream.Close();

            if (!nameChange) 
            {
                _message = "Bilden '" + fileName + "' har sparats!"; 
            }
           
            return fileName; 
        }

        public string renameFileIfExists(string fileName)
        {
            if (ImageExists(fileName))
            {
                string tempfileName = "";
                string withoutEx = Path.GetFileNameWithoutExtension(fileName);
                string withEx = Path.GetExtension(fileName);
                int counter = 1;

                //För varje filnamn som existerar så läggs en siffra inom parantes till på slutet ex. bild(1).jpg
                while (ImageExists(fileName))
                {
                    tempfileName = withoutEx + "(" + counter.ToString() + ")" + withEx;
                    counter++;
                    fileName = tempfileName;
                }

                //meddela klienten att filnamnet har ändrats (spara i session) 
                _message = "En fil med samma namn fanns redan. Filen sparades som " + fileName + ".";
                nameChange = true;
            }
            
            return fileName;
        }

    }
}