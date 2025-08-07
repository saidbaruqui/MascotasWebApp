<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestConexion.aspx.cs" Inherits="MascotasWeb.TestConexion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prueba de Conexión</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnProbarConexion" runat="server" Text="Probar Conexión" OnClick="btnProbarConexion_Click" />
            <br /><br />
            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
