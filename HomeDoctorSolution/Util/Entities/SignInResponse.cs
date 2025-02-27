using HomeDoctorSolution.Models.ModelDTO;

namespace HomeDoctorSolution.Util.Entities
{
    public class SignIningResponse
    {
        /// <summary>
        /// Authorization token
        /// </summary>
        public string AccessToken { get; set; } = null!;

        public string Data { get; set; } = null!;

    }
    public class SignInResponse
    {
        /// <summary>
        /// Authorization token
        /// </summary>
        /// 
        public string AccessToken { get; set; } = null!;
        public AccountProfileResponseDTO Profile { get; set; } = null!;
        public int CountBookingWait { get; set; } = 0;

    }
}
