using Microsoft.EntityFrameworkCore.Migrations;

namespace Reserva.Data.Migrations
{
    public partial class SPgetContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR REPLACE FUNCTION public.getcontact(reservation_id integer)
            RETURNS TABLE(Name character varying, COntactType text, Phone text)
            LANGUAGE plpgsql
            AS $function$
                BEGIN
                    return QUERY select ""Name"", ""ContactType"", ""Phone"" from ""Contacts"" b where ""Id"" = reservation_id;
                END
                $function$
            ;";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
