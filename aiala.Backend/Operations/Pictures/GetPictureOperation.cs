using System;
using System.Threading.Tasks;
using aiala.Backend.Resources;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Operations.Pictures
{
    public class GetStoragePictureOperation : InputOutputOperation<(string container, Guid id), StorageFile>
    {
        private readonly IStorageService _storageService;

        public GetStoragePictureOperation(IStorageService storageService)
        {
            _storageService = storageService;
        }

        protected override async Task<IOperationResult> Execute((string container, Guid id) input)
        {
            var file = await _storageService.Get(input.container.ToLower(), input.id.ToString());
            return file == null ? NotFound(Messages.PictureNotFound) : Succeeded(file);
        }
    }
}
