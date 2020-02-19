using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Mappings.Pictures
{
    public class PictureToPictureModelMapping : ModelMapping<Picture, PictureModel>
    {
        private readonly IPictureHelperService _pictureHelperService;

        public PictureToPictureModelMapping(IPictureHelperService pictureHelperService)
        {
            _pictureHelperService = pictureHelperService;
        }

        protected override Task<PictureModel> OnMap(Picture input, OperationContext context = null)
        {
            return Task.FromResult(new PictureModel
            {
                Id = input.Id,
                CreatedAt = input.CreatedAt,
                PictureUrl = _pictureHelperService.GetPictureUrl(input, input.PictureType),
                AiMetadata = input.AiMetadata != null ? new AiPictureMetadataModel
                {
                    Id = input.AiMetadata.Id,
                    Description = input.AiMetadata?.Description,
                    DescriptionConfidence = input.AiMetadata?.HasHumanConfidence == true ? 1 : (input.AiMetadata?.DescriptionConfidence ?? 0f),
                    Tags = input.AiMetadata?.Tags.Select(t => new AiPictureTagModel
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Confidence = t.HasHumanConfidence ? 1 : t.Confidence
                    }).ToList() ?? new List<AiPictureTagModel>()
                } : null
            });
        }
    }
}
