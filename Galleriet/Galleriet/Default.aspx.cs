using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Galleriet.Model;
using System.Text.RegularExpressions;

namespace Galleriet
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
           
        }

    /*    public IEnumerable<Gallery> FileRepeater_GetData()
        {
           /* var regex = new Regex("(.jpg|.gif)", RegexOptions.IgnoreCase);
            var di = new DirectoryInfo(Server.MapPath("~/Content/files"));
            return (from fi in di.GetFiles()
                   select new Gallery 
                   { 
                   Name = fi.Name,
                   Class = regex.IsMatch(fi.Extension) ? fi.Extension.Substring(1) : string.Empty
                   }).AsEnumerable();  

        }*/
    }
}