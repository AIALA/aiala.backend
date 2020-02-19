using System;
using System.Threading.Tasks;
using aiala.Backend.Resources;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Operations.Pictures
{
    public class DeleteStoragePictureOperation : InputOperation<(string containerName, Guid pictureId)>
    {
        private readonly IStorageService _storageService;

        public DeleteStoragePictureOperation(IStorageService storageService)
        {
            _storageService = storageService;
        }

        protected override async Task<IOperationResult> Execute((string containerName, Guid pictureId) input)
        {
            var result = await _storageService.Delete(input.containerName.ToLower(), input.pictureId.ToString());

            return result ? Succeeded() : Failed(Messages.DeletePictureFailed);
        }
    }
}
