using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Data.Pictures;
using xappido.Directory;
using xappido.Operations;

namespace aiala.Backend.Services
{
    public class PictureHelperService : IPictureHelperService
    {
        private readonly IOptions<DirectoryOptions> _directoryOptions;
        private readonly IServiceProvider _serviceProvider;

        public PictureHelperService(IOptions<DirectoryOptions> directoryOptions, IServiceProvider serviceProvider)
        {
            _directoryOptions = directoryOptions;
            _serviceProvider = serviceProvider;
        }

        public string GetPictureUrl(Picture picture, PictureType pictureType)
        {
            var url = _directoryOptions.Value.ApiBaseUrl;
            if (!url.EndsWith("/"))
            {
                url += "/";
            }

            if (picture == null)
            {
                return $"{url}images/defaults/{pictureType}.png";
            }

            return $"{url}api/v1/pictures/{(int)pictureType}/{picture.Id}";
        }

        public PictureModel GetDefaultPictureModel(PictureType pictureType)
        {
            return new PictureModel
            {
                CreatedAt = DateTimeOffset.UtcNow,
                PictureUrl = GetPictureUrl(null, pictureType)
            };
        }

        public async Task<PictureModel> MapViaOperation(Picture picture)
        {
            var result = await _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IOperationExecutor>()
                .Map<Picture, object>((o, c) => picture)
                .Execute();

            if (result.ResultType == OperationResultType.Succeeded)
            {
                return result.GetResult<PictureModel>();
            }

            throw result.Error;
        }
    }
}
