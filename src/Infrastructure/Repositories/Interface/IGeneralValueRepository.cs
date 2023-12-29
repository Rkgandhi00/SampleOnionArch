using Application.Dtos.Outbound;

namespace Infrastructure.Repositories.Interface
{
    public interface IGeneralValueRepository
    {
        Task<List<GeneralValuesDto>?> GetGeneralValues(int typeId);
    }
}
