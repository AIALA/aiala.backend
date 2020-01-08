using aiala.Backend.ApiModels.Pictures;
using System.Collections.Generic;
using xappido.Directory.ApiModels;

namespace aiala.Backend.ApiModels.Directory
{
    public class UserDetailModel : UserListModel
    {
        public string PhoneNumber { get; set; }

        public List<string> PermissionGroups { get; set; }

        public PictureModel Picture { get; set; }
    }
}
