using xappido.Directory.ApiModels;

namespace aiala.Backend.ApiModels.Directory
{
    public class AppUserCreateModel : UserCreateModel
    {
        public string PhoneNumber { get; set; }

        public UserCreateRole Role { get; set; }
    }

    public enum UserCreateRole
    {
        EmergencyContact,
        MobileAppUser,
        WebAppUser,
        Administrator
    }
}
