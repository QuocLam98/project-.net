using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Util;
using HomeDoctorSolution.Util.Entities;
using HomeDoctorSolution.Util.Hubs;
using System.ComponentModel;
using DateTimeConverter = HomeDoctorSolution.Util.DateTimeConverter;
var builder = WebApplication.CreateBuilder(args);
//Add scope
builder.Services
       .AddInfrastructure(builder.Configuration)
       .AddInfrastructureServices(builder.Configuration)
       .AddSignalR();

builder.Configuration.AddJsonFile("appsettings.json", true, true).AddEnvironmentVariables();
AppSettingConfig.Instance.SetConfiguration(builder.Configuration);
var app = builder.Build();
app.UseInfrastructure();

app.MapHub<AccountSendMessageHub>("/AccountSendMessageHub");
app.MapHub<ListConversationHub>("/ListConversation");
app.Run();
