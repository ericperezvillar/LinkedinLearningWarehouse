using AutoMapper;
using LinkedinLearningWarehouse.DTOs.LearningAsset;
using LinkedinLearningWarehouse.Interfaces.LearningAsset;
using LinkedinLearningWarehouse.Models.LearningAsset;

namespace LinkedinLearningWarehouse.Mappers.Resolvers
{
    public class ClassificationTypeResolver : IValueResolver<ClassificationDto, ClassificationDetail, int?>
    {
        private readonly IClassificationTypeService _classificationTypeService;

        public ClassificationTypeResolver(IClassificationTypeService classificationTypeService)
        {
            _classificationTypeService = classificationTypeService;
        }

        public int? Resolve(ClassificationDto source, ClassificationDetail destination, int? destMember, ResolutionContext context)
        {
            return _classificationTypeService.GetClassificationTypeIdAsync(source.Type).Result;
        }
    }
}
