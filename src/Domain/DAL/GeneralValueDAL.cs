using Common.Config;
using Domain.DAL.Interface;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Domain.DAL
{
    public class GeneralValueDAL : DataBaseOperations, IGeneralValueDAL
    {
        private readonly DataBaseSettings _databaseSettings;
        public GeneralValueDAL(IOptions<DataBaseSettings> opts)
        {
            _databaseSettings = opts.Value;
        }

        public async Task<List<GeneralValues>?> GetGeneralValues(Dictionary<string, object?> inputparam)
        {
            try
            {
                var generalValues = await ExecuteStoreProcedure<GeneralValues>(_databaseSettings.YOUR_DBConnectionString, "pr_get_generalvalues", inputparam);
                return generalValues;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
