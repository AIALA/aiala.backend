using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using aiala.Backend.Resources;
using xappido.Authorization;
using xappido.Operations;
using xappido.Storage;
using xappido.Storage.Azure;

namespace aiala.Backend.Operations.Pictures
{
    public class UploadPictureOperation : InputOutputOperation<(IFormFile picture, string container), (Guid id, string storageDirectLink)>
    {
        private readonly IStorageService _storageService;

        private static readonly string[] _supportedContentTypes = new[] { "image/png", "image/jpg", "image/jpeg" };

        public UploadPictureOperation(IStorageService storageService)
        {
            _storageService = storageService;
        }

        protected override async Task<IOperationResult> Execute((IFormFile picture, string container) input)
        {
            if (input.picture.Length <= 0)
            {
                return Invalid(ValidationMessages.ImageEmpty);
            }
            if (!_supportedContentTypes.Contains(input.picture.ContentType))
            {
                return Invalid(string.Format(ValidationMessages.ContentTypeNotSupported, input.picture.ContentType));
            }

            var id = Guid.NewGuid();
            var storageFile = new StorageFile(
                id.ToString(),
                input.picture.ContentType,
                input.picture.OpenReadStream(),
                new Dictionary<string, string>
                {
                    ["Author"] = Context.User.GetAccountId().ToString(),
                    ["Tenant"] = Context.User.GetTenantId().ToString()
                });
            var storageResult = await _storageService.Save(input.container, storageFile);

            return Succeeded((id, storageResult.Path));
        }
    }
}
