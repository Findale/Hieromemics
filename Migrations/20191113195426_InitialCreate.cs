using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieromemics.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pictures",
                columns: table => new
                {
                    PicID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StoragePath = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pictures", x => x.PicID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FriendCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    TemplateID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.TemplateID);
                    table.ForeignKey(
                        name: "FK_templates_pictures_PicID",
                        column: x => x.PicID,
                        principalTable: "pictures",
                        principalColumn: "PicID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "friendList",
                columns: table => new
                {
                    FriendListID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(nullable: false),
                    FriendCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_friendList", x => x.FriendListID);
                    table.ForeignKey(
                        name: "FK_friendList_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    messageID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PicID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    FriendCode = table.Column<int>(nullable: false),
                    timestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.messageID);
                    table.ForeignKey(
                        name: "FK_messages_pictures_PicID",
                        column: x => x.PicID,
                        principalTable: "pictures",
                        principalColumn: "PicID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavedPics",
                columns: table => new
                {
                    SavedPicID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(nullable: false),
                    PicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedPics", x => x.SavedPicID);
                    table.ForeignKey(
                        name: "FK_SavedPics_pictures_PicID",
                        column: x => x.PicID,
                        principalTable: "pictures",
                        principalColumn: "PicID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SavedPics_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendList_UserID",
                table: "friendList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_messages_PicID",
                table: "messages",
                column: "PicID");

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserID",
                table: "messages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPics_PicID",
                table: "SavedPics",
                column: "PicID");

            migrationBuilder.CreateIndex(
                name: "IX_SavedPics_UserID",
                table: "SavedPics",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_templates_PicID",
                table: "templates",
                column: "PicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendList");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "SavedPics");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "pictures");
        }
    }
}
