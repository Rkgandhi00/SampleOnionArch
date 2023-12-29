using Domain.Models;

namespace Domain.DAL.Interface
{
    public interface IGeneralValueDAL
    {
        Task<List<GeneralValues>?> GetGeneralValues(Dictionary<string, object?> inputparam);
    }
}
