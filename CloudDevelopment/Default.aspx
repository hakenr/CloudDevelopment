<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
			<asp:Label ID="MyLabel" runat="server" /></br>
			<br />
			<asp:FileUpload ID="MyFileUpload" runat="server" />
			<asp:Button ID="GoButton" Text="GO!" OnClick="GoButton_Click" runat="server" />

			<h1>Files</h1>
			<asp:Repeater ID="FilesRepeater" ItemType="Microsoft.WindowsAzure.Storage.Blob.IListBlobItem" runat="server">
				<ItemTemplate>
					<asp:LinkButton ID="FileLink" CommandArgument="<%# Item.Uri %>" Text="<%# Item.Uri %>" OnCommand="FileLink_Command" runat="server" /><br />
				</ItemTemplate>
			</asp:Repeater>
        </div>
    </form>
</body>
</html>
