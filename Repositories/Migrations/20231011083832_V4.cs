using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("b43b68fa-dd9a-4f0a-bf8a-4a970af3863c"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("d29fbe67-b65d-4097-8f71-f32950cebf89"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("17d29532-9089-47e8-a8ae-6e3c40613f43"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("a2a27fd8-8413-42f1-8745-e5a85bb4df65"));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartedDate",
                table: "Trip",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "StartedDate",
                table: "Trip");

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("b43b68fa-dd9a-4f0a-bf8a-4a970af3863c"), null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(9132), null, null, false, null, null, "Indoor" },
                    { new Guid("d29fbe67-b65d-4097-8f71-f32950cebf89"), null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(9134), null, null, false, null, null, "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("17d29532-9089-47e8-a8ae-6e3c40613f43"), "VietNam", null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(8905), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("a2a27fd8-8413-42f1-8745-e5a85bb4df65"), "Norway", null, new DateTime(2023, 10, 11, 10, 58, 4, 808, DateTimeKind.Local).AddTicks(8921), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });
        }
    }
}
