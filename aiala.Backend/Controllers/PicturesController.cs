using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Activities;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Activities;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Models.Pictures;
using aiala.Backend.Operations.Activities;
using aiala.Backend.Operations.Directory;
using aiala.Backend.Operations.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Authorization.Attributes;
using xappido.Mvc.Attributes;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Pictures")]
    [Authorize]
    public class PicturesController : ControllerBase
    {
        private const int UploadSizeLimit = 100 * 1024 * 1024; // 100MB

        private readonly IOperationExecutor _executor;

        public PicturesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Upload picture
        /// </summary>
        /// <returns></returns>
        [RequestSizeLimit(UploadSizeLimit)]
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpPost("{pictureType}")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UploadPicture(List<IFormFile> files, [FromRoute] PictureType pictureType, [FromQuery] Guid? accountId)
        {
            // Get form file in two ways. First for how the app transmits it, second for the webapp.
            var file = files.SingleOrDefault();
            if (file == null)
            {
                file = Request.Form.Files.SingleOrDefault();
            }
            if (file == null)
            {
                return BadRequest("Invalid Form");
            }

            _executor
                .Add<UploadPictureOperation, (IFormFile, string)>(() => (file, pictureType.ToString().ToLower()))
                .Add<AddPictureOperation, (Guid id, string link), (Guid, PictureType, string)>(r => (r.id, pictureType, r.link));

            if (pictureType == PictureType.Gallery)
            {
                Picture picture = null;
                _executor
                    .Add<AnalyzePictureOperation, Picture, Picture>(p => p)
                    .Add<CreatePictureActivityOperation, Picture, (Guid, ActivityType)>(p =>
                        {
                            picture = p;
                            return (p.Id, ActivityType.PictureAdded);
                        })
                    .Add<AddActivityOperation, PictureActivity, AddActivityOperation.Request>(a => new AddActivityOperation.Request(
                        new ActivityMetadataModel
                        {
                            ActiveStepId = null,
                            ActiveTaskId = null,
                            Latitude = null,
                            Longitude = null,
                            Timestamp = DateTimeOffset.UtcNow
                        },
                        a))
                    .Map<PictureToPictureModelMapping>(() => picture);
            }
            else
            {
                if (pictureType == PictureType.Profile)
                {
                    _executor.Add<SetProfilePictureOperation, Picture, (Picture, Guid)>(last => (last, accountId.Value));
                }

                _executor.Map<PictureToPictureModelMapping, Picture>(last => last);
            }

            var result = await _executor.Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Get Gallery
        /// </summary>
        /// <returns></returns>
        [AuthorizePolicy(typeof(GetSchedulePolicy))]
        [HttpGet("gallery")]
        [ProducesResponseType(typeof(AiPictureMetadataModel[]), 200)]
        public async Task<IActionResult> GetGallery()
        {
            var result = await _executor
                .Add<GetPicturesOperation, PictureType>(() => PictureType.Gallery)
                .Map<PictureToPictureModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Update Picture
        /// </summary>
        /// <param name="pictureId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpPut("gallery/{pictureId}")]
        [ProducesResponseType(typeof(AiPictureMetadataModel), 200)]
        public async Task<IActionResult> UpdatePicture(Guid pictureId, [FromBody] UpdatePictureModel model)
        {
            var result = await _executor
                .Map<UpdatePictureModelToUpdatePictureOperationsModelMapping>(() => (pictureId, model))
                .Add<UpdateAiPictureMetadataOperation, UpdateAiPictureMetadataOperationsModel, UpdateAiPictureMetadataOperationsModel>(last => last)
                .Map<PictureToPictureModelMapping, Picture>(last => last)
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Get Picture
        /// </summary>
        /// <param name="pictureType"></param>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{pictureType}/{pictureId}")]
        [ProducesResponseType(typeof(byte[]), 200)]
        public async Task<IActionResult> GetPicture([FromRoute] PictureType pictureType, [FromRoute] Guid pictureId)
        {
            var result = await _executor
                .Add<GetStoragePictureOperation, (string, Guid)>(() => (pictureType.ToString(), pictureId))
                .Execute();

            if (result.ResultType != OperationResultType.Succeeded)
            {
                return result.CreateHttpResult();
            }

            var storageFile = result.GetResult<StorageFile>();
            storageFile.Content.Position = 0;
            return File(storageFile.Content, storageFile.ContentType);
        }

        /// <summary>
        /// Delete Picture
        /// </summary>
        /// <param name="pictureId"></param>
        /// <returns></returns>
        [HttpDelete("{pictureId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeletePicture([FromRoute] Guid pictureId)
        {
            var result = await _executor
                .Add<DeletePictureOperation, Guid>(() => pictureId)
                .Execute();

            return result.CreateHttpResult();
        }
    }
}
