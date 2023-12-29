using Application.DataTransformers.Interface;
using Application.Dtos.Outbound;
using Infrastructure.Repositories.Interface;

namespace Infrastructure.Repositories
{
    public class GeneralValueRepository : IGeneralValueRepository
    {
        private readonly IGeneralValueDtoTransformer _transformer;
        public GeneralValueRepository(IGeneralValueDtoTransformer transformer)
        {
            _transformer = transformer;
        }

        public async Task<List<GeneralValuesDto>?> GetGeneralValues(int typeId)
        {
            try
            {
                var inputparam = new Dictionary<string, object?>();
                inputparam.Add("TypeId", typeId);

                return await _transformer.GetGeneralValues(inputparam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
