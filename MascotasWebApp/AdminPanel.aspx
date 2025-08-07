<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="MascotasWebApp.AdminPanel" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Panel de Administración</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #D7CCC8;
            font-family: 'Segoe UI', sans-serif;
        }

        .admin-container {
            max-width: 500px;
            margin: 80px auto;
            background-color: #FAF9F6;
            border-radius: 16px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
            padding: 2rem;
        }

        .card-title {
            color: #4E342E;
            text-align: center;
            font-weight: bold;
            margin-bottom: 1.5rem;
        }

        .list-group-item {
            border: none;
            padding: 0.75rem 1.25rem;
            font-size: 1rem;
            color: #4E342E;
        }

        .list-group-item:hover {
            background-color: #E6B89C;
            color: #FFFFFF;
        }

        .list-group-item.text-danger:hover {
            background-color: #C62828;
            color: #FFFFFF;
        }

        .text-primary {
            color: #8D6E63 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="admin-container">
            <h2 class="card-title">Panel de Administración 👤</h2>

            <asp:Label ID="lblBienvenido" runat="server" CssClass="d-block text-center fs-5 fw-semibold text-primary mb-4" />

            <div class="list-group">
                <a href="Admin_Usuarios.aspx" class="list-group-item list-group-item-action">👥 Gestionar Usuarios</a>
                <a href="Admin_Productos.aspx" class="list-group-item list-group-item-action">🛍️ Gestionar Productos</a>
                <a href="Admin_Promociones.aspx" class="list-group-item list-group-item-action">🎁 Gestionar Promociones</a>
                <a href="Admin_Proveedores.aspx" class="list-group-item list-group-item-action">📦 Gestionar Proveedores</a>
                <a href="Admin_Bitacora.aspx" class="list-group-item list-group-item-action">📄 Ver Bitácora</a>
                <a href="Admin_Mascotas.aspx" class="list-group-item list-group-item-action">🐾 Ver Mascotas</a>
                <a href="CerrarSesion.aspx" class="list-group-item list-group-item-action text-danger">🚪 Cerrar Sesión</a>
            </div>
        </div>
    </form>
</body>
</html>
