﻿namespace HomeDoctorSolution.Constants
{
    public class AdminAccountConst
    {
        public const string DEFAULT_PHOTO = "/uploads/admin/default_avatar.jpg";
        /// <summary>
        /// super admin id
        /// </summary>
        public const int SUPER_ADMIN_ID = 1001;
        public const int ADMIN_ID = 1002;

        public const int COURSELORS = 1000002;
        public const int STUDENTS = 1000003;

        public static int[] ADMIN_ROLE = new int[] { SUPER_ADMIN_ID, ADMIN_ID };
        public static class Status
        {
            public const int Active = 1000001;
            public const int InActive = 1000002;
        }

        //default account counselor
        public const int COUNSELOR_DEFAULT = 1000003;
    }
}
