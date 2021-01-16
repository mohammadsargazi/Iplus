using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bipap.DAL.Migrations
{
    public partial class InitialModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    MedicalSystemCode = table.Column<string>(nullable: true),
                    OfficeAddress = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    Clinic = table.Column<string>(nullable: true),
                    ActiveCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndOfTreatmentStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfTreatmentStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileUploadTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploadTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettelmentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettelmentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupportUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    ContractNumber = table.Column<string>(nullable: true),
                    ShabaNumber = table.Column<string>(nullable: true),
                    Credit = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ActiveCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypeInformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    RangeFrom = table.Column<string>(nullable: true),
                    RangeTo = table.Column<string>(nullable: true),
                    Resolution = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    DeviceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypeInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceTypeInformations_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    DeviceStatusId = table.Column<int>(nullable: true),
                    DeviceTypeId = table.Column<int>(nullable: false),
                    SupportUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceStatuses_DeviceStatusId",
                        column: x => x.DeviceStatusId,
                        principalTable: "DeviceStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_SupportUsers_SupportUserId",
                        column: x => x.SupportUserId,
                        principalTable: "SupportUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportUserOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SupportUserId = table.Column<int>(nullable: true),
                    SettelmentStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportUserOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportUserOrders_SettelmentStatuses_SettelmentStatusId",
                        column: x => x.SettelmentStatusId,
                        principalTable: "SettelmentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportUserOrders_SupportUsers_SupportUserId",
                        column: x => x.SupportUserId,
                        principalTable: "SupportUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EndOfTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PatientPurchaseDate = table.Column<DateTime>(nullable: false),
                    EndOfTreatmentStatusId = table.Column<int>(nullable: false),
                    SupportUserId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndOfTreatments_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndOfTreatments_EndOfTreatmentStatus_EndOfTreatmentStatusId",
                        column: x => x.EndOfTreatmentStatusId,
                        principalTable: "EndOfTreatmentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndOfTreatments_SupportUsers_SupportUserId",
                        column: x => x.SupportUserId,
                        principalTable: "SupportUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    PersonalId = table.Column<string>(nullable: true),
                    NantionalCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    InsuranceKinde = table.Column<string>(nullable: true),
                    InsuranceDate = table.Column<DateTime>(nullable: false),
                    InsuranceId = table.Column<int>(nullable: false),
                    ActiveCode = table.Column<string>(nullable: true),
                    GenderId = table.Column<int>(nullable: true),
                    DoctorId = table.Column<int>(nullable: true),
                    DeviceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    SessionCount = table.Column<int>(nullable: false),
                    DayCount = table.Column<int>(nullable: false),
                    HoursActivity = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    FileUploadTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_FileUploadTypes_FileUploadTypeId",
                        column: x => x.FileUploadTypeId,
                        principalTable: "FileUploadTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Range = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: false),
                    DurationOfTreatment = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: true),
                    DeviceTypeId = table.Column<int>(nullable: true),
                    PrescriptionStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_PrescriptionStatuses_PrescriptionStatusId",
                        column: x => x.PrescriptionStatusId,
                        principalTable: "PrescriptionStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StepOneModules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Visible = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Month = table.Column<string>(nullable: true),
                    Day = table.Column<string>(nullable: true),
                    Hour = table.Column<string>(nullable: true),
                    Minutes = table.Column<string>(nullable: true),
                    StrarTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    SessionLength = table.Column<string>(nullable: true),
                    Parameters = table.Column<string>(nullable: true),
                    Pressure = table.Column<string>(nullable: true),
                    Flow = table.Column<string>(nullable: true),
                    Heater = table.Column<string>(nullable: true),
                    Leak = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    FileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepOneModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepOneModules_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceStatusId",
                table: "Devices",
                column: "DeviceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SupportUserId",
                table: "Devices",
                column: "SupportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceTypeInformations_DeviceTypeId",
                table: "DeviceTypeInformations",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfTreatments_DeviceId",
                table: "EndOfTreatments",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfTreatments_EndOfTreatmentStatusId",
                table: "EndOfTreatments",
                column: "EndOfTreatmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfTreatments_SupportUserId",
                table: "EndOfTreatments",
                column: "SupportUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileUploadTypeId",
                table: "Files",
                column: "FileUploadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_PatientId",
                table: "Files",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DeviceId",
                table: "Patients",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GenderId",
                table: "Patients",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DeviceTypeId",
                table: "Prescriptions",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PrescriptionStatusId",
                table: "Prescriptions",
                column: "PrescriptionStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StepOneModules_FileId",
                table: "StepOneModules",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportUserOrders_SettelmentStatusId",
                table: "SupportUserOrders",
                column: "SettelmentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportUserOrders_SupportUserId",
                table: "SupportUserOrders",
                column: "SupportUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "DeviceTypeInformations");

            migrationBuilder.DropTable(
                name: "EndOfTreatments");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "StepOneModules");

            migrationBuilder.DropTable(
                name: "SupportUserOrders");

            migrationBuilder.DropTable(
                name: "EndOfTreatmentStatus");

            migrationBuilder.DropTable(
                name: "PrescriptionStatuses");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "SettelmentStatuses");

            migrationBuilder.DropTable(
                name: "FileUploadTypes");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "DeviceStatuses");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "SupportUsers");
        }
    }
}
