﻿namespace HomeDoctorSolution.Models.ModelDTO
{
    public class SignInWithSocialNetworkDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
