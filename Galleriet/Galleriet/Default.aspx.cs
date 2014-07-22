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
using System.Xml.Linq;
using System.IO;
using Galleriet.Model;
using System.Diagnostics;
public partial class Default : System.Web.UI.Page
{
    protected HtmlInputFile filMyFile;

    private Gallery _gallery;

    private Gallery Gallery
    {
        get { return _gallery ?? (_gallery = new Gallery()); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        successMessage.Attributes.Add("class", "hidden");
    }

    public Gallery Gallerie
    {
        get
        {
            return Session["Gallery"] as Gallery;
        }
        set
        {
            Session["Gallery"] = value;
        }
    }
    public IEnumerable<string> FileRepeater_GetData() 
    {
        return Gallery.GetImageNames();
        // FileRepeater.DataSource = images; // defines datasource
        // FileRepeater.DataBind();  // binds datasourse

    }
    protected void ExitBtn_Click(object sender, EventArgs e)
    {
        successMessage.Attributes.Add("class", "hidden");
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
        { ValidationSummary1.Attributes.Add("class", "show"); }

        if (IsValid)
        {
             try
             {
            if (FileUpload1.PostedFile != null)
            {
                string path = System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName);
                
                System.IO.Stream fs = FileUpload1.PostedFile.InputStream;

                string[] SucessMessage = Gallery.SaveImage(fs, FileUpload1.FileName);
                Label1.Text = SucessMessage[0];
                Debug.WriteLine(SucessMessage[0]);
                Debug.WriteLine(SucessMessage[1]);
                bigImage.Src = "~/Content/files/" + SucessMessage[1];
                string ID = Request.QueryString[SucessMessage[1]]; 
               // Response.Redirect("~/Default.aspx");
                              
             }
             else 
            {
              throw new ArgumentException("Var vänlig välj en fil att ladda upp");
            }

            Label1.CssClass = "visible";
            successMessage.Attributes.Add("class", "visible1");
            successMessage.Attributes.Remove("hidden");

           /* HtmlControl htmlDivControl = (HtmlControl)Page.FindControl("images");
            string ulWidth = Convert.ToString(htmlDivControl.Style["width"]);
            htmlDivControl.Style.Add("width", (ulWidth + "200px"));
            */

        }
        catch (Exception ex)
         {
             var error = new CustomValidator
             {
                 IsValid = false,
                 ErrorMessage = ex.Message
             };
             SetFocus(UploadBtn);
             Page.Validators.Add(error);            
         }
            }
        }
    }