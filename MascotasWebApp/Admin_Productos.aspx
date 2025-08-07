<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Productos.aspx.cs" Inherits="MascotasWebApp.Admin_Productos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Productos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #F5F5DC;
            font-family: 'Segoe UI', sans-serif;
        }

        h2 {
            text-align: center;
            color: #4E342E;
            margin-top: 30px;
            margin-bottom: 30px;
        }

        .card-formulario {
            max-width: 700px;
            margin: 0 auto 40px auto;
            background-color: #FAF9F6;
            border-radius: 16px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
            padding: 2rem;
        }

        .form-label {
            color: #4E342E;
            font-weight: 500;
        }

        .form-control:focus, .form-select:focus {
            border-color: #8D6E63;
            box-shadow: 0 0 0 0.2rem rgba(141, 110, 99, 0.25);
        }

        .btn-cafe {
            background-color: #8D6E63;
            color: white;
        }

        .btn-cafe:hover {
            background-color: #6D4C41;
        }

        .table th, .table td {
            vertical-align: middle;
            text-align: center;
        }

        .form-buttons .btn {
            margin-right: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <asp:HiddenField ID="hfProductoID" runat="server" />

            <h2>Gestión de Productos 🛒</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block text-center fw-bold mb-3" />

            <!-- FORMULARIO -->
            <div class="card-formulario">
                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtNombre" runat="server" CssClass="form-label" Text="Nombre:" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtPrecio" runat="server" CssClass="form-label" Text="Precio:" />
                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" Visible="False" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtCantidad" runat="server" CssClass="form-label" Text="Cantidad:" />
                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtDescripcion" runat="server" CssClass="form-label" Text="Descripción:" />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" Visible="False" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="ddlCategoria" runat="server" CssClass="form-label" Text="Categoría:" />
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Alimento" Value="alimento" />
                        <asp:ListItem Text="Juguete" Value="juguete" />
                        <asp:ListItem Text="Accesorio" Value="accesorio" />
                        <asp:ListItem Text="Cuidado" Value="cuidado" />
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="fuImagen" runat="server" CssClass="form-label" Text="Imagen del producto:" />
                    <asp:FileUpload ID="fuImagen" runat="server" CssClass="form-control" />
                </div>

                <div class="form-buttons d-flex justify-content-between">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Producto" CssClass="btn btn-cafe" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary" OnClick="btnClear_Click" CausesValidation="False" />
                    <asp:Button ID="btnVolverAdminPanel" runat="server" Text="Volver" CssClass="btn btn-outline-secondary" OnClick="btnVolverAdminPanel_Click" />
                </div>
            </div>

            <!-- TABLA -->
            <div class="table-responsive mb-5 mt-4">
              <asp:GridView 
                    ID="gvProductos" 
                    runat="server" 
                    AutoGenerateColumns="False" 
                    CssClass="table table-hover table-striped shadow rounded"
                    OnRowCommand="gvProductos_RowCommand"
                    DataKeyNames="id,precio,descripcion,cantidad">

                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="precio" HeaderText="PrecioSinFormato" Visible="False" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="categoria" HeaderText="Categoría" />
                        <asp:BoundField DataField="descripcion" HeaderText="DescripcionOculta" Visible="False" />
                        <asp:ImageField DataImageUrlField="imagen" HeaderText="Imagen" ControlStyle-Height="60px" ControlStyle-Width="60px" />

                       
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-warning" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                                    CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-danger" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
