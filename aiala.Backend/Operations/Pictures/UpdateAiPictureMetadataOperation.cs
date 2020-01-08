using Microsoft.EntityFrameworkCore;
using aiala.Backend.Data;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Models.Pictures;
using aiala.Backend.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using xappido.Operations;

namespace aiala.Backend.Operations.Pictures
{
    public class UpdateAiPictureMetadataOperation : InputOutputOperation<UpdateAiPictureMetadataOperationsModel, Picture>
    {
        private readonly AppDbContext _dbContext;

        public UpdateAiPictureMetadataOperation(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task<IOperationResult> Execute(UpdateAiPictureMetadataOperationsModel input)
        {
            var aiMetadata = (await _dbContext.Pictures
                .Include(p => p.AiMetadata)
                    .ThenInclude(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == input.PictureId))
                ?.AiMetadata;

            if (aiMetadata == null)
            {
                return NotFound(Messages.PictureNotFound);
            }

            if (aiMetadata.Description != input.Description)
            {
                aiMetadata.Description = input.Description;
                aiMetadata.HasHumanConfidence = true;
            }

            var tagsToRemove = aiMetadata.Tags.Where(et => input.RemovedTags.Any(rt => rt == et.Id)).ToList();
            _dbContext.AiPictureTags.RemoveRange(tagsToRemove);
            aiMetadata.Tags = aiMetadata.Tags.Where(et => !tagsToRemove.Contains(et)).ToList();

            foreach (var updatedTag in input.UpdatedTags)
            {
                var tag = aiMetadata.Tags.FirstOrDefault(t => t.Id == updatedTag.Id);
                if (tag == null)
                {
                    input.AddedTags.Add(updatedTag.Name);
                    continue;
                }

                var sanitizedName = SanitizeTagName(updatedTag.Name);
                var matchingTags = aiMetadata.Tags.Where(et => et.Name == sanitizedName);
                if (matchingTags.Any())
                {
                    // Remove potential duplicates. This one has human confidence, so the others aren't needed anymore.
                    _dbContext.AiPictureTags.RemoveRange(matchingTags);
                    aiMetadata.Tags = aiMetadata.Tags.Where(et => !matchingTags.Contains(et)).ToList();
                }

                tag.HasHumanConfidence = true;
                tag.Name = sanitizedName;
            }

            foreach (var addedTag in input.AddedTags)
            {
                var sanitizedName = SanitizeTagName(addedTag);

                if (aiMetadata.Tags.Any(t => t.Name == sanitizedName))
                {
                    continue;
                }

                aiMetadata.Tags.Add(new AiPictureTag
                {
                    Id = Guid.NewGuid(),
                    Name = SanitizeTagName(addedTag),
                    Confidence = 1,
                    HasHumanConfidence = true
                });
            }
            

            await _dbContext.SaveChangesAsync();

            return Succeeded(aiMetadata.Picture);
        }

        private static string SanitizeTagName(string tagName) => tagName.ToLower().Trim();
    }
}
