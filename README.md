# MascotasWebApp

**MascotasWebApp** es una aplicación web desarrollada en **ASP.NET WebForms (C#)** con una base de datos en **SQL Server**, pensada para gestionar una tienda de mascotas. La plataforma ofrece funcionalidades diferenciadas por rol de usuario (cliente, empleado, administrador).

---

## Características principales

### Autenticación y Registro
- Login seguro con SHA256.
- Control de acceso por rol (cliente, empleado, administrador).
- Registro de nuevos usuarios con validaciones y procedimiento almacenado.

### Módulo Cliente
- Visualización de productos en formato de catálogo.
- Carrito de compras con resumen de pedido.
- Compra con inserción en tabla de ventas y detalles.
- Registro de mascotas propias.

### Módulo Administrador
- Gestión completa (CRUD) de:
  - Usuarios
  - Productos (con imágenes)
  - Promociones (con fechas válidas)
  - Proveedores
- Visualización de bitácora de ventas.
- Ver mascotas registradas por clientes.

### Módulo Empleado
- Consulta de productos y stock.
- Visualización de ventas y detalles.
- Visualización de mascotas registradas.
- (Opcional) Registrar ventas si se activa punto de venta.

---

## Tecnologías usadas

- **ASP.NET WebForms (.aspx / .cs)**
- **C#**
- **SQL Server (con procedimientos almacenados y triggers)**
- **Bootstrap para diseño responsivo**
- **Git + GitHub para control de versiones**

---

## Base de datos

Incluye procedimientos almacenados para CRUD de:
- Usuarios
- Productos
- Promociones
- Proveedores

Y trigger para actualizar stock tras cada venta.

---

