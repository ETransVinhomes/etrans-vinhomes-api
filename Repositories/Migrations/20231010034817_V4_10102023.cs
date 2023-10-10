using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V4_10102023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_Start_Location_RouteLocation",
                table: "RouteLocation");

            migrationBuilder.DropIndex(
                name: "IX_RouteLocation_EndLocationId",
                table: "RouteLocation");

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("0453ff94-f5a1-47f8-be07-8e687ef0513e"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("be7348c9-0653-4d1e-92bf-cf144a15976a"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("563d5c2e-31d7-4d8f-98c5-2ef8c6ca6fc8"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("ec1813e1-b945-4b1f-a179-52d63e024aab"));

            migrationBuilder.DropColumn(
                name: "EndLocationId",
                table: "RouteLocation");

            migrationBuilder.RenameColumn(
                name: "StartLocationId",
                table: "RouteLocation",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteLocation_StartLocationId",
                table: "RouteLocation",
                newName: "IX_RouteLocation_LocationId");

            migrationBuilder.AddColumn<bool>(
                name: "IsHead",
                table: "RouteLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "NextRouteLocationId",
                table: "RouteLocation",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Route",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("40b4f927-a314-463d-88ab-610c317e4d38"), null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8443), null, null, false, null, null, "Outdoor" },
                    { new Guid("ee3d06ec-d040-4845-9347-791aaba10e0b"), null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8433), null, null, false, null, null, "Indoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("5bd3802c-dbf1-4655-82e3-dc7d0269720e"), "VietNam", null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8248), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("95d277cc-3838-4be1-9fc7-100bb5aede2b"), "Norway", null, new DateTime(2023, 10, 10, 10, 48, 17, 618, DateTimeKind.Local).AddTicks(8262), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocation_NextRouteLocationId",
                table: "RouteLocation",
                column: "NextRouteLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RouteLocation_RouteLocation_NextRouteLocationId",
                table: "RouteLocation",
                column: "NextRouteLocationId",
                principalTable: "RouteLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_RouteLocation_RouteLocation_NextRouteLocationId",
                table: "RouteLocation");

            migrationBuilder.DropIndex(
                name: "IX_RouteLocation_NextRouteLocationId",
                table: "RouteLocation");

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("40b4f927-a314-463d-88ab-610c317e4d38"));

            migrationBuilder.DeleteData(
                table: "LocationType",
                keyColumn: "Id",
                keyValue: new Guid("ee3d06ec-d040-4845-9347-791aaba10e0b"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("5bd3802c-dbf1-4655-82e3-dc7d0269720e"));

            migrationBuilder.DeleteData(
                table: "Provider",
                keyColumn: "Id",
                keyValue: new Guid("95d277cc-3838-4be1-9fc7-100bb5aede2b"));

            migrationBuilder.DropColumn(
                name: "IsHead",
                table: "RouteLocation");

            migrationBuilder.DropColumn(
                name: "NextRouteLocationId",
                table: "RouteLocation");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Route");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "RouteLocation",
                newName: "StartLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_RouteLocation_LocationId",
                table: "RouteLocation",
                newName: "IX_RouteLocation_StartLocationId");

            migrationBuilder.AddColumn<Guid>(
                name: "EndLocationId",
                table: "RouteLocation",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "LocationType",
                columns: new[] { "Id", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "IsDeleted", "ModificationBy", "ModificationDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0453ff94-f5a1-47f8-be07-8e687ef0513e"), null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7730), null, null, false, null, null, "Indoor" },
                    { new Guid("be7348c9-0653-4d1e-92bf-cf144a15976a"), null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7733), null, null, false, null, null, "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Provider",
                columns: new[] { "Id", "Address", "CreatedBy", "CreationDate", "DeleteBy", "DeletionDate", "ExternalId", "IsDeleted", "ModificationBy", "ModificationDate", "Name", "PhoneNumber", "Status" },
                values: new object[,]
                {
                    { new Guid("563d5c2e-31d7-4d8f-98c5-2ef8c6ca6fc8"), "VietNam", null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7479), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "VinETrans", "0778020298", "Active" },
                    { new Guid("ec1813e1-b945-4b1f-a179-52d63e024aab"), "Norway", null, new DateTime(2023, 10, 8, 20, 39, 2, 145, DateTimeKind.Local).AddTicks(7499), null, null, new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, "Phuong Trang ETrans", "0778020298", "Active" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteLocation_EndLocationId",
                table: "RouteLocation",
                column: "EndLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_End_Location_RouteLocation",
                table: "RouteLocation",
                column: "EndLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Start_Location_RouteLocation",
                table: "RouteLocation",
                column: "StartLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
