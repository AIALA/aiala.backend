using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aiala.Backend.ApiModels.Directory;
using aiala.Backend.ApiModels.Places;
using aiala.Backend.ApiModels.Schedule;
using aiala.Backend.Data.Pictures;
using aiala.Backend.Data.Schedule;
using aiala.Backend.Mappings.Pictures;
using aiala.Backend.Models.Schedule;
using aiala.Backend.Services;
using xappido.Directory.Services;
using xappido.Operations;

namespace aiala.Backend.Mappings.Schedule
{
    public class DayToDayModelMapping : ModelMapping<Day, DayModel>
    {
        private readonly IProfileService _profileService;
        private readonly PictureToPictureModelMapping _pictureMapping;
        private readonly IPictureHelperService _pictureHelperService;

        public DayToDayModelMapping(IProfileService profileService, PictureToPictureModelMapping pictureMapping, IPictureHelperService pictureHelperService)
        {
            _profileService = profileService;
            _pictureMapping = pictureMapping;
            _pictureHelperService = pictureHelperService;
        }

        protected override async Task<DayModel> OnMap(Day input, OperationContext context = null)
        {
            return new DayModel
            {
                Id = input.Id,
                IsTemporaryEntity = input is FilledDay,
                Date = input.Date,
                Name = input.Name,
                Tasks = (await Task.WhenAll(input.Tasks?.Select(async t => new ScheduledTaskModel
                {
                    Id = t.Id,
                    TaskId = t.Task.Id,
                    Name = t.Name,
                    Start = t.Start,
                    End = t.End,
                    DefaultDuration = t.DefaultDuration,
                    Place = t.Place == null ? null : new PlaceAppModel
                    {
                        Latitude = t.Place.Latitude,
                        Longitude = t.Place.Longitude,
                        Name = t.Place.Name,
                        Picture = t.Place.Picture != null ? await _pictureMapping.Map(t.Place.Picture) : _pictureHelperService.GetDefaultPictureModel(PictureType.PlacePictures),
                    },
                    FreeFormPlace = t.FreeFormPlace,
                    Picture = t.Picture != null ? await _pictureMapping.Map(t.Picture) : _pictureHelperService.GetDefaultPictureModel(PictureType.TaskPictures),
                    Feedback = t.Feedback,
                    State = GetTaskState(t),
                    ExpirationOffset = t.ExpirationOffset,
                    Steps = t.Steps.Select(s => new ScheduledStepModel
                    {
                        Id = s.Id,
                        Order = s.Order,
                        Text = s.Text,
                        State = s.State
                    }).ToList(),
                    EmergencyContacts = !t.UseTaskContacts ? new List<EmergencyContactModel>() : new[] { t.EmergencyContact1, t.EmergencyContact2 }
                        .Where(e => e != null)
                        .Select(e => new EmergencyContactModel
                        {
                            Name = $"{e.User.Firstname} {e.User.Lastname}",
                            PictureUrl = t.Picture != null ? _profileService.GetPictureUrl(t.Picture.Id) : null
                        }).ToList()
                }))).ToList()
            };
        }

        private StepState GetTaskState(ScheduledTask task)
        {
            if (task.Steps.Count == 0)
            {
                return task.Feedback == TaskFeedback.None ? StepState.Undone : StepState.Done;
            }

            return task.Steps.Any(s => s.State == StepState.Undone) ? StepState.Undone : StepState.Done;
        }
    }
}
