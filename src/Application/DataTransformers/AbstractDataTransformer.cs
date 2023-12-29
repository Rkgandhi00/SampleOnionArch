using AutoMapper;

namespace Application.DataTransformers
{
    public abstract class AbstractDataTransformer
    {
        private readonly IMapper _mapper;

        public AbstractDataTransformer(
            IMapper mapper)
        {
            _mapper = mapper;
        }

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        protected List<TDestination> Map<TSource, TDestination>(List<TSource> source)
        {
            var destinationRecords = new List<TDestination>();
            source.ForEach(x => { destinationRecords.Add(Map<TSource, TDestination>(x)); });
            return destinationRecords;
        }
    }
}
