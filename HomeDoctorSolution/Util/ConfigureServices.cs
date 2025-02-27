using AutoMapper;
using HomeDoctorSolution.Models;
using HomeDoctorSolution.Repository;
using HomeDoctorSolution.Repository.Interfaces;
using HomeDoctorSolution.Repository.UploadFile;
using HomeDoctorSolution.Repository.UploadFile.Interfaces;
using HomeDoctorSolution.Services;
using HomeDoctorSolution.Services.Interfaces;
using HomeDoctorSolution.Services.UploadFile;
using HomeDoctorSolution.Services.UploadFile.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Quartz;
using HomeDoctorSolution.Services.SendEmailQuarztJob;
using HomeDoctor.Repository.Interfaces;
using HomeDoctor.Repository;
using HomeDoctor.Services.Interfaces;
using HomeDoctor.Services;

namespace HomeDoctorSolution.Util
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HomeDoctorContext>(options => options.UseSqlServer(configuration.GetConnectionString("DBConnection"),
                builder => builder.MigrationsAssembly(typeof(HomeDoctorContext).Assembly.FullName)));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Add repository
            #region
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAccountMetaRepository, AccountMetaRepository>();
            services.AddScoped<IAccountMetaService, AccountMetaService>();

            services.AddScoped<IAccountStatusRepository, AccountStatusRepository>();
            services.AddScoped<IAccountStatusService, AccountStatusService>();

            services.AddScoped<IAccountTypeRepository, AccountTypeRepository>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();

            services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
            services.AddScoped<IActivityLogService, ActivityLogService>();

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();

            services.AddScoped<IBookingStatusRepository, BookingStatusRepository>();
            services.AddScoped<IBookingStatusService, BookingStatusService>();

            services.AddScoped<IBookingTypeRepository, BookingTypeRepository>();
            services.AddScoped<IBookingTypeService, BookingTypeService>();

            services.AddScoped<IBookingtMetaRepository, BookingtMetaRepository>();
            services.AddScoped<IBookingtMetaService, BookingtMetaService>();

            services.AddScoped<IConsultantRepository, ConsultantRepository>();
            services.AddScoped<IConsultantService, ConsultantService>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<ICommentStatusRepository, CommentStatusRepository>();
            services.AddScoped<ICommentStatusService, CommentStatusService>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IContactStatusRepository, ContactStatusRepository>();
            services.AddScoped<IContactStatusService, ContactStatusService>();

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IDistrictService, DistrictService>();

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IEntityService, EntityService>();

            services.AddScoped<IFavouritePostRepository, FavouritePostRepository>();
            services.AddScoped<IFavouritePostService, FavouritePostService>();

            services.AddScoped<IFeaturedPostRepository, FeaturedPostRepository>();
            services.AddScoped<IFeaturedPostService, FeaturedPostService>();

            services.AddScoped<IFeaturedPostTypeRepository, FeaturedPostTypeRepository>();
            services.AddScoped<IFeaturedPostTypeService, FeaturedPostTypeService>();

            services.AddScoped<IHomepageContentRepository, HomepageContentRepository>();
            services.AddScoped<IHomepageContentService, HomepageContentService>();

            services.AddScoped<IHomepageContentTypeRepository, HomepageContentTypeRepository>();
            services.AddScoped<IHomepageContentTypeService, HomepageContentTypeService>();

            services.AddScoped<IHomepageContentMetaRepository, HomepageContentMetaRepository>();
            services.AddScoped<IHomepageContentMetaService, HomepageContentMetaService>();

            services.AddScoped<ILanguageConfigRepository, LanguageConfigRepository>();
            services.AddScoped<ILanguageConfigService, LanguageConfigService>();

            services.AddScoped<IMenuTypeRepository, MenuTypeRepository>();
            services.AddScoped<IMenuTypeService, MenuTypeService>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMenuService, MenuService>();

            services.AddScoped<IHomepageContentTypeRepository, HomepageContentTypeRepository>();
            services.AddScoped<IHomepageContentTypeService, HomepageContentTypeService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IRoomService, RoomService>();

            services.AddScoped<IMessageStatusRepository, MessageStatusRepository>();
            services.AddScoped<IMessageStatusService, MessageStatusService>();

            services.AddScoped<IMessageTypeRepository, MessageTypeRepository>();
            services.AddScoped<IMessageTypeService, MessageTypeService>();

            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<INotificationStatusRepository, NotificationStatusRepository>();
            services.AddScoped<INotificationStatusService, NotificationStatusService>();

            services.AddScoped<IOnlineStatusRepository, OnlineStatusRepository>();
            services.AddScoped<IOnlineStatusService, OnlineStatusService>();

            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            services.AddScoped<IOrganizationStatusRepository, OrganizationStatusRepository>();
            services.AddScoped<IOrganizationStatusService, OrganizationStatusService>();

            services.AddScoped<IOrganizationTypeRepository, OrganizationTypeRepository>();
            services.AddScoped<IOrganizationTypeService, OrganizationTypeService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IPostCategoryRepository, PostCategoryRepository>();
            services.AddScoped<IPostCategoryService, PostCategoryService>();

            services.AddScoped<IPostCommentStatusRepository, PostCommentStatusRepository>();
            services.AddScoped<IPostCommentStatusService, PostCommentStatusService>();

            services.AddScoped<IPostLayoutRepository, PostLayoutRepository>();
            services.AddScoped<IPostLayoutService, PostLayoutService>();

            services.AddScoped<IPostMetaRepository, PostMetaRepository>();
            services.AddScoped<IPostMetaService, PostMetaService>();

            services.AddScoped<IPostPublishStatusRepository, PostPublishStatusRepository>();
            services.AddScoped<IPostPublishStatusService, PostPublishStatusService>();

            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IPostTagService, PostTagService>();

            services.AddScoped<IPostTypeRepository, PostTypeRepository>();
            services.AddScoped<IPostTypeService, PostTypeService>();

            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceService, ProvinceService>();

            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddScoped<IQuestionTypeService, QuestionTypeService>();

            services.AddScoped<IReadedPostRepository, ReadedPostRepository>();
            services.AddScoped<IReadedPostService, ReadedPostService>();

            services.AddScoped<IRightsRepository, RightsRepository>();
            services.AddScoped<IRightsService, RightsService>();

            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IRoleMenuRepository, RoleMenuRepository>();
            services.AddScoped<IRoleMenuService, RoleMenuService>();

            services.AddScoped<IRoleRightsRepository, RoleRightsRepository>();
            services.AddScoped<IRoleRightsService, RoleRightsService>();

            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<ISubscribeService, SubscribeService>();

            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyService, SurveyService>();

            services.AddScoped<ISurveyAccountRepository, SurveyAccountRepository>();
            services.AddScoped<ISurveyAccountService, SurveyAccountService>();

            services.AddScoped<ISurveyMetaRepository, SurveyMetaRepository>();
            services.AddScoped<ISurveyMetaService, SurveyMetaService>();

            services.AddScoped<ISurveySectionRepository, SurveySectionRepository>();
            services.AddScoped<ISurveySectionService, SurveySectionService>();

            services.AddScoped<ISurveySectionAccountRepository, SurveySectionAccountRepository>();
            services.AddScoped<ISurveySectionAccountService, SurveySectionAccountService>();

            services.AddScoped<ISurveySectionAccountDetailRepository, SurveySectionAccountDetailRepository>();
            services.AddScoped<ISurveySectionAccountDetailService, SurveySectionAccountDetailService>();

            services.AddScoped<ISurveySectionQuestionRepository, SurveySectionQuestionRepository>();
            services.AddScoped<ISurveySectionQuestionService, SurveySectionQuestionService>();

            services.AddScoped<ISurveyStatusRepository, SurveyStatusRepository>();
            services.AddScoped<ISurveyStatusService, SurveyStatusService>();

            services.AddScoped<ISurveyTypeRepository, SurveyTypeRepository>();
            services.AddScoped<ISurveyTypeService, SurveyTypeService>();

            services.AddScoped<ISystemConfigRepository, SystemConfigRepository>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            services.AddScoped<IWardRepository, WardRepository>();
            services.AddScoped<IWardService, WardService>();


            services.AddScoped<IHealthFacilityRepository, HealthFacilityRepository>();
            services.AddScoped<IHealthFacilityService, HealthFacilityService>();

            services.AddScoped<IHealthFacilityTypeRepository, HealthFacilityTypeRepository>();
            services.AddScoped<IHealthFacilityTypeService, HealthFacilityTypeService>();

            services.AddScoped<IHealthFacilityStatusRepository, HealthFacilityStatusRepository>();
            services.AddScoped<IHealthFacilityStatusService, HealthFacilityStatusService>();

            services.AddScoped<IMedicalProfileRepository, MedicalProfileRepository>();
            services.AddScoped<IMedicalProfileService, MedicalProfileService>();

            services.AddScoped<IAnamnesisRepository, AnamnesisRepository>();
            services.AddScoped<IAnamnesisService, AnamnesisService>();

            services.AddScoped<ISurveyAccountShareLinkRepository, SurveyAccountShareLinkRepository>();
            services.AddScoped<ISurveyAccountShareLinkService, SurveyAccountShareLinkService>();

            services.AddScoped<IDoctorsRepository, DoctorsRepository>();
            services.AddScoped<IDoctorsService, DoctorsService>();

            services.AddScoped<IDoctorTypeRepository, DoctorTypeRepository>();
            services.AddScoped<IDoctorTypeService, DoctorTypeService>();

            services.AddScoped<IDoctorStatusRepository, DoctorStatusRepository>();
            services.AddScoped<IDoctorStatusService, DoctorStatusService>();

            services.AddScoped<IServicesRepository, ServicesRepository>();
            services.AddScoped<IServicesService, ServicesService>();

            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartService, CartService>();

            services.AddScoped<ICartProductRepository, CartProductRepository>();
            services.AddScoped<ICartProductService, CartProductService>();

            services.AddScoped<IProductBrandRepository, ProductBrandRepository>();
            services.AddScoped<IProductBrandService, ProductBrandService>();

            services.AddScoped<IProductMetaRepository, ProductMetaRepository>();
            services.AddScoped<IProductMetaService, ProductMetaService>();

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();

            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductTypeService, ProductTypeService>();

            services.AddScoped<IProductStatusRepository, ProductStatusRepository>();
            services.AddScoped<IProductStatusService, ProductStatusService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderDetailStatusRepository, OrderDetailStatusRepository>();
            services.AddScoped<IOrderDetailStatusService, OrderDetailStatusService>();

            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();

            services.AddScoped<IOrderStatusShipRepository, OrderStatusShipRepository>();
            services.AddScoped<IOrderStatusShipService, OrderStatusShipService>();

            services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
            services.AddScoped<IOrderStatusService, OrderStatusService>();

            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IOrderTypeService, OrderTypeService>();

            services.AddScoped<IOrderPaymentStatusRepository, OrderPaymentStatusRepository>();
            services.AddScoped<IOrderPaymentStatusService, OrderPaymentStatusService>();

            services.AddScoped<IVoucherStatusRepository, VoucherStatusRepository>();
            services.AddScoped<IVoucherStatusService, VoucherStatusService>();

            services.AddScoped<IVoucherTypeRepository, VoucherTypeRepository>();
            services.AddScoped<IVoucherTypeService, VoucherTypeService>();

            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IPromotionService, PromotionService>();

            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherService, VoucherService>();

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IOrdersService, OrdersService>();

            services.AddScoped<IShipAddressRepository, ShipAddressRepository>();
            services.AddScoped<IShipAddressService, ShipAddressService>();

            services.AddScoped<ITransactionStatusRepository, TransactionStatusRepository>();
            services.AddScoped<ITransactionStatusService, TransactionStatusService>();

            services.AddScoped<ITransactionTypeRepository, TransactionTypeRepository>();
            services.AddScoped<ITransactionTypeService, TransactionTypeService>();

            services.AddScoped<ITransactionsRepository, TransactionsRepository>();
            services.AddScoped<ITransactionsService, TransactionsService>();

            services.AddScoped<ITransactionMetaRepository, TransactionMetaRepository>();
            services.AddScoped<ITransactionMetaService, TransactionMetaService>();
            #endregion
            //Add UploadFile
            services.AddScoped<IFileExplorerService, FileExplorerService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IUploadFileRepository, UploadFileRepository>();
            services.AddScoped<IFolderUploadRepository, FolderUploadRepository>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            //Token service
            services.AddScoped<ITokenService, TokenService>();

            //add service quarzt
            //services.AddQuartz(q =>
            //{
            //    //q.UseMicrosoftDependencyInjectionScopedJobFactory();
            //    q.AddJob<SendEmailReminder>("da");
            //    q.AddTrigger(t => t.ForJob("myQuartzJob").WithIdentity("myQuartzJobTrigger").StartNow());
            //});
            //services.AddQuartzHostedService(options =>
            //{
            //    options.WaitForJobsToComplete = true;
            //});

            return services;

        }
    }
}
