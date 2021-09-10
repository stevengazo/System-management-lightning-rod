using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;


namespace DataAccess
{
    public static class StoredProcedure 
    {
        public static void ExecuteSP(MigrationBuilder migrationBuilder)
        {
            #region Stored Procedures
            var spSearchClient = @"
					-- =============================================
					-- Author:		Steven Gazo
					-- Create date: 9/9/21
					-- Description:	Search a client in the database
					-- =============================================
					Create  PROCEDURE SearchClients
					-- Add the parameters for the stored procedure here
					@_idToSearch varchar(100) =null,
					@_name varchar(40),
					@_SectorId varchar =0
					AS
					BEGIN
						-- SET NOCOUNT ON added to prevent extra result sets from
						-- interfering with SELECT statements.
						SET NOCOUNT ON;

						-- Insert statements for procedure here
							Declare @_sqlCommand varchar(max) =' SELECT * FROM Clients ';
						DECLARE @_BAND binary = 0 ;
						DECLARE @_exec binary = 0;
						if @_idToSearch IS NOT NULL
						BEGIN
								begin
									set @_sqlCommand = @_sqlCommand + (' WHERE Id like ''%'+ @_idToSearch+'%''') ;
									set @_BAND = 1;
									set @_exec = 1;
								end
						END
						if @_Name is not null
						BEGIN
							if @_BAND = 0
								begin
									set @_sqlCommand = @_sqlCommand + (' WHERE Name like  ''%'+ @_name + '%''') ;
									set @_BAND = 1;
									set @_exec = 1;			
								end
							else 
								begin
									set @_sqlCommand = @_sqlCommand + (' and Name like  ''%'+ @_name + '%''') ;
									set @_BAND = 1;
									set @_exec = 1;			
								end
						END
						if @_SectorId != 0
						BEGIN
							if @_BAND = 0
								begin
									set @_sqlCommand = @_sqlCommand + (' WHERE SectorId = '+ @_SectorId) ;
									set @_BAND = 1;
									set @_exec = 1;			
								end
							else
								begin
									set @_sqlCommand = @_sqlCommand + (' and SectorId = '+ @_SectorId) ;
									set @_BAND = 1;
									set @_exec = 1;						
								end
						END
						if @_exec = 1
						begin
							print (@_sqlCommand);
							EXEC (@_sqlCommand)
						end

					END

					";
            var SP_M_SelectByDeviceId = @"   CREATE PROCEDURE GetMaintenanceByDeviceId
	                                            @_DeviceId varchar(50) 
                                            AS
                                            BEGIN
                                                SET NOCOUNT ON;
                                                SELECT * 
                                                FROM Maintenances
                                                WHERE Maintenances.DeviceId =@_DeviceId
                                            END
                                    ";

            string SP_D_M_ByYear = @"
                CREATE PROCEDURE GetDeviceByUnreallizedMaintenanceByYear 
	                @_Year varchar(4) = '1'
                AS
                BEGIN
	            SET NOCOUNT ON;
                -- Insert statements for procedure here
	                Select Devices.*
	                from Devices left join (Select * 
							                From Maintenances
							                Where YEAR(MaintenanceDate) = @_Year) as ListMaintenance
	                on Devices.DeviceId = ListMaintenance.DeviceId
	                where ListMaintenance.MaintenanceId is  null
                end
                                        ";
            string sp1 = @"
                CREATE PROCEDURE SearchDeviceByDeviceId
	                @_DeviceSearch varchar(450) = '0'
                AS
                BEGIN
	                SET NOCOUNT ON;
                    -- Insert statements for procedure here
	                Select *
	                FROM Devices
	                WHERE Devices.DeviceId like CONCAT('%',@_DeviceSearch,'%')
                END
                GO
            ";
            string sp2 = @"
            create PROCEDURE SearchClientByName
	            -- Add the parameters for the stored procedure here
	            @_Name varchar(30) ='sample'
            AS
            BEGIN
	            SET NOCOUNT ON;
                -- Insert statements for procedure here
	            SELECT *
	            FROM Clients
	            WHERE Clients.Name LIKE  ('%'+@_Name+'%')
            END
            ";
			migrationBuilder.Sql(spSearchClient);
            migrationBuilder.Sql(sp1);
            migrationBuilder.Sql(sp2);
            migrationBuilder.Sql(SP_M_SelectByDeviceId);
            migrationBuilder.Sql(SP_D_M_ByYear);
            #endregion
        }
    }
}
