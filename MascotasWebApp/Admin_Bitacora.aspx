<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Bitacora.aspx.cs" Inherits="MascotasWebApp.Admin_Bitacora" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bitácora de Ventas</title>
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

        .filtro-container {
            max-width: 700px;
            margin: 0 auto 40px auto;
            background-color: #FAF9F6;
            padding: 2rem;
            border-radius: 16px;
            box-shadow: 0 0 15px rgba(0,0,0,0.1);
        }

        .ventas-container {
            max-width: 1000px;
            margin: 0 auto;
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

        .total-label {
            font-weight: bold;
            color: #2E7D32;
            font-size: 1.25rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mb-5">
            <div class="mt-3 text-end">
                <asp:Button ID="btnVolverAdminPanel" runat="server" Text="Volver a Panel" CssClass="btn btn-outline-secondary" OnClick="btnVolverAdminPanel_Click" />
            </div>

            <!-- VENTAS REALIZADAS -->
            <div class="ventas-container mt-5">
                <h2>📊 Ventas Realizadas</h2>

                <asp:Label ID="lblTotalVentas" runat="server" CssClass="total-label d-block text-end my-3" />

                <div class="table-responsive">
                    <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="False"
                        CssClass="table table-hover table-striped shadow rounded">
                        <Columns>
                            <asp:BoundField DataField="VentaID" HeaderText="ID Venta" />
                            <asp:BoundField DataField="UsuarioID" HeaderText="ID Usuario" />
                            <asp:BoundField DataField="Usuario" HeaderText="Nombre Usuario" />
                            <asp:BoundField DataField="PromoID" HeaderText="ID Promoción" />
                            <asp:BoundField DataField="Promocion" HeaderText="Nombre Promoción" />
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="Total" HeaderText="Monto" DataFormatString="{0:C}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
