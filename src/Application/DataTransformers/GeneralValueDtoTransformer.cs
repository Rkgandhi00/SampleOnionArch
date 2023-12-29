using Application.DataTransformers.Interface;
using Application.Dtos.Outbound;
using AutoMapper;
using Domain.DAL.Interface;
using Domain.Models;

namespace Application.DataTransformers
{
    public class GeneralValueDtoTransformer : AbstractDataTransformer, IGeneralValueDtoTransformer
    {
        private readonly IGeneralValueDAL _generalValueDAL;
        public GeneralValueDtoTransformer(
            IGeneralValueDAL generalValueDAL,
            IMapper mapper) : base(mapper)
        {
            _generalValueDAL = generalValueDAL;
        }

        public async Task<List<GeneralValuesDto>?> GetGeneralValues(Dictionary<string, object?> inputparam)
        {
            try
            {
                var generalValues = await _generalValueDAL.GetGeneralValues(inputparam);

                if (generalValues != null && generalValues.Count > 0)
                {
                    return Map<GeneralValues, GeneralValuesDto>(generalValues);
                }

                return null;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }
    }
}
