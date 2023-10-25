using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "Id",
                keyValue: new Guid("6907d9e3-bb83-4642-b28c-1fb18b4ce5e8"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("2eac0c96-0c83-40f7-9b60-3b8aa114b4e7"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("a9269944-bdb3-4627-be03-247799854b09"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("09fff4f2-4bbb-4b80-8bf9-b7e4fd3720ef"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("1bd60da2-9c40-4d9a-80aa-dabbcbb0b5f2"));

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("7d014bc3-a468-4e6a-bf93-ec36fa7fa9a2"), null, new DateTime(2023, 10, 25, 11, 8, 5, 777, DateTimeKind.Local).AddTicks(9888), null, null, false, null, null, "Outdoor" },
                    { new Guid("b144b37c-f4dc-4f9a-a1b7-9663b17a8656"), null, new DateTime(2023, 10, 25, 11, 8, 5, 777, DateTimeKind.Local).AddTicks(9874), null, null, false, null, null, "Indoor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("7d014bc3-a468-4e6a-bf93-ec36fa7fa9a2"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("b144b37c-f4dc-4f9a-a1b7-9663b17a8656"));

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DateOfBirth", "DeleteBy", "DeletionDate", "Email", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Sex", "Status" },
                values: new object[] { new Guid("6907d9e3-bb83-4642-b28c-1fb18b4ce5e8"), null, new DateTime(2023, 10, 11, 15, 38, 31, 939, DateTimeKind.Local).AddTicks(1483), null, null, null, "customer@gmail.com", new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "CustomerName", "09XXXXXXXX", null, "Active" });

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("2eac0c96-0c83-40f7-9b60-3b8aa114b4e7"), null, new DateTime(2023, 10, 11, 15, 38, 31, 939, DateTimeKind.Local).AddTicks(1512), null, null, false, null, null, "Outdoor" },
                    { new Guid("a9269944-bdb3-4627-be03-247799854b09"), null, new DateTime(2023, 10, 11, 15, 38, 31, 939, DateTimeKind.Local).AddTicks(1509), null, null, false, null, null, "Indoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("09fff4f2-4bbb-4b80-8bf9-b7e4fd3720ef"), "Norway", null, new DateTime(2023, 10, 11, 15, 38, 31, 939, DateTimeKind.Local).AddTicks(1088), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" },
                    { new Guid("1bd60da2-9c40-4d9a-80aa-dabbcbb0b5f2"), "VietNam", null, new DateTime(2023, 10, 11, 15, 38, 31, 939, DateTimeKind.Local).AddTicks(1072), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" }
                });
        }
    }
}
