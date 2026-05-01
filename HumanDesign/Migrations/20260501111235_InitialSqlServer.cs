using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanDesign.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlServer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    referral_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "attributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imgno = table.Column<int>(name: "img-no", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attributes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "center",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    definition = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_center", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "channel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gate_a = table.Column<int>(type: "int", nullable: false),
                    gate_b = table.Column<int>(type: "int", nullable: false),
                    theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gift = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shadow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cross",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cross1 = table.Column<int>(type: "int", nullable: true),
                    cross2 = table.Column<int>(type: "int", nullable: true),
                    cross3 = table.Column<int>(type: "int", nullable: true),
                    cross4 = table.Column<int>(type: "int", nullable: true),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cross", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    size = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gate",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gate", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code1 = table.Column<int>(type: "int", nullable: false),
                    code2 = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strategy = table.Column<int>(type: "int", nullable: true),
                    characteristic = table.Column<int>(type: "int", nullable: true),
                    theme = table.Column<int>(type: "int", nullable: true),
                    preview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variable_arrow",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    is_left = table.Column<bool>(type: "bit", nullable: false),
                    color = table.Column<int>(type: "int", nullable: false),
                    tone = table.Column<int>(type: "int", nullable: false),
                    @base = table.Column<int>(name: "base", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variable_arrow", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prospects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDateLocal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prospects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prospects_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "design",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    definition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    incarnation_cross = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_design", x => x.id);
                    table.ForeignKey(
                        name: "FK_design_prospects_name",
                        column: x => x.name,
                        principalTable: "prospects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "center_definition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CenterName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_center_definition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_center_definition_design_DesignId",
                        column: x => x.DesignId,
                        principalTable: "design",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "defined_channels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    design_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    channel_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gate_a = table.Column<int>(type: "int", nullable: false),
                    gate_b = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_defined_channels", x => x.id);
                    table.ForeignKey(
                        name: "FK_defined_channels_design_design_id",
                        column: x => x.design_id,
                        principalTable: "design",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "planetary_activations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    design_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    planet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gate = table.Column<int>(type: "int", nullable: false),
                    line = table.Column<int>(type: "int", nullable: false),
                    color = table.Column<int>(type: "int", nullable: false),
                    tone = table.Column<int>(type: "int", nullable: false),
                    @base = table.Column<int>(name: "base", type: "int", nullable: false),
                    fixation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planetary_activations", x => x.id);
                    table.ForeignKey(
                        name: "FK_planetary_activations_design_design_id",
                        column: x => x.design_id,
                        principalTable: "design",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "variables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    design_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    digestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    reasoning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cognition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motivation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    perspective = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    environment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DigestionArrowId = table.Column<int>(type: "int", nullable: false),
                    EnvironmentArrowId = table.Column<int>(type: "int", nullable: false),
                    AwarenessArrowId = table.Column<int>(type: "int", nullable: false),
                    PerspectiveArrowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variables", x => x.id);
                    table.ForeignKey(
                        name: "FK_variables_design_design_id",
                        column: x => x.design_id,
                        principalTable: "design",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_variables_variable_arrow_AwarenessArrowId",
                        column: x => x.AwarenessArrowId,
                        principalTable: "variable_arrow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variables_variable_arrow_DigestionArrowId",
                        column: x => x.DigestionArrowId,
                        principalTable: "variable_arrow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variables_variable_arrow_EnvironmentArrowId",
                        column: x => x.EnvironmentArrowId,
                        principalTable: "variable_arrow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_variables_variable_arrow_PerspectiveArrowId",
                        column: x => x.PerspectiveArrowId,
                        principalTable: "variable_arrow",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ParentId",
                table: "AspNetUsers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_center_name_definition",
                table: "center",
                columns: new[] { "name", "definition" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_center_definition_DesignId_CenterName",
                table: "center_definition",
                columns: new[] { "DesignId", "CenterName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_defined_channels_design_id",
                table: "defined_channels",
                column: "design_id");

            migrationBuilder.CreateIndex(
                name: "IX_design_name",
                table: "design",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_planetary_activations_design_id",
                table: "planetary_activations",
                column: "design_id");

            migrationBuilder.CreateIndex(
                name: "IX_prospects_OwnerId",
                table: "prospects",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_AwarenessArrowId",
                table: "variables",
                column: "AwarenessArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_design_id",
                table: "variables",
                column: "design_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_variables_DigestionArrowId",
                table: "variables",
                column: "DigestionArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_EnvironmentArrowId",
                table: "variables",
                column: "EnvironmentArrowId");

            migrationBuilder.CreateIndex(
                name: "IX_variables_PerspectiveArrowId",
                table: "variables",
                column: "PerspectiveArrowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "attributes");

            migrationBuilder.DropTable(
                name: "center");

            migrationBuilder.DropTable(
                name: "center_definition");

            migrationBuilder.DropTable(
                name: "channel");

            migrationBuilder.DropTable(
                name: "cross");

            migrationBuilder.DropTable(
                name: "defined_channels");

            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "gate");

            migrationBuilder.DropTable(
                name: "planetary_activations");

            migrationBuilder.DropTable(
                name: "profile");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "variables");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "design");

            migrationBuilder.DropTable(
                name: "variable_arrow");

            migrationBuilder.DropTable(
                name: "prospects");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
