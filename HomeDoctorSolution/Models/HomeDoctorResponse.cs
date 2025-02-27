using Microsoft.AspNetCore.Mvc;

namespace HomeDoctorSolution.Models
{
    public class HomeDoctorResponse : IActionResult
    {
        public static string STATUS_SUCCESS = "200";
        public string status { get; set; }
        public string message { get; set; }
        public string value { get; set; }
        public IList<Object> data { get; set; }
        public object resources { get; set; }




        public HomeDoctorResponse(string status, string message, IList<Object> data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }
        public HomeDoctorResponse(string status, string message, object data)
        {
            this.status = status;
            this.message = message;
            this.resources = data;
        }
        public HomeDoctorResponse(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

        public HomeDoctorResponse()
        {
        }

        public static HomeDoctorResponse Success<T>(T? data, string? message = "SUCCESS") where T : class
        {
            return new HomeDoctorResponse()
            {
                status = "200",
                message = message,
                resources = data
            };
        }
        public static HomeDoctorResponse SUCCESS(IList<Object> data)
        {
            return new HomeDoctorResponse("200", "SUCCESS", data);
        }

        public static HomeDoctorResponse SUCCESS(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("200", "SUCCESS", returnData);
        }
        public static HomeDoctorResponse BAD_REQUEST(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("400", "BAD_REQUEST", returnData);
        }

        public static HomeDoctorResponse BAD_REQUEST()
        {
            List<Object> returnData = new List<Object>();
            var obj = new { Code = 400, Message = "BAD_REQUEST" };
            returnData.Add(obj);
            return new HomeDoctorResponse("400", "BAD_REQUEST", returnData);
        }
        public static HomeDoctorResponse Error(string status, string title, object errors)
        {
            return new HomeDoctorResponse()
            {
                status = status,
                message = title,
                resources = errors
            };
        }
        public static HomeDoctorResponse BadRequest(object errors)
        {
            return Error("403", "BadRequest", errors);
        }
        public static HomeDoctorResponse SUCCESS()
        {
            return new HomeDoctorResponse("200", "SUCCESS");
        }
        //trả về SUCCESSNOTBIDDING trong hoàn tiền đặt cọc
        //public static HomeDoctorResponse SUCCESSNOTBIDDING(Object data)
        //{
        //    List<Object> returnData = new List<Object>();
        //    returnData.Add(data);
        //    return new HomeDoctorResponse("205", "SUCCESSNOTBIDDING", returnData);
        //}
        public static HomeDoctorResponse SUCCESSNOTBIDDING(IList<Object> data)
        {
            return new HomeDoctorResponse("205", "SUCCESSNOTBIDDING", data);
        }
        //trả về SUCCESSHAVEBIDDING trong hoàn tiền đặt cọc
        //public static HomeDoctorResponse SUCCESSHAVEBIDDING(Object data)
        //{
        //    List<Object> returnData = new List<Object>();
        //    returnData.Add(data);
        //    return new HomeDoctorResponse("206", "SUCCESSHAVEBIDDING", returnData);
        //}
        public static HomeDoctorResponse SUCCESSHAVEBIDDING(IList<Object> data)
        {
            return new HomeDoctorResponse("206", "SUCCESSHAVEBIDDING", data);
        }

        public static HomeDoctorResponse CREATED(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("201", "CREATED", returnData);
        }

        public static HomeDoctorResponse Faild()
        {
            return new HomeDoctorResponse("099", "FAILD");
        }
        public static HomeDoctorResponse UNAUTHORIZED()
        {
            return new HomeDoctorResponse("401", "UNAUTHORIZED");
        }
        public static HomeDoctorResponse BiddingFaildEnded(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("096", "BIDDINGFAILD", returnData);
        }

        public static HomeDoctorResponse EmailExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("202", "EMAILEXIST", data);
        }
        public static HomeDoctorResponse EmailNotValid(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("204", "EMAILNOTVALID");
        }
        public static HomeDoctorResponse UsernameExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("203", "USENAMEEXIST");
        }
        public static HomeDoctorResponse IdCardNumberExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("205", "IDCARNUMBEREXIST");
        }
        public static HomeDoctorResponse BiddingRequestExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("203", "BIDDINGREQUESTEXIST", returnData);
        }
        public static HomeDoctorResponse PhoneExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("204", "PHONEEXIST");
        }
        public static HomeDoctorResponse CompanyIdExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("206", "COMPANYIDEXIST");
        }
        public static HomeDoctorResponse ItemExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("203", "ITEMEXIST", returnData);
        }
        public static HomeDoctorResponse NotFoundBiddingMax()
        {
            return new HomeDoctorResponse("999", "FAILD");
        }
        public static HomeDoctorResponse PostNameExist()
        {
            return new HomeDoctorResponse("203", "POSTNAMEEXIST");
        }
        public static HomeDoctorResponse NotFoundBiddingSecond()
        {
            return new HomeDoctorResponse("998", "FAILD");
        }
        public static HomeDoctorResponse PasswordExist(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("202", "PASSWORDEXIST", returnData);
        }
        public static HomeDoctorResponse PasswordIsNotFormat(Object data)
        {
            List<Object> returnData = new List<Object>();
            returnData.Add(data);
            return new HomeDoctorResponse("205", "PASSWORDISNOTINCORRECTFORMAT", returnData);
        }

        public static HomeDoctorResponse OTP_REQUIRED(IList<Object> data)
        {
            return new HomeDoctorResponse("200", "OTP_REQUIRED", data);
        }
        public static HomeDoctorResponse OTP_OVER_LIMIT(IList<Object> data)
        {
            return new HomeDoctorResponse("099", "OTP_OVER_LIMIT", data);
        }
        public static HomeDoctorResponse OTP_INVALID_DATA(IList<Object> data)
        {
            return new HomeDoctorResponse("098", "INVALID_DATA", data);
        }
        public static HomeDoctorResponse OTP_EXIST()
        {
            return new HomeDoctorResponse("204", "OTP_EXIST");
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
