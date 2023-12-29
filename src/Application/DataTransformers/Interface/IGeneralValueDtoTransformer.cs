using Application.Dtos.Outbound;

namespace Application.DataTransformers.Interface
{
    public interface IGeneralValueDtoTransformer
    {
        Task<List<GeneralValuesDto>?> GetGeneralValues(Dictionary<string, object?> inputparam);
    }
}
