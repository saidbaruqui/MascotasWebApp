<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="MascotasWebApp.Catalogo" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catálogo de Productos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #F5F5DC;
            font-family: 'Segoe UI', sans-serif;
        }

        h2, h3 {
            color: #4E342E;
            text-align: center;
        }

        .card {
            border: none;
            border-radius: 16px;
            background-color: #FAF9F6;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
        }

        .card-title {
            color: #4E342E;
            font-weight: 600;
        }

        .btn-cafe {
            background-color: #8D6E63;
            color: white;
        }

        .btn-cafe:hover {
            background-color: #6D4C41;
        }

        .btn-info, .btn-success {
            border-radius: 8px;
        }

        .table th, .table td {
            vertical-align: middle;
            text-align: center;
        }

        .section-divider {
            border-top: 2px solid #D7CCC8;
        }

        .btn-outline-danger {
            border-radius: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2 class="mb-4">🐾 Catálogo de Productos</h2>

            <div class="row" runat="server" id="productContainer">
                <asp:Repeater ID="rptCatalogo" runat="server">
                    <ItemTemplate>
                        <div class="col-md-4 col-sm-6 mb-4">
                            <div class="card h-100">
                                <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' class="card-img-top p-3" style="height: 200px; object-fit: contain;" />
                                <div class="card-body d-flex flex-column">
                                    <h5 class="card-title"><%# Eval("nombre") %></h5>
                                    <p class="card-text">
                                        <%# Convert.ToDecimal(Eval("precio_original")) > Convert.ToDecimal(Eval("precio_final")) 
                                            ? $"<span class='text-muted text-decoration-line-through'>{((decimal)Eval("precio_original")).ToString("C")}</span> <span class='text-success fw-bold'>{((decimal)Eval("precio_final")).ToString("C")}</span>"
                                            : $"<span class='text-success fw-bold'>{((decimal)Eval("precio_final")).ToString("C")}</span>" %>
                                    </p>
                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar al carrito" CssClass="btn btn-cafe mt-auto" CommandArgument='<%# Eval("id") %>' OnClick="btnAgregar_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <hr class="section-divider mt-5 mb-4" />

            <h3 class="mb-3">
                🛒 Tu Carrito - 
                <asp:Label ID="lblUsuario" runat="server" CssClass="text-primary fw-semibold"></asp:Label>
                <asp:Button ID="btnLogout" runat="server" Text="Cerrar Sesión" CssClass="btn btn-outline-danger btn-sm float-end" OnClick="btnLogout_Click" />
            </h3>

            <asp:GridView ID="gvCarrito" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped shadow-sm rounded" OnRowCommand="gvCarrito_RowCommand">
                <Columns>
                    <asp:BoundField DataField="nombre" HeaderText="Producto" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="descuento" HeaderText="Descuento (%)" />
                    <asp:BoundField DataField="subtotal" HeaderText="Subtotal con Descuento" DataFormatString="{0:C}" />
                   <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                                CommandArgument='<%# Eval("id") %>'
                                CssClass="btn btn-danger btn-sm rounded-pill" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:Label ID="lblTotal" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Green" CssClass="d-block mt-3 text-center"></asp:Label>

            <div class="text-center mt-4 mb-5">
                <asp:Button ID="btnCheckout" runat="server" Text="Finalizar Compra" CssClass="btn btn-success me-3" OnClick="btnCheckout_Click" />
                <asp:Button ID="btnRegistrarMascota" runat="server" Text="Registrar Mascota" CssClass="btn btn-info" OnClick="btnRegistrarMascota_Click" />
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="d-block text-center text-success"></asp:Label>
        </div>
    </form>
</body>
</html>
