using aiala.Backend.ApiModels.Pictures;
using aiala.Backend.Data.Pictures;
using System;
using System.Threading.Tasks;

namespace aiala.Backend.Services
{
    public interface IPictureHelperService
    {
        string GetPictureUrl(Picture picture, PictureType pictureType);

        PictureModel GetDefaultPictureModel(PictureType pictureType);

        Task<PictureModel> MapViaOperation(Picture picture);
    }
}
