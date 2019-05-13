using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Models.DBModels;
using Tests_server_app.Models.ViewModels;
using Tests_server_app.Services.Database;

namespace Tests_server_app.Services.DatabaseServ
{
    public interface IDatabaseService : IUserDatabaseService, ITestDatabaseService, 
        IThemeDatabaseService, IRoleDatabaseService
    {
        // contains all methods to interact with database
        
    }
}
