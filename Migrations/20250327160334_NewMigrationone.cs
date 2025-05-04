using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testproj.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    ComplaintID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    ComplaintTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.ComplaintID);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackID);
                });

            migrationBuilder.CreateTable(
                name: "Fines",
                columns: table => new
                {
                    FineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    FineAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    FineReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransactionID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fines", x => x.FineID);
                    table.ForeignKey(
                        name: "FK_Fines_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliceReports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoliceRole",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceRole", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "PoliceStation",
                columns: table => new
                {
                    StationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceStation", x => x.StationID);
                });

            migrationBuilder.CreateTable(
                name: "PoliceOfficers",
                columns: table => new
                {
                    OfficerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceOfficers", x => x.OfficerID);
                    table.ForeignKey(
                        name: "FK_PoliceOfficers_PoliceRole_RoleID",
                        column: x => x.RoleID,
                        principalTable: "PoliceRole",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoliceOfficers_PoliceStation_StationID",
                        column: x => x.StationID,
                        principalTable: "PoliceStation",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccidentReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    AccidentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResponderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccidentReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_AccidentReports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccidentReports_PoliceOfficers_ResponderID",
                        column: x => x.ResponderID,
                        principalTable: "PoliceOfficers",
                        principalColumn: "OfficerID");
                });

            migrationBuilder.CreateTable(
                name: "CybercrimeReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvidenceURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssignedOfficerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CybercrimeReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_CybercrimeReports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CybercrimeReports_PoliceOfficers_AssignedOfficerID",
                        column: x => x.AssignedOfficerID,
                        principalTable: "PoliceOfficers",
                        principalColumn: "OfficerID");
                });

            migrationBuilder.CreateTable(
                name: "RobberyReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    RobberyType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RobberyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StolenItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResponderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RobberyReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_RobberyReports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RobberyReports_PoliceOfficers_ResponderID",
                        column: x => x.ResponderID,
                        principalTable: "PoliceOfficers",
                        principalColumn: "OfficerID");
                });

            migrationBuilder.CreateTable(
                name: "SecurityRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    RequestReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateRequested = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssignedOfficerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityRequests", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_SecurityRequests_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityRequests_PoliceOfficers_AssignedOfficerID",
                        column: x => x.AssignedOfficerID,
                        principalTable: "PoliceOfficers",
                        principalColumn: "OfficerID");
                });

            migrationBuilder.CreateTable(
                name: "TrafficReports",
                columns: table => new
                {
                    ReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CitizenID = table.Column<int>(type: "int", nullable: false),
                    TrafficType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SeverityLevel = table.Column<int>(type: "int", nullable: false),
                    DateReported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    ResponderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_TrafficReports_Citizens_CitizenID",
                        column: x => x.CitizenID,
                        principalTable: "Citizens",
                        principalColumn: "CitizenID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrafficReports_PoliceOfficers_ResponderID",
                        column: x => x.ResponderID,
                        principalTable: "PoliceOfficers",
                        principalColumn: "OfficerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccidentReports_CitizenID",
                table: "AccidentReports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_AccidentReports_ResponderID",
                table: "AccidentReports",
                column: "ResponderID");

            migrationBuilder.CreateIndex(
                name: "IX_CybercrimeReports_AssignedOfficerID",
                table: "CybercrimeReports",
                column: "AssignedOfficerID");

            migrationBuilder.CreateIndex(
                name: "IX_CybercrimeReports_CitizenID",
                table: "CybercrimeReports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_Fines_CitizenID",
                table: "Fines",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceOfficers_RoleID",
                table: "PoliceOfficers",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceOfficers_StationID",
                table: "PoliceOfficers",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_PoliceReports_CitizenID",
                table: "PoliceReports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_RobberyReports_CitizenID",
                table: "RobberyReports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_RobberyReports_ResponderID",
                table: "RobberyReports",
                column: "ResponderID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityRequests_AssignedOfficerID",
                table: "SecurityRequests",
                column: "AssignedOfficerID");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityRequests_CitizenID",
                table: "SecurityRequests",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficReports_CitizenID",
                table: "TrafficReports",
                column: "CitizenID");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficReports_ResponderID",
                table: "TrafficReports",
                column: "ResponderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccidentReports");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "CybercrimeReports");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Fines");

            migrationBuilder.DropTable(
                name: "PoliceReports");

            migrationBuilder.DropTable(
                name: "RobberyReports");

            migrationBuilder.DropTable(
                name: "SecurityRequests");

            migrationBuilder.DropTable(
                name: "TrafficReports");

            migrationBuilder.DropTable(
                name: "PoliceOfficers");

            migrationBuilder.DropTable(
                name: "PoliceRole");

            migrationBuilder.DropTable(
                name: "PoliceStation");
        }
    }
}
