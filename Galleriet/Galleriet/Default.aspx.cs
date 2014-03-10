using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;


public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string UpPath;
        UpPath = "C:\\UploadedUserFiles";
        if (!Directory.Exists(UpPath))
        {
            Directory.CreateDirectory("C:\\UploadedUserFiles\\");
        }
    }
    protected void UploadButton_Click(object sender, EventArgs e)
    {   
      //uploadDetails.visible=true;

        fileName.Text = "hej";
            
        /*    = FileField.PostedFile.FileName;
        FileContent.InnerHtml = FileField.PostedFile.ContentType;
        FileSize.InnerHtml = FileField.PostedFile.ContentLength.ToString();
        UploadDetails.Visible = true;

        string strFileName;
        strFileName = FileField.PostedFile.FileName;
        string c = System.IO.Path.GetFileName(strFileName);
        try
        {
            FileField.PostedFile.SaveAs("C:\\UploadedUserFiles\\" + c);
            Span1.InnerHtml = "File Uploaded Sucessfully.";
        }
        catch (Exception exp)
        {
            Span1.InnerHtml = "Some Error occured.";
            UploadDetails.Visible = false;
        }*/
    }
}
