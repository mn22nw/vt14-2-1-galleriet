<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
</head>
<body>
    <form id="FormFileUpload" runat="server">
    <div id="test">
        <h1>Galleriet</h1>
        <p>Välj en fil att ladda upp</p>

        <asp:FileUpload ID="imgUpload" runat="server" />

        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" />
        <asp:Repeater ID="FileRepeater" runat="server" ItemType="DataboundFiles.Model.Gallery" SelectMethod="FileRepeater_GetData">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        Text=" <%# Item.Name %>" NavigateUrl='<%# "~/Content/files" +Item.Name %>'
                        CssClass=" <%# Item.Class %>" />                 
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div id="uploadDetails" runat="Server" >
        <asp:Label ID="fileName" runat="server" Text="Filnamn"></asp:Label>
        <asp:Label ID="fileContent" runat="server" Text="Filtyp"></asp:Label>
        <asp:Label ID="fileSize" runat="server" Text="Filstorlek"></asp:Label>
    </div>
    </form>
</body>
</html>
