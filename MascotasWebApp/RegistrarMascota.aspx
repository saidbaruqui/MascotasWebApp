<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarMascota.aspx.cs" Inherits="MascotasWebApp.RegistrarMascota" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Mascota</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #F5F5DC;
            font-family: 'Segoe UI', sans-serif;
        }

        .form-label {
            color: #4E342E;
            font-weight: 500;
        }

        .form-control:focus {
            border-color: #8D6E63;
            box-shadow: 0 0 0 0.2rem rgba(141, 110, 99, 0.25);
        }

        .container-formulario {
            max-width: 500px;
            background-color: #FAF9F6;
            padding: 2rem;
            border-radius: 16px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            margin-top: 60px;
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
        <div class="container container-formulario">
            <h2 class="titulo-formulario">Registrar Mascota 🐾</h2>

            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" CssClass="form-label" Text="Nombre de la Mascota:" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Max" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblTipo" runat="server" AssociatedControlID="txtTipo" CssClass="form-label" Text="Tipo:" />
                <asp:TextBox ID="txtTipo" runat="server" CssClass="form-control" placeholder="Ej. Perro, Gato" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblRaza" runat="server" AssociatedControlID="txtRaza" CssClass="form-label" Text="Raza:" />
                <asp:TextBox ID="txtRaza" runat="server" CssClass="form-control" placeholder="Ej. Labrador" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblEdad" runat="server" AssociatedControlID="txtEdad" CssClass="form-label" Text="Edad (años):" />
                <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ej. 3" />
            </div>

            <div class="d-grid">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-cafe" OnClick="btnGuardar_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-success d-block mt-3 text-center" />
        </div>
    </form>
</body>
</html>
