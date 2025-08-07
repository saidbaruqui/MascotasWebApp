<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MascotasWebApp.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Iniciar Sesión - MascotasWebApp</title>
    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Estilos personalizados -->
    <style>
        body {
            background-color: #D7CCC8;
            font-family: 'Segoe UI', sans-serif;
        }

        .login-container {
            max-width: 400px;
            margin: 80px auto;
            background-color: #FAF9F6;
            border-radius: 16px;
            box-shadow: 0 0 20px rgba(0,0,0,0.1);
            padding: 2rem;
        }

        .form-label {
            color: #4E342E;
            font-weight: bold;
        }

        .btn-cafe {
            background-color: #8D6E63;
            color: white;
        }

        .btn-cafe:hover {
            background-color: #6D4C41;
        }

        .link-registro {
            color: #8D6E63;
            text-decoration: none;
        }

        .link-registro:hover {
            text-decoration: underline;
            color: #4E342E;
        }

        .form-control:focus {
            border-color: #8D6E63;
            box-shadow: 0 0 0 0.2rem rgba(141, 110, 99, 0.25);
        }

        .titulo {
            text-align: center;
            color: #4E342E;
            margin-bottom: 1.5rem;
            font-size: 1.5rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="titulo">Iniciar Sesión</div>

            <div class="mb-3">
                <asp:Label ID="lblCorreo" runat="server" AssociatedControlID="txtCorreo" CssClass="form-label" Text="Correo electrónico:" />
                <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" placeholder="correo@ejemplo.com" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblPwd" runat="server" AssociatedControlID="txtPwd" CssClass="form-label" Text="Contraseña:" />
                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" CssClass="form-control" placeholder="********" />
            </div>

            <div class="d-grid mb-3">
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-cafe" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block text-center mb-2" />

            <div class="text-center">
                <asp:LinkButton ID="lnkRegistrar" runat="server" CssClass="link-registro" OnClick="lnkRegistrar_Click">
                    ¿No tienes cuenta? Regístrate aquí
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
