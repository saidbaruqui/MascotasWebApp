<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="MascotasWebApp.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #F5F5DC;
            font-family: 'Segoe UI', sans-serif;
        }

        .registro-container {
            max-width: 500px;
            margin: 70px auto;
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

        .titulo-formulario {
            text-align: center;
            color: #4E342E;
            margin-bottom: 1.5rem;
        }

        .btn-cafe {
            background-color: #8D6E63;
            color: white;
        }

        .btn-cafe:hover {
            background-color: #6D4C41;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registro-container">
            <h2 class="titulo-formulario">Registro de Usuario</h2>

            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" CssClass="form-label" Text="Nombre:" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre(s)" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblApellidos" runat="server" AssociatedControlID="txtApellidos" CssClass="form-label" Text="Apellidos:" />
                <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" placeholder="Apellidos" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblCorreo" runat="server" AssociatedControlID="txtCorreo" CssClass="form-label" Text="Correo electrónico:" />
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="correo@ejemplo.com" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPwd" runat="server" AssociatedControlID="txtPwd" CssClass="form-label" Text="Contraseña:" />
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control" placeholder="********" />
            </div>

            

            <div class="d-grid">
                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-cafe" OnClick="btnRegistrar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block text-center mt-3 text-danger" />
        </div>
    </form>
</body>
</html>
