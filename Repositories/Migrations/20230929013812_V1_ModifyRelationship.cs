using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V1_ModifyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("a2b722e3-a34e-4d48-8cd4-d66179ddef6c"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("c2fcc57a-3180-46e7-bfc9-509dc71e8594"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("5fa948d7-9757-4911-815e-619f36ed4170"), null, new DateTime(2023, 9, 29, 8, 38, 12, 607, DateTimeKind.Local).AddTicks(6113), null, null, false, null, null, "Indoor" },
                    { new Guid("c06c409a-574e-48bf-8fc8-b7de225fc61a"), null, new DateTime(2023, 9, 29, 8, 38, 12, 607, DateTimeKind.Local).AddTicks(6130), null, null, false, null, null, "Outdoor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("5fa948d7-9757-4911-815e-619f36ed4170"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("c06c409a-574e-48bf-8fc8-b7de225fc61a"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("a2b722e3-a34e-4d48-8cd4-d66179ddef6c"), null, new DateTime(2023, 9, 25, 8, 5, 20, 454, DateTimeKind.Local).AddTicks(3685), null, null, false, null, null, "Indoor" },
                    { new Guid("c2fcc57a-3180-46e7-bfc9-509dc71e8594"), null, new DateTime(2023, 9, 25, 8, 5, 20, 454, DateTimeKind.Local).AddTicks(3700), null, null, false, null, null, "Outdoor" }
                });
        }
    }
}
