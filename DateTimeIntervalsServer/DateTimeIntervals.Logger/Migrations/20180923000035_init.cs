using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DateTimeIntervals.Logger.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RemoteAddr = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    ResponseStatus = table.Column<int>(nullable: false),
                    RequestMethod = table.Column<string>(nullable: true),
                    RequestTimestamp = table.Column<string>(nullable: true),
                    RequestPath = table.Column<string>(nullable: true),
                    RequestProtocol = table.Column<string>(nullable: true),
                    UserAgent = table.Column<string>(nullable: true),
                    DurationMs = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogData");
        }
    }
}
