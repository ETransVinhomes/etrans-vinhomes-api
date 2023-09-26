using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("1023a4ef-b23b-4d06-b8ed-a798fc94aed3"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("5c10e2b1-55e3-4934-af96-70ca5d68da30"));

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("a2b722e3-a34e-4d48-8cd4-d66179ddef6c"), null, new DateTime(2023, 9, 25, 8, 5, 20, 454, DateTimeKind.Local).AddTicks(3685), null, null, false, null, null, "Indoor" },
                    { new Guid("c2fcc57a-3180-46e7-bfc9-509dc71e8594"), null, new DateTime(2023, 9, 25, 8, 5, 20, 454, DateTimeKind.Local).AddTicks(3700), null, null, false, null, null, "Outdoor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("a2b722e3-a34e-4d48-8cd4-d66179ddef6c"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("c2fcc57a-3180-46e7-bfc9-509dc71e8594"));

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("1023a4ef-b23b-4d06-b8ed-a798fc94aed3"), null, new DateTime(2023, 9, 10, 23, 14, 23, 378, DateTimeKind.Local).AddTicks(7821), null, null, false, null, null, "Indoor" },
                    { new Guid("5c10e2b1-55e3-4934-af96-70ca5d68da30"), null, new DateTime(2023, 9, 10, 23, 14, 23, 378, DateTimeKind.Local).AddTicks(7846), null, null, false, null, null, "Outdoor" }
                });
        }
    }
}
