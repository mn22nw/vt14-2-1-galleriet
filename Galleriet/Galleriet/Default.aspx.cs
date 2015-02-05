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
    private string imgDirectory = "~/Content/files/";

    protected void Page_Load(object sender, EventArgs e)
    {   
        //Om session Gallery är null, initiera nytt Gallery-objekt
        if (Gallery == null)
            Gallery = new Gallery();

        //Tilldela den stora bilden värdet för den första bilden i files mappen (om det finns någon), eller till den som är vald i url-strängen
        View_BigImage();
        //Visa ev. meddelanden för klienten (om det finns några dvs.)
        View_Message_if_exists();

    }

    public Gallery Gallery
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
    /*
     *Hämtar  
     */
    public IEnumerable<string> FileRepeater_GetData() 
    {   
        return Gallery.GetImageNames();
    }
    protected void ExitBtn_Click(object sender, EventArgs e)
    {   
        //Dölj meddelanderuta om användaren klickar på kryss-knappen
        successMessage.Attributes.Add("class", "hidden"); 
    }


    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (!IsValid)
        { ValidationSummary1.Attributes.Add("class", "show"); }

        //Om valideringen går igenom
        if (IsValid)
        {
             try
             {
                if (FileUpload1.PostedFile != null)
                {
                    
                    //Hämta fullständiga sökvägen för den uppladdade filen
                    string path = System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName);
                    
                    //Skapa en ny inputstream för den uppladdade filen
                    System.IO.Stream fs = FileUpload1.PostedFile.InputStream;

                    //Spara bilden 
                    string fileName = Gallery.SaveImage(fs, FileUpload1.FileName);

                    //Sätt den stora bilden till den nya bilden
                    bigImage.Src = imgDirectory + fileName;

                    Response.Redirect("~/Default.aspx?name=" + fileName +"&message=" + Gallery.Message);
                 }
                 else 
                {
                  throw new ArgumentException("Var vänlig välj en fil att ladda upp");
                }    
            }
             catch (OutOfMemoryException )
             {
                 var error = new CustomValidator
                 {
                     IsValid = false,
                     ErrorMessage = "Filen är för stor. Var vänlig ladda upp en fil som är max 4MB"
                 };
                 SetFocus(UploadBtn);
                 Page.Validators.Add(error);

                 
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

             //Hide any previous successMessages. 
             successMessage.Visible = false;
             exit.Visible = false;
           }
        }

    protected void View_BigImage()
    {

        if (!string.IsNullOrWhiteSpace(Request.QueryString["name"]))
        {
            string name = Request.QueryString["name"];
            bigImage.Src = "Content/files/" + name;
        }

        else
        {
            string firstImg = FileRepeater_GetData().First();
            bigImage.Src = "Content/files/" + firstImg;
        }

    }

    protected void View_Message_if_exists()
    {
        successMessage.Visible = false;
        exit.Visible = false;

        if (!string.IsNullOrWhiteSpace(Request.QueryString["message"]))
        {
            Label1.CssClass = "visible";
            successMessage.Visible = true;
            exit.Visible = true;
            successMessage.Attributes.Remove("hidden");

            string message = Request.QueryString["message"].ToString();
             Label1.Text = message;
        }
       
    }

    protected void checkfilesize(object source, ServerValidateEventArgs args)
    {
        string data = args.Value;
        args.IsValid = false;
        double filesize = FileUpload1.FileContent.Length;
        
        if (filesize > 5000)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }

        //http://www.codeproject.com/Tips/290098/Asp-Net-Custom-Validator-Control-to-validate-file
    }
    
 }   