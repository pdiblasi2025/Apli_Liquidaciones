using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    NombreUsuario = table.Column<string>(nullable: true),
                    ApellidoUsuario = table.Column<string>(nullable: true),
                    AuthId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conceptos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true),
                    IsGeneric = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conceptos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CondicionesPago",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicionesPago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jurisdiccion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Codigo = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurisdiccion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    User_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licencias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Codigo = table.Column<string>(nullable: true),
                    Expiracion = table.Column<DateTime>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    CompaniaId = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Licencias_Companias_CompaniaId",
                        column: x => x.CompaniaId,
                        principalTable: "Companias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    ProvinciaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localidades_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    OmsId = table.Column<int>(nullable: false),
                    NombreUsuario = table.Column<string>(nullable: true),
                    RazonSocial = table.Column<string>(nullable: true),
                    NumeroDeDocumento = table.Column<string>(nullable: true),
                    CondicionIva = table.Column<int>(nullable: true),
                    TipoCliente = table.Column<int>(nullable: true),
                    CondicionPagoId = table.Column<int>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Calle = table.Column<string>(nullable: true),
                    Altura = table.Column<int>(nullable: true),
                    PisoDepartamento = table.Column<string>(nullable: true),
                    LocalidadId = table.Column<int>(nullable: true),
                    JurisdiccionId = table.Column<int>(nullable: true),
                    CodigoPostal = table.Column<int>(nullable: true),
                    MetodoEnvio = table.Column<int>(nullable: true),
                    EstadoFacturacion = table.Column<int>(nullable: true),
                    EnvioPorEmail = table.Column<bool>(nullable: false),
                    NumeroIngresosBrutos = table.Column<string>(nullable: true),
                    Estado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_CondicionesPago_CondicionPagoId",
                        column: x => x.CondicionPagoId,
                        principalTable: "CondicionesPago",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Jurisdiccion_JurisdiccionId",
                        column: x => x.JurisdiccionId,
                        principalTable: "Jurisdiccion",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConceptosClientes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    ConceptoId = table.Column<int>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(20, 2)", nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptosClientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConceptosClientes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConceptosClientes_Conceptos_ConceptoId",
                        column: x => x.ConceptoId,
                        principalTable: "Conceptos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IngresosBrutosArchivos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Archivo = table.Column<byte[]>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false),
                    Posicion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosBrutosArchivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosBrutosArchivos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Liquidaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    IdCliente = table.Column<int>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    Saldo = table.Column<decimal>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    Factura = table.Column<string>(nullable: true),
                    OtrosComprobantes = table.Column<string>(nullable: true),
                    NumeroFactura = table.Column<int>(nullable: false),
                    MontoTotalImpuestos = table.Column<decimal>(nullable: false),
                    MontoFinalFactura = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liquidaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Liquidaciones_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConceptosLiquidaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    ConceptoId = table.Column<int>(nullable: false),
                    ConceptoClienteId = table.Column<int>(nullable: true),
                    LiquidacionId = table.Column<int>(nullable: false),
                    Detalle = table.Column<string>(nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(20, 2)", nullable: false),
                    Observacion = table.Column<string>(nullable: true),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptosLiquidaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConceptosLiquidaciones_ConceptosClientes_ConceptoClienteId",
                        column: x => x.ConceptoClienteId,
                        principalTable: "ConceptosClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConceptosLiquidaciones_Conceptos_ConceptoId",
                        column: x => x.ConceptoId,
                        principalTable: "Conceptos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConceptosLiquidaciones_Liquidaciones_LiquidacionId",
                        column: x => x.LiquidacionId,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleLiquidacionPos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    OmsId = table.Column<int>(nullable: false),
                    LiquidacionId = table.Column<int>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false),
                    CodigoEnvio = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    Etiqueta = table.Column<string>(nullable: true),
                    Cantidad = table.Column<decimal>(nullable: false),
                    Valoritems = table.Column<decimal>(nullable: false),
                    CostoEnvioTarifado = table.Column<decimal>(nullable: false),
                    CostoEnvioReal = table.Column<decimal>(nullable: false),
                    Peso = table.Column<decimal>(nullable: false),
                    Volumen = table.Column<decimal>(nullable: false),
                    Ancho = table.Column<decimal>(nullable: false),
                    Largo = table.Column<decimal>(nullable: false),
                    Alto = table.Column<decimal>(nullable: false),
                    ValorSinImpuesto = table.Column<decimal>(nullable: false),
                    EstadoEnvio = table.Column<int>(nullable: false),
                    OtrosGastos = table.Column<decimal>(nullable: false),
                    Estado = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleLiquidacionPos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleLiquidacionPos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleLiquidacionPos_Liquidaciones_LiquidacionId",
                        column: x => x.LiquidacionId,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DetalleLiquidacionPre",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedBy = table.Column<string>(nullable: true),
                    DeleteBy = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    OmsId = table.Column<int>(nullable: false),
                    LiquidacionId = table.Column<int>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false),
                    CodigoOrdenPago = table.Column<string>(nullable: true),
                    IdMercadoPago = table.Column<int>(nullable: false),
                    Urlpago = table.Column<string>(nullable: true),
                    IdPreferenciaMP = table.Column<int>(nullable: false),
                    CallbackasociadoMP = table.Column<string>(nullable: true),
                    IdOrdenPago = table.Column<int>(nullable: false),
                    OtrosGastos = table.Column<decimal>(nullable: false),
                    Estado = table.Column<int>(nullable: false),
                    ValorSinImpuesto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleLiquidacionPre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleLiquidacionPre_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleLiquidacionPre_Liquidaciones_LiquidacionId",
                        column: x => x.LiquidacionId,
                        principalTable: "Liquidaciones",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Conceptos",
                columns: new[] { "Id", "Codigo", "CreateDate", "CreatedBy", "DeleteBy", "DeleteDate", "Deleted", "Enabled", "IsGeneric", "Nombre", "UpdateDate", "UpdatedBy" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, false, false, true, "Flete", null, null });

            migrationBuilder.InsertData(
                table: "CondicionesPago",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "A 30 días FF" },
                    { 2, "A 45 días FF" },
                    { 3, "A 60 días FF" }
                });

            migrationBuilder.InsertData(
                table: "Jurisdiccion",
                columns: new[] { "Id", "Codigo", "Descripcion" },
                values: new object[,]
                {
                    { 23, "923", "Tierra del Fuego" },
                    { 22, "922", "Santiago del Estero" },
                    { 21, "921", "Santa Fe" },
                    { 20, "920", "Santa Cruz" },
                    { 19, "919", "San Luis" },
                    { 18, "918", "San Juan" },
                    { 17, "917", "Salta" },
                    { 16, "916", "Rio Negro" },
                    { 15, "915", "Neuquen" },
                    { 14, "914", "Misiones" },
                    { 13, "913", "Mendoza" },
                    { 11, "911", "La Pampa" },
                    { 24, "924", "Tucuman" },
                    { 10, "910", "Jujuy" },
                    { 9, "909", "Formosa" },
                    { 8, "908", "Entre Rios" },
                    { 7, "907", "Chubut" },
                    { 6, "906", "Chaco" },
                    { 5, "905", "Corrientes" },
                    { 4, "904", "Cordoba" },
                    { 3, "903", "Catamarca" },
                    { 2, "902", "Buenos Aires" },
                    { 1, "901", "Capital Federal" },
                    { 12, "912", "La Rioja" },
                    { 25, "Z", "No aplicable" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_CondicionPagoId",
                table: "Clientes",
                column: "CondicionPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_JurisdiccionId",
                table: "Clientes",
                column: "JurisdiccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_LocalidadId",
                table: "Clientes",
                column: "LocalidadId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosClientes_ClienteId",
                table: "ConceptosClientes",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosClientes_ConceptoId",
                table: "ConceptosClientes",
                column: "ConceptoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosLiquidaciones_ConceptoClienteId",
                table: "ConceptosLiquidaciones",
                column: "ConceptoClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosLiquidaciones_ConceptoId",
                table: "ConceptosLiquidaciones",
                column: "ConceptoId");

            migrationBuilder.CreateIndex(
                name: "IX_ConceptosLiquidaciones_LiquidacionId",
                table: "ConceptosLiquidaciones",
                column: "LiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPos_ClienteId",
                table: "DetalleLiquidacionPos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPos_LiquidacionId",
                table: "DetalleLiquidacionPos",
                column: "LiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPre_ClienteId",
                table: "DetalleLiquidacionPre",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleLiquidacionPre_LiquidacionId",
                table: "DetalleLiquidacionPre",
                column: "LiquidacionId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosBrutosArchivos_ClienteId",
                table: "IngresosBrutosArchivos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Licencias_CompaniaId",
                table: "Licencias",
                column: "CompaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Liquidaciones_IdCliente",
                table: "Liquidaciones",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Localidades_ProvinciaId",
                table: "Localidades",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UsuarioId",
                table: "Roles",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptosLiquidaciones");

            migrationBuilder.DropTable(
                name: "DetalleLiquidacionPos");

            migrationBuilder.DropTable(
                name: "DetalleLiquidacionPre");

            migrationBuilder.DropTable(
                name: "IngresosBrutosArchivos");

            migrationBuilder.DropTable(
                name: "Licencias");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ConceptosClientes");

            migrationBuilder.DropTable(
                name: "Liquidaciones");

            migrationBuilder.DropTable(
                name: "Companias");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Conceptos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "CondicionesPago");

            migrationBuilder.DropTable(
                name: "Jurisdiccion");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "Provincias");
        }
    }
}
