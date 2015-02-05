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

       //sätt den stora bilden till den första om det finns någon / till den som är vald i url-sträng
        get_BigImage();

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

    protected void get_BigImage()
    {
        //Set big image to the first image in the files folder (if it exists)
        
        string name = Request.QueryString["name"];

        if(!string.IsNullOrEmpty(name) ) {
            
            bigImage.Src = "Content/files/" + name;

        }
        else {
            string firstImg = FileRepeater_GetData().First();
            bigImage.Src = "Content/files/" + firstImg; 
        }   
       
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
    protected void View_Message_if_exists()
    {


        Label1.CssClass = "visible";
        successMessage.Attributes.Add("class", "visible1");
        successMessage.Attributes.Remove("hidden");

        if (!string.IsNullOrWhiteSpace(Request.QueryString["message"]))
        {
           string message = Request.QueryString["message"].ToString();
           Label1.Text = message;
        }
       
        else 
        {
            //Hide old success message if reload
              successMessage.Attributes.Add("class", "hidden");
        }
    }
 }   