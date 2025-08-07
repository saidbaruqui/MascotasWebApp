<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Mascotas.aspx.cs" Inherits="MascotasWebApp.Admin_Mascotas" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mascotas Registradas</title>
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

        .form-filtro {
            max-width: 400px;
            margin: 0 auto;
            background-color: #FAF9F6;
            padding: 1.5rem;
            border-radius: 16px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .form-label {
            font-weight: 500;
            color: #4E342E;
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
        <div class="container mb-5">
            <h2>Mascotas Registradas 🐾</h2>

            <div class="form-filtro mb-4">
                <div class="mb-3">
                    <asp:Label ID="lblFiltro" runat="server" CssClass="form-label" Text="Filtrar por ID de Usuario:" />
                    <asp:TextBox ID="txtFiltroUsuario" runat="server" CssClass="form-control" />
                </div>
                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-cafe" OnClick="btnFiltrar_Click" />
                    <asp:Button ID="btnVolverAdminPanel" runat="server" Text="Volver al Panel" CssClass="btn btn-outline-secondary" OnClick="btnVolverAdminPanel_Click" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gvMascotas" runat="server" AutoGenerateColumns="True" CssClass="table table-hover table-striped shadow rounded" />
            </div>
        </div>
    </form>
</body>
</html>
