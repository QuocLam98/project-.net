using HomeDoctorSolution.Services.Interfaces;
using Quartz;

namespace HomeDoctorSolution.Services.SendEmailQuarztJob
{
    
    public class SendEmailReminder : IJob
    {
        private readonly IBookingService bookingService;

        public SendEmailReminder(IBookingService _bookingService)
        {
            _bookingService = bookingService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            //bookingService.SendEmailReminderBooking();
            Console.WriteLine("MyService is doing something.");
            return Task.CompletedTask;
        }
    }
}
