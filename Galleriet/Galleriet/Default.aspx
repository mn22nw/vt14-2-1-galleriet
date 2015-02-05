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
                     <div id="exit" runat="server"><p>Stäng</p></div>
                    <asp:Label ID="Label1" runat="server" Text="">    
                    </asp:Label>
                    </div>
                    <%-- Stora bilden --%>
                    <div id="bigImageDiv">
                    <img id="bigImage" alt="" runat="server" src= "#" /></div>

                    <div id="allThumbs">
                    
                        <%-- REPEATER --%>   
                        <asp:Repeater ID="FileRepeater" runat="server" ItemType="System.String" SelectMethod="FileRepeater_GetData">
                            <HeaderTemplate>
                                <ul class="images">
                            </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                      <asp:HyperLink ID="HyperLink1" runat="server" 
                                            ImageUrl='<%# "~/Content/files/thumbs/"+"thumb" +Item %>' 
                                            NavigateUrl='<%# "~/Default.aspx?name=" +Item %>'
                                            CssClass="imgButtons"
                                             />        
                                    </li>
                                </ItemTemplate>
                            <FooterTemplate>    
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
           
                     <%--Filuppladdning --%>
                     <div id="fileUpload">
                         <fieldset> <legend>Ladda upp en fil</legend>
                          <%-- Felmeddelanden --%>  
                          <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors"
                           HeaderText="Fel inträffade. Korrigera och försök igen." /> 
                         
                          <%-- Inputfält och knapp för uppladdning + validering --%> 
                          <asp:FileUpload id="FileUpload1" runat="server" >  </asp:FileUpload>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                          ErrorMessage="Du måste ange en sökväg" CssClass="error" 
                          ControlToValidate="FileUpload1" SetFocusOnError="True" Text="*" Display="Dynamic"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="FileUpload1" SetFocusOnError="True" Text="*" Display="Dynamic" CssClass="error" 
                            ErrorMessage="Filen har inte rätt filformat!" ValidationExpression="^.*\.((j|J)(p|P)(e|E)?(g|G)|(g|G)(i|I)(f|F)|(p|P)(n|N)(g|G))$"></asp:RegularExpressionValidator>
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
       
        function correctNumber(number){
            return number;
        }

        //Scrollar tillbaka till senast klickade tumnagelbild och markerar den
        function SelectAnScrollToThumbNail() {

            var $imgSrc = $('#bigImage').attr("src");

            var a = document.querySelectorAll("#allThumbs a");

            for ($i = 0; $i < a.length; $i++) {

                $n = $imgSrc.split("/");
                $fn = $n[$n.length - 1];

                var href = a[$i].href;
                var name = href.split("=");
                var fileName = name[1];

                //för att $i ska få rätt värde i if satsen kör den via correctNumber()
                var number = correctNumber($i);

                if (fileName === $fn) {
                    console.log(href);
                    console.log(a[number]);
                    thumbnail = a[number].querySelector("img");
                    thumbnail.id = "selectedThumbnail";

                    //scrolla till vald bild i div-en
                    $("#allThumbs").scrollLeft((number * 100));
                }
            }
        }

        $(document).ready(function () {
            
            SelectAnScrollToThumbNail();

            $('#ValidationSummary1').onpropertychange = function () {
                if (this.style.display != 'none') {
                    validationSummary.scrollIntoView();
                }
            }

            //stänger successmeddelandet (används denna ens?)
            $('#exit').click(function () {
                $('#successMessage').css("display", "none");
            });

            //Gömmer img-ikonen som syns när bilden laddas annars.
            $("img").error(function () { $(this).hide(); });
    });
</script>
</body>
</html>
