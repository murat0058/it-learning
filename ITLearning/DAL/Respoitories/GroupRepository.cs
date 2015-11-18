using ITLearning.Frontend.Web.Common.Configs;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITLearning.Frontend.Web.DAL.Respoitories
{
    public class GroupRepository
    {
        private readonly IOptions<DatabaseConfiguration> _dbConfiguration;

        public GroupRepository(IOptions<DatabaseConfiguration> dbConfiguration)
        {
            _dbConfiguration = dbConfiguration;
        }
    }
}
