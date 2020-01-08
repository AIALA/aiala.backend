using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Models.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Mappings.Pictures
{
    public class UpdatePictureModelToUpdatePictureOperationsModelMapping : ModelMapping<(Guid id, UpdatePictureModel model), UpdateAiPictureMetadataOperationsModel>
    {
        protected override Task<UpdateAiPictureMetadataOperationsModel> OnMap((Guid id, UpdatePictureModel model) input, OperationContext context = null)
        {
            return Task.FromResult(new UpdateAiPictureMetadataOperationsModel
            {
                PictureId = input.id,
                Description = input.model.Description,
                AddedTags = input.model.AddedTags,
                UpdatedTags = input.model.UpdatedTags.Select(t => new UpdateAiPictureTagOperationsModel
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList(),
                RemovedTags = input.model.RemovedTags
            });
        }
    }
}
