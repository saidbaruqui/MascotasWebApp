<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Proveedores.aspx.cs" Inherits="MascotasWebApp.Admin_Proveedores" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Proveedores</title>
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
            max-width: 600px;
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

        .form-control:focus {
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Gestión de Proveedores 📦</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block text-center mb-3" />

            <!-- FORMULARIO -->
            <div class="card-formulario">
                <asp:HiddenField ID="hfProveedorID" runat="server" />

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtNombre" runat="server" CssClass="form-label" Text="Nombre:" />
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtTelefono" runat="server" CssClass="form-label" Text="Teléfono:" />
                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtCorreo" runat="server" CssClass="form-label" Text="Correo:" />
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtDireccion" runat="server" CssClass="form-label" Text="Dirección:" />
                    <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Proveedor" CssClass="btn btn-cafe" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnVolverAdminPanel" runat="server" Text="Volver" CssClass="btn btn-outline-secondary" OnClick="btnVolverAdminPanel_Click" />
                </div>
            </div>

            <!-- TABLA -->
            <div class="table-responsive mb-5">
                <asp:GridView ID="gvProveedores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover shadow rounded mt-4" OnRowCommand="gvProveedores_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                        <asp:BoundField DataField="correo" HeaderText="Correo" />
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                        <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-warning" />
                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-danger" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
