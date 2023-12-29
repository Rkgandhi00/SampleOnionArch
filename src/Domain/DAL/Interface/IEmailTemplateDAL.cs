using Domain.Models;

namespace Domain.DAL.Interface
{
    public interface IEmailTemplateDAL
    {
        Task<Email> Get(Dictionary<string, object?> inputParam);
    }
}
