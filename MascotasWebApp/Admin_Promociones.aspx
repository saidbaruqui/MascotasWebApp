<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin_Promociones.aspx.cs" Inherits="MascotasWebApp.Admin_Promociones" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestión de Promociones</title>
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

        .form-check label {
            margin-left: 0.5rem;
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
            <h2>Gestión de Promociones 🎁</h2>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block text-center fw-bold mb-3" />

            <!-- FORMULARIO -->
            <div class="card-formulario">
                <asp:HiddenField ID="hfPromoID" runat="server" />

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtDescripcion" runat="server" CssClass="form-label" Text="Descripción:" />
                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
                </div>

                <div class="mb-3">
                    <asp:Label AssociatedControlID="txtPorcentaje" runat="server" CssClass="form-label" Text="Porcentaje de descuento (%):" />
                    <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control" TextMode="Number" />
                </div>

                <div class="row mb-3">
                    <div class="col">
                        <asp:Label AssociatedControlID="txtFechaInicio" runat="server" CssClass="form-label" Text="Fecha de inicio:" />
                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                    <div class="col">
                        <asp:Label AssociatedControlID="txtFechaFin" runat="server" CssClass="form-label" Text="Fecha final:" />
                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" TextMode="Date" />
                    </div>
                </div>

                <div class="mb-3">
                    <asp:Label runat="server" CssClass="form-label fw-bold" Text="Selecciona los productos para esta promoción:" />
                    <asp:CheckBoxList ID="cblProductos" runat="server" CssClass="form-check" RepeatLayout="Flow" RepeatDirection="Vertical" />
                </div>

                <div class="d-flex justify-content-between">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar Promoción" CssClass="btn btn-cafe" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnVolverAdminPanel" runat="server" Text="Volver al Panel" CssClass="btn btn-outline-secondary" OnClick="btnVolverAdminPanel_Click" />
                </div>
            </div>

            <!-- TABLA -->
            <div class="table-responsive mb-5 mt-4">
                <asp:GridView ID="gvPromociones" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped shadow rounded" OnRowCommand="gvPromociones_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                        <asp:BoundField DataField="porcentaje" HeaderText="Porcentaje (%)" />
                        <asp:BoundField DataField="fecha_inicio" HeaderText="Inicio" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="fecha_fin" HeaderText="Fin" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-warning" />
                        <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-danger" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
