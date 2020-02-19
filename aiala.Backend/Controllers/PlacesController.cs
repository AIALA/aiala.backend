using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.ApiModels.Places;
using aiala.Backend.Authorization.Policies;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Places;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Mappings.Places;
using aiala.Backend.Operations.Pictures;
using aiala.Backend.Operations.Places;
using xappido.Authorization.Attributes;
using xappido.Authorization.Policies;
using xappido.Mvc.Attributes;
using xappido.Operations;
using xappido.Storage;

namespace aiala.Backend.Controllers
{
    [ApiController]
    [ApiVersion(1)]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "Places")]
    [Authorize]
    public class PlacesController : ControllerBase
    {
        private const string ContainerName = "placepictures";

        private readonly IOperationExecutor _executor;

        public PlacesController(IOperationExecutor executor)
        {
            _executor = executor;
        }

        /// <summary>
        /// Get Places
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizePolicy(typeof(ValidUserPolicy))]
        [ProducesResponseType(typeof(List<PlaceModel>), 200)]
        public async Task<IActionResult> GetPlaces()
        {
            var result = await _executor
                .Add<GetPlacesOperation>()
                .Map<PlaceToPlaceModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }

        /// <summary>
        /// Upsert Place
        /// </summary>
        /// <param name="place"></param>
        /// <returns>The upserted place</returns>
        [HttpPost]
        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [ProducesResponseType(typeof(PlaceModel), 200)]
        public async Task<IActionResult> UpsertPlace([FromBody] PlaceModel place)
        {
            var result = await _executor
                .Validate(ModelState)
                .Map<PlaceToPlaceModelMapping>(() => place)
                .Add<UpsertPlaceOperation, Place, Place>(p => p)
                .Map<PlaceToPlaceModelMapping>()
                .Execute();

            return result.CreateHttpResult();
        }

        [AuthorizePolicy(typeof(ScheduleManagementPolicy))]
        [HttpDelete("{placeId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeletePlace(Guid placeId)
        {
            var result = await _executor
                .Add<DeletePlaceOperation, Guid>(() => placeId)
                .Execute();

            return result.CreateHttpResult();
        }
    }
}
