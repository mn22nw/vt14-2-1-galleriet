<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<head runat="server">
    <title>Galleriet</title>
    <link href="~/Content/Style.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
</head>
<body>
     <div id="page">
         <div id="AllContent">
    <form id="FormFileUpload" runat="server">
        <h1>Galleriet</h1>
        <div id="ImageGallery" >
            <div id="successMessage" runat="server" >
             <div id="exit"><p>Stäng</p></div>
            <asp:Label ID="Label1" runat="server" Text="">    
            </asp:Label>
           
            </div>
            <%-- Stora bilden --%>
            <div id="bigImageDiv">
            <img id="bigImage" alt="" runat="server" src="" /></div>

            <div id="allThumbs">
         <%-- REPEATER --%>   
            <asp:Repeater ID="FileRepeater" runat="server" ItemType="System.String" SelectMethod="FileRepeater_GetData">
            <HeaderTemplate>
                <ul class="images">
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                   <%-- <asp:HyperLink ID="HyperLink1" runat="server" 
                        ImageUrl='<%# "~/Content/files/thumbs/"+"thumb" +Item %>' 
                        NavigateUrl='<%# "~/Content/files/" +Item %>'
                        Target="content"
                         />     --%>     
                    <img src='<%# "Content/files/thumbs/"+"thumb" +Item %>'  alt='<%# Item %>'  class="imgButtons" style="cursor:pointer"
                         />
                </li>
            </ItemTemplate>
            <FooterTemplate>    
                </ul>
            </FooterTemplate>
            </asp:Repeater>
            </div>
           
             <%--FILE UPLOAD --%>
             <div id="fileUpload">
             <fieldset> <legend>Ladda upp en fil</legend>
              <%-- Felmeddelanden --%>
               
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors"
               HeaderText="Fel inträffade. Korrigera och försök igen." /> 

              <asp:FileUpload id="FileUpload1" runat="server" >  </asp:FileUpload>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
              ErrorMessage="Du måste ange en sökväg" CssClass="error" 
              ControlToValidate="FileUpload1" SetFocusOnError="True" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="FileUpload1" SetFocusOnError="True" Text="*" Display="Dynamic" CssClass="error" 
            ErrorMessage="Filen har inte rätt filformat!" ValidationExpression="^.*\.(.gif|.jpg|.png|.GIF|.JPG|.JPEG|.PNG)$"></asp:RegularExpressionValidator>
          --%> <%--onclick="<% imageTag_Click();%>" --%>
              <asp:Button id="UploadBtn" 
                    Text="Ladda upp" OnClick="UploadButton_Click"  
                    runat="server"> </asp:Button>    
           </fieldset>
          </div>
                
         </div>
    </form>
    </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#bigImage').src == "") {
                var bigImageStartPath = "Content/files/" + $('li').children().first().attr('alt');
                console.log(bigImageStartPath);
                $('#bigImage').attr('src', bigImageStartPath);
            }

            $('#ImageGallery img').click(function () {  
            var bigImagePath = "Content/files/" + $(this).attr('alt');
            $('#bigImage').attr('src', bigImagePath);
            window.location.hash = $(this).attr('alt');
            });

            $('#exit').click(function () {
                $('#successMessage').css("display", "none");
            });

            window.scrollTo = function () { };

            validationSummary.onpropertychange = function () {
                if (this.style.display != 'none') {
                    validationSummary.scrollIntoView();
                }
            }
         
    });
</script>
</body>
</html>
