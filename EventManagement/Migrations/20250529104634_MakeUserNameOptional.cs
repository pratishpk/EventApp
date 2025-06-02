using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserNameOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantEvents_Events_EventDetailsEventId",
                table: "ParticipantEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantEvents_Users_UserInfoEmailId",
                table: "ParticipantEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Speakers_SpeakerId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantEvents_EventDetailsEventId",
                table: "ParticipantEvents");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantEvents_UserInfoEmailId",
                table: "ParticipantEvents");

            migrationBuilder.DropColumn(
                name: "EventDetailsEventId",
                table: "ParticipantEvents");

            migrationBuilder.DropColumn(
                name: "UserInfoEmailId",
                table: "ParticipantEvents");

            migrationBuilder.AlterColumn<int>(
                name: "SpeakerId",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Speakers_SpeakerId",
                table: "Sessions",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Speakers_SpeakerId",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "SpeakerId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventDetailsEventId",
                table: "ParticipantEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserInfoEmailId",
                table: "ParticipantEvents",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantEvents_EventDetailsEventId",
                table: "ParticipantEvents",
                column: "EventDetailsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantEvents_UserInfoEmailId",
                table: "ParticipantEvents",
                column: "UserInfoEmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantEvents_Events_EventDetailsEventId",
                table: "ParticipantEvents",
                column: "EventDetailsEventId",
                principalTable: "Events",
                principalColumn: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantEvents_Users_UserInfoEmailId",
                table: "ParticipantEvents",
                column: "UserInfoEmailId",
                principalTable: "Users",
                principalColumn: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Speakers_SpeakerId",
                table: "Sessions",
                column: "SpeakerId",
                principalTable: "Speakers",
                principalColumn: "SpeakerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
