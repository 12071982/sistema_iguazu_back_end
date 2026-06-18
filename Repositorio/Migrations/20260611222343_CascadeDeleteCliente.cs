using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositorio.Migrations
{
    public partial class CascadeDeleteCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Nacimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pasaporte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frecuencia_Viajero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Destino",
                columns: table => new
                {
                    ID_Destino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atracciones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idioma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Moneda = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destino", x => x.ID_Destino);
                });

            migrationBuilder.CreateTable(
                name: "error",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_user = table.Column<int>(type: "int", nullable: false),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    controller = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    method = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    user_agent = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    host = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    class_component = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    function_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    line_number = table.Column<int>(type: "int", nullable: false),
                    error = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    error_code = table.Column<int>(type: "int", nullable: false),
                    dateCreate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_error", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Paquete",
                columns: table => new
                {
                    ID_Paquete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Destino = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duracion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio_Base = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Inicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Fin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inclusiones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Exclusiones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquete", x => x.ID_Paquete);
                });

            migrationBuilder.CreateTable(
                name: "Temporada",
                columns: table => new
                {
                    ID_Temporada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temporada", x => x.ID_Temporada);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo_Electronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha_Registro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID_Usuario);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    ID_Reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cliente = table.Column<int>(type: "int", nullable: false),
                    ID_Paquete = table.Column<int>(type: "int", nullable: false),
                    Fecha_Reserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_Personas = table.Column<int>(type: "int", nullable: false),
                    Precio_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.ID_Reserva);
                    table.ForeignKey(
                        name: "FK_Reserva_Cliente_ID_Cliente",
                        column: x => x.ID_Cliente,
                        principalTable: "Cliente",
                        principalColumn: "ID_Cliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paquete_Temporada",
                columns: table => new
                {
                    ID_PaqueteTemporada = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Paquete = table.Column<int>(type: "int", nullable: false),
                    ID_Temporada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paquete_Temporada", x => x.ID_PaqueteTemporada);
                    table.ForeignKey(
                        name: "FK_Paquete_Temporada_Paquete_ID_Paquete",
                        column: x => x.ID_Paquete,
                        principalTable: "Paquete",
                        principalColumn: "ID_Paquete",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paquete_Temporada_Temporada_ID_Temporada",
                        column: x => x.ID_Temporada,
                        principalTable: "Temporada",
                        principalColumn: "ID_Temporada",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    ID_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Reserva = table.Column<int>(type: "int", nullable: false),
                    Metodo_Pago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Pago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero_Transaccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.ID_Pago);
                    table.ForeignKey(
                        name: "FK_Pago_Reserva_ID_Reserva",
                        column: x => x.ID_Reserva,
                        principalTable: "Reserva",
                        principalColumn: "ID_Reserva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Detalle_Pago",
                columns: table => new
                {
                    ID_Detalle_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Pago = table.Column<int>(type: "int", nullable: false),
                    Cuota = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha_Vencimiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Detalle_Pago", x => x.ID_Detalle_Pago);
                    table.ForeignKey(
                        name: "FK_Detalle_Pago_Pago_ID_Pago",
                        column: x => x.ID_Pago,
                        principalTable: "Pago",
                        principalColumn: "ID_Pago",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_Pago_ID_Pago",
                table: "Detalle_Pago",
                column: "ID_Pago");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ID_Reserva",
                table: "Pago",
                column: "ID_Reserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paquete_Temporada_ID_Paquete",
                table: "Paquete_Temporada",
                column: "ID_Paquete");

            migrationBuilder.CreateIndex(
                name: "IX_Paquete_Temporada_ID_Temporada",
                table: "Paquete_Temporada",
                column: "ID_Temporada");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_ID_Cliente",
                table: "Reserva",
                column: "ID_Cliente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destino");

            migrationBuilder.DropTable(
                name: "Detalle_Pago");

            migrationBuilder.DropTable(
                name: "error");

            migrationBuilder.DropTable(
                name: "Paquete_Temporada");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Paquete");

            migrationBuilder.DropTable(
                name: "Temporada");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
