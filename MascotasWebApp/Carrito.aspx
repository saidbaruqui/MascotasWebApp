<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="MascotasWebApp.Carrito" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>Carrito</title></head>
<body>
<form id="form1" runat="server">
    <h2>Tu Carrito</h2>
    <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCarrito_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Producto" />
            <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' Text="Eliminar" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />
    <asp:Button ID="btnCheckout" runat="server" Text="Finalizar Compra" OnClick="btnCheckout_Click" />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" />
</form>
</body>
</html>
