using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Diagnostics;


namespace Galleriet.Model
{
    public class Gallery
    {

        private Regex ApprovedExtensions;
        private String physicalUplodedImagesPath; 
        static string invalidChars = new string(Path.GetInvalidFileNameChars());
        private Regex SanitizePath; 

        public Gallery()
        {
            ApprovedExtensions = new Regex("(.gif|.jpg|.jpeg|.png)", RegexOptions.IgnoreCase);

            physicalUplodedImagesPath = Path.Combine(AppDomain.CurrentDomain.GetData("APPBASE").ToString(), "Content\\files\\");

            SanitizePath = new Regex(string.Format("[{0}]", Regex.Escape(invalidChars)));
            //För att sedan ersätta otillåtna tecken använd metoden Regex.Replace().
        }

        public IEnumerable<string> GetImageNames()
        {
            DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/Content/files/"));
            IEnumerable<string> imagesOrdered = di.GetFiles().Select(file => file.Name).OrderBy(item => item);

            return imagesOrdered;
        }

        public bool ImageExists(string name)
        {
            Debug.WriteLine(physicalUplodedImagesPath + name);
            if (File.Exists(physicalUplodedImagesPath + name))
            {
                return true;
            }

            return false;
        }

        private bool IsValidImage(Image image, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            Gallery getRegex = new Gallery();
           
            Match match = getRegex.ApprovedExtensions.Match(extension);
            //http://www.dotnetperls.com/static-regex
            if (match.Success)
                return true;
           
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid ||
                image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid || image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                return true;

            else
                return false;
        }

        public string[] SaveImage(Stream stream, string fileName)
        {
            fileName = SanitizePath.Replace(fileName, "1");

            string[] SuccessMessages = new string[2];
            SuccessMessages[0] = "Bilden '" + fileName + "' har sparats!"; 

            try
            {
                if (!IsValidImage(Image.FromStream(stream), fileName)) //verifierar att filen är av rätt MIME-typ
                {
                    Debug.WriteLine("miipuu");
                    throw new ArgumentException("Filen är inte rätt filformat!");
                }
            }
            catch
            {
                throw new ArgumentException("Filen är inte rätt filformat!");
            }


            if (ImageExists(fileName)) // säkerställer att filnamnet är unikt
            {
               string tempfileName = "";
               string withoutE = Path.GetFileNameWithoutExtension(fileName);
               string withE = Path.GetExtension(fileName);

			   int counter = 1;

			        while (ImageExists(fileName))
			        { 
				        tempfileName = withoutE + "(" + counter.ToString() + ")" + withE;
				        fileName = physicalUplodedImagesPath + tempfileName;
				        counter++;
			        }
			        fileName = tempfileName;
                    SuccessMessages[0] = "En fil med samma namn fanns redan." + "<br />Filen sparade som " + fileName +".";
                   
		        }
            SuccessMessages[1] = fileName;

            Image image = Image.FromStream(stream);
            image.Save(HttpContext.Current.Server.MapPath("~/Content/files/" + fileName));
            

            //**GENERATE THUMBNAIL**//
            Image thumbnail = image.GetThumbnailImage(120, 90, null, System.IntPtr.Zero);
            thumbnail.Save(HttpContext.Current.Server.MapPath("~/Content/files/thumbs/" + "thumb" + fileName ));
            stream.Close();

            return SuccessMessages; 
        }
    }
}