<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Galleriet.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Galleriet</title>
</head>
<body>
    <form id="FormFileUpload" runat="server">
    <div>
        <h1>Galleriet</h1>
        <p>Välj en fil att ladda upp</p>

        <asp:FileUpload ID="imgUpload" runat="server" Size="60" />

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
    <div id="uploadDetails" visible="false">
        <p>Filnamn </p> <span id="fileName" runat="server"></span>
        <p>Fildata </p> <span id="fileContent" runat="server"></span>
        <p>Filstorlek </p> <span id="fileSize" runat="server"></span>
    </div>
    </form>
</body>
</html>
