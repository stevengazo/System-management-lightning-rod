﻿using System;
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
			var spSearchIncidents = $@"
		-- =============================================
-- Author:		Steven Fabricio Gazo Maliaño
-- Create date: 21-01-2022
-- Description:	Search rows in the table Incidents
-- =============================================
CREATE PROCEDURE [dbo].[searchIncidents]  
	-- Add the parameters for the stored procedure here
    @_DeviceId varchar(MAX) = null,
    @_Alias varchar(MAX) = null,
    @_Year varchar(MAX) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for procedure 
      DECLARE @_SQLCommand varchar(max) = 'SELECT Incidents.* FROM Incidents ';
    DECLARE @_Band1 BINARY = 0;
    IF( @_Alias IS NOT NULL AND @_DeviceId IS NOT NULL )
    BEGIN
        print('alias valid y device valid');
        SET @_Band1 = 1;
        SET @_SQLCommand = @_SQLCommand + ' INNER JOIN (SELECT * FROM Devices WHERE Devices.Alias LIKE ''%'   + @_Alias +'%'' AND  Devices.DeviceId LIKE ''%' + @_DeviceId + '%'') as D on Incidents.DeviceId = D.DeviceId ';
        PRINT(@_SQLCommand);
    END
    IF( @_Alias IS NULL AND @_DeviceId IS NOT NULL )
    BEGIN
        SET @_Band1 = 1;
        SET @_SQLCommand = @_SQLCommand + ' INNER JOIN (SELECT * FROM Devices WHERE  Devices.DeviceId LIKE ''%' + @_DeviceId + '%'') as D on Incidents.DeviceId = D.DeviceId ';
        PRINT(@_SQLCommand);
    END
    IF( @_Alias IS NOT NULL AND @_DeviceId IS  NULL )
    BEGIN
        SET @_Band1 = 1;
        SET @_SQLCommand = @_SQLCommand + ' INNER JOIN (SELECT * FROM Devices WHERE Devices.Alias LIKE ''%' + @_Alias     +'%'') as D on Incidents.DeviceId = D.DeviceId ';
    END    
    IF (@_Year IS NOT NULL)
    BEGIN
        SET @_Band1 = 1;
        SET @_SQLCommand = @_SQLCommand + ' WHERE YEAR(Incidents.IncidentDate) = ' + @_Year ;
        PRINT(@_SQLCommand);
    END
    if(@_Band1 = 1)
    BEGIN
        print(@_SQLCommand)
        EXECUTE (@_SQLCommand);        
    END
END

			";
			migrationBuilder.Sql(spSearchIncidents);
			var spUpdateRecomendedDate = $@"CREATE PROCEDURE UpdateRecomendedDateOfMaintenance
												@_DeviceId varchar(50) 
											AS
											BEGIN
												SET NOCOUNT ON;
												Declare @_sample Date = null;
												set @_sample = (SELECT TOP 1  Maintenances.MaintenanceDate 
															FROM Maintenances
															WHERE Maintenances.DeviceId = @_DeviceId
															ORDER BY MaintenanceDate DESC);

												if(@_sample =null)
													BEGIN
														PRINT('No hay fechas..');
													END
												ELSE
													BEGIN
														Set @_sample= DATEADD(year, 1, @_sample);
														SELECT @_sample;  
														UPDATE Devices
														SET RecomendedDateOfMaintenance = @_sample
														WHERE Devices.DeviceId = @_DeviceId
													END
												/*SELECT DeviceId, RecomendedDateOfMaintenance
												from Devices WHERE DeviceId =@_DeviceId*/
											END
											GO
											";
			migrationBuilder.Sql(spUpdateRecomendedDate);

			var spSeachDevice = $@"-- =============================================
									-- Author:		Steven Gazo
									-- Create date: 10/9/21
									-- Description:	Search a device by the parameters
									-- =============================================
									CREATE PROCEDURE SearchDevice
										-- Add the parameters for the stored procedure here
										@_DeviceId varchar(50) =null,
										@_Alias varchar(50) = null,
										@_Year varchar(5) = null,
										@_CountryId varchar(5)=null
									AS
									BEGIN
										-- SET NOCOUNT ON added to prevent extra result sets from
										-- interfering with SELECT statements.
										SET NOCOUNT ON;

										-- Insert statements for procedure here
										Declare @_sqlCommand varchar(max) =' SELECT * FROM Devices  ';
										DECLARE @_BAND binary = 0 ;
										DECLARE @_exec binary = 0;

										IF( @_DeviceId IS NOT NULL )
										BEGIN
											set @_sqlCommand = @_sqlCommand + ' WHERE DeviceId LIKE ''%' + @_DeviceId+'%''';
											set @_BAND = 1;
											set @_exec = 1;
										END
										IF( @_Alias IS NOT NULL)
										BEGIN
											IF( @_BAND =0)
												BEGIN
													set @_sqlCommand = @_sqlCommand + ' WHERE Alias LIKE ''%' + @_Alias+'%''';
													set @_BAND = 1;
													set @_exec = 1;
												END
											ELSE
												BEGIN
													set @_sqlCommand = @_sqlCommand + 'and Alias LIKE ''%' + @_Alias+'%''';
													set @_BAND = 1;
													set @_exec = 1;
												END
										END
										IF( @_Year IS NOT NULL)
										BEGIN
											IF(@_BAND =0)
												BEGIN
													set @_sqlCommand = @_sqlCommand + ' WHERE YEAR(InstallationDate) = '+ @_Year;
													set @_BAND = 1;
													set @_exec = 1;
												END
											ELSE
												BEGIN
													set @_sqlCommand = @_sqlCommand + ' AND YEAR(InstallationDate) = '+ @_Year;
													set @_BAND = 1;
													set @_exec = 1;
												END
										END
										IF( @_CountryId IS NOT NULL)
										BEGIN
											IF(@_BAND =0)
												BEGIN
													set @_sqlCommand = @_sqlCommand + ' WHERE CountryId =  '+ @_CountryId;
													set @_BAND = 1;
													set @_exec = 1;
												END
											ELSE
												BEGIN
													set @_sqlCommand = @_sqlCommand + ' AND CountryId =  '+ @_CountryId;
													set @_BAND = 1;
													set @_exec = 1;
												END
										END
										if @_exec = 1
										begin
											print (@_sqlCommand);
											EXEC (@_sqlCommand)
										end

									END
									";
			migrationBuilder.Sql(spSeachDevice);
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
						and Devices.IsActive = 1
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
