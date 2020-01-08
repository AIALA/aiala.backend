using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.ApiModels.Refit;
using System;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;
using xappido.Storage;
using Refit;
using Microsoft.Extensions.Options;
using aiala.Backend.Options;

namespace aiala.Backend.Operations.Pictures
{
    public class AnalyzePictureOperation : InputOutputOperation<Picture, Picture>
    {
        const string CognitiveEndpoint = "https://northeurope.api.cognitive.microsoft.com/vision/v2.0/analyze";
        const string Features = "Tags,Description";
        private readonly AzureVisionOptions _options;
        private readonly AppDbContext _dbContext;

        public AnalyzePictureOperation(AppDbContext dbContext, IOptions<AzureVisionOptions> options)
        {
            _options = options.Value;
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(Picture input)
        {
            var pictureResult = await Execute<GetStoragePictureOperation>((input.PictureType.ToString(), input.Id));
            if (pictureResult.ResultType != OperationResultType.Succeeded)
            {
                return pictureResult;
            }

            var file = pictureResult.GetResult<StorageFile>();
            VisionAnalysisModel analysisModel;

            var visionApi = RestService.For<IAzureVisionApi>(CognitiveEndpoint);
            try
            {
                var body = new VisionUrlBody
                {
                    Url = input.StorageDirectLink
                };
                analysisModel = await visionApi.PostVisionAnalysis(body, Features, _options.Key);
            }
            catch (ApiException e)
            {
                return e.AsOperationResult(this);
            }

            input.AiMetadata = new AiPictureMetadata();
            input.AiMetadata.Tags = analysisModel.Tags
                .Where(t => _options.TagBlacklist?.Contains(t.Name) != true)
                .OrderByDescending(t => t.Confidence)
                .Where(t => t.Confidence > _options.TagsConfidenceThreshold)
                .Take(5)
                .Select(t => new AiPictureTag
                {
                    Id = Guid.NewGuid(),
                    Name = t.Name,
                    Confidence = t.Confidence
                })
                .ToList();

            var caption = analysisModel.Description.Captions
                .OrderByDescending(c => c.Confidence)
                .Where(c => c.Confidence > _options.CaptionsConfidenceThreshold)
                .FirstOrDefault();
            input.AiMetadata.Description = caption.Text;
            input.AiMetadata.DescriptionConfidence = caption.Confidence;

            await _dbContext.SaveChangesAsync();

            return Succeeded(input);
        }
    }
}
