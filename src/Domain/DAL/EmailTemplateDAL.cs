using Common.Config;
using Domain.DAL.Interface;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Domain.DAL
{
    public class EmailTemplateDAL : DataBaseOperations, IEmailTemplateDAL
    {
        private readonly DataBaseSettings _databaseSettings;

        public EmailTemplateDAL(IOptions<DataBaseSettings> opts)
        {
            _databaseSettings = opts.Value;
        }

        public async Task<Email> Get(Dictionary<string, object?> inputParam)
        {
            try
            {
                var result = await ExecuteStoreProcedure<Email>(_databaseSettings.YOUR_DBConnectionString, "pr_get_EmailTemplate", inputParam);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }
    }
}
