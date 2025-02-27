using System;
using System.Collections.Generic;
using System.Data;
using HomeDoctor.Models;
using HomeDoctorSolution.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeDoctorSolution.Models
{
    public partial class HomeDoctorContext : DbContext
    {
        public HomeDoctorContext()
        {
        }

        public HomeDoctorContext(DbContextOptions<HomeDoctorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AccountMeta> AccountMeta { get; set; } = null!;
        public virtual DbSet<AccountStatus> AccountStatuses { get; set; } = null!;
        public virtual DbSet<AccountType> AccountTypes { get; set; } = null!;
        public virtual DbSet<ActivityLog> ActivityLogs { get; set; } = null!;
        public virtual DbSet<Anamnesis> Anamneses { get; set; } = null!;
        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingStatus> BookingStatuses { get; set; } = null!;
        public virtual DbSet<BookingType> BookingTypes { get; set; } = null!;
        public virtual DbSet<BookingtMeta> BookingtMeta { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<CartProduct> CartProducts { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<CommentStatus> CommentStatuses { get; set; } = null!;
        public virtual DbSet<Consultant> Consultants { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<ContactStatus> ContactStatuses { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Doctor> Doctor { get; set; } = null!;
        public virtual DbSet<DoctorStatus> DoctorStatuses { get; set; } = null!;
        public virtual DbSet<DoctorType> DoctorTypes { get; set; } = null!;
        public virtual DbSet<Email> Emails { get; set; } = null!;
        public virtual DbSet<Entity> Entities { get; set; } = null!;
        public virtual DbSet<FavouritePost> FavouritePosts { get; set; } = null!;
        public virtual DbSet<FeaturedPost> FeaturedPosts { get; set; } = null!;
        public virtual DbSet<FeaturedPostType> FeaturedPostTypes { get; set; } = null!;
        public virtual DbSet<FolderUpload> FolderUploads { get; set; } = null!;
        public virtual DbSet<HealthFacility> HealthFacilities { get; set; } = null!;
        public virtual DbSet<HealthFacilityStatus> HealthFacilityStatuses { get; set; } = null!;
        public virtual DbSet<HealthFacilityType> HealthFacilityTypes { get; set; } = null!;
        public virtual DbSet<HomepageContent> HomepageContents { get; set; } = null!;
        public virtual DbSet<HomepageContentMeta> HomepageContentMeta { get; set; } = null!;
        public virtual DbSet<HomepageContentType> HomepageContentTypes { get; set; } = null!;
        public virtual DbSet<LanguageConfig> LanguageConfigs { get; set; } = null!;
        public virtual DbSet<MedicalProfile> MedicalProfiles { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuType> MenuTypes { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; } = null!;
        public virtual DbSet<MessageType> MessageTypes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationStatus> NotificationStatuses { get; set; } = null!;
        public virtual DbSet<OnlineStatus> OnlineStatuses { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<OrderDetailStatus> OrderDetailStatuses { get; set; } = null!;
        public virtual DbSet<OrderPaymentStatus> OrderPaymentStatuses { get; set; } = null!;
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<OrderStatusShip> OrderStatusShips { get; set; } = null!;
        public virtual DbSet<OrderType> OrderTypes { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<OrganizationStatus> OrganizationStatuses { get; set; } = null!;
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostCategory> PostCategories { get; set; } = null!;
        public virtual DbSet<PostCommentStatus> PostCommentStatuses { get; set; } = null!;
        public virtual DbSet<PostLayout> PostLayouts { get; set; } = null!;
        public virtual DbSet<PostMeta> PostMeta { get; set; } = null!;
        public virtual DbSet<PostPublishStatus> PostPublishStatuses { get; set; } = null!;
        public virtual DbSet<PostTag> PostTags { get; set; } = null!;
        public virtual DbSet<PostType> PostTypes { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductBrand> ProductBrands { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductStatus> ProductStatuses { get; set; } = null!;
        public virtual DbSet<ProductMeta> ProductMetas { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<PromotionMeta> PromotionMeta { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionType> QuestionTypes { get; set; } = null!;
        public virtual DbSet<ReadedPost> ReadedPosts { get; set; } = null!;
        public virtual DbSet<Right> Rights { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleMenu> RoleMenus { get; set; } = null!;
        public virtual DbSet<RoleRight> RoleRights { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ShipAddress> ShipAddresses { get; set; } = null!;
        public virtual DbSet<Subscribe> Subscribes { get; set; } = null!;
        public virtual DbSet<Survey> Surveys { get; set; } = null!;
        public virtual DbSet<SurveyAccount> SurveyAccounts { get; set; } = null!;
        public virtual DbSet<SurveyAccountShareLink> SurveyAccountShareLinks { get; set; } = null!;
        public virtual DbSet<SurveyMeta> SurveyMeta { get; set; } = null!;
        public virtual DbSet<SurveySection> SurveySections { get; set; } = null!;
        public virtual DbSet<SurveySectionAccount> SurveySectionAccounts { get; set; } = null!;
        public virtual DbSet<SurveySectionAccountDetail> SurveySectionAccountDetails { get; set; } = null!;
        public virtual DbSet<SurveySectionQuestion> SurveySectionQuestions { get; set; } = null!;
        public virtual DbSet<SurveyStatus> SurveyStatuses { get; set; } = null!;
        public virtual DbSet<SurveyType> SurveyTypes { get; set; } = null!;
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Transaction> Transaction { get; set; } = null!;
        public virtual DbSet<TransactionMeta> TransactionMeta { get; set; } = null!;
        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; } = null!;
        public virtual DbSet<TransactionType> TransactionTypes { get; set; } = null!;
        public virtual DbSet<UploadFiles> UploadFiles { get; set; } = null!;
        public virtual DbSet<Voucher> Vouchers { get; set; } = null!;
        public virtual DbSet<VoucherStatus> VoucherStatuses { get; set; } = null!;
        public virtual DbSet<VoucherType> VoucherTypes { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=103.214.9.237;Database=HomeDoctorSolution;UID=homedoctor.dev;PWD=Abc123456;Trusted_Connection=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(CustomQuery).GetMethod(nameof(CustomQuery.ToCustomString))).HasTranslation(
              e =>
              {
                  return new SqlFunctionExpression(functionName: "format", arguments: new[]{
                                 e.First(),
                                 new SqlFragmentExpression("'dd/MM/yyyy HH:mm:ss'")
                          }, nullable: true, new List<bool>(), type: typeof(string), typeMapping: new StringTypeMapping("", DbType.String));
              });

            modelBuilder.HasDbFunction(typeof(CustomQuery).GetMethod(nameof(CustomQuery.ToDateString))).HasTranslation(
                e =>
                {
                    return new SqlFunctionExpression(functionName: "format", arguments: new[]{
                                 e.First(),
                                 new SqlFragmentExpression("'dd/MM/yyyy'")
                            }, nullable: true, new List<bool>(), type: typeof(string), typeMapping: new StringTypeMapping("", DbType.String));
                });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Address).HasColumnType("ntext");

                entity.Property(e => e.AppleId)
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FacebookId).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(255);

                entity.Property(e => e.GoogleId).HasMaxLength(255);

                entity.Property(e => e.GuId).HasMaxLength(255);

                entity.Property(e => e.HealthFacilityId).HasColumnName("HealthFacilityID");

                entity.Property(e => e.IdCardGrantedDate).HasMaxLength(255);

                entity.Property(e => e.IdCardGrantedPlace).HasMaxLength(255);

                entity.Property(e => e.IdCardNumber).HasMaxLength(255);

                entity.Property(e => e.IdCardPhoto1).HasMaxLength(255);

                entity.Property(e => e.IdCardPhoto2).HasMaxLength(255);

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MiddleName).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(255);

                entity.Property(e => e.Zipcode).HasMaxLength(255);

                entity.HasOne(d => d.AccountStatus)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_AccountStatus");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_account_accountTypeId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_account_roleId");
            });

            modelBuilder.Entity<AccountMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountMeta)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountMeta_Account");
            });

            modelBuilder.Entity<AccountStatus>(entity =>
            {
                entity.ToTable("AccountStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLog");

                entity.Property(e => e.Browser).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Device).HasMaxLength(255);

                entity.Property(e => e.EntityCode).HasMaxLength(255);

                entity.Property(e => e.GuId).HasMaxLength(255);

                entity.Property(e => e.Info).HasMaxLength(255);

                entity.Property(e => e.IpAddress).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Os).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ActivityLogs)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActivityLog_Account");
            });

            modelBuilder.Entity<Anamnesis>(entity =>
            {
                entity.ToTable("Anamnesis");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Anamnesis)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Anamnesis_Account");
            });

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Question");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("Author");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Info).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Author_Account");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.BookingStatus)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BookingStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_booking_bookingStatusId");

                entity.HasOne(d => d.BookingType)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BookingTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_booking_bookingTypeId");
            });

            modelBuilder.Entity<BookingStatus>(entity =>
            {
                entity.ToTable("BookingStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<BookingType>(entity =>
            {
                entity.ToTable("BookingType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<BookingtMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingtMeta)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bookingtMeta_bookingId");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cart_Account");
            });

            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.ToTable("CartProduct");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProduct_Cart");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CartProduct_Product");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.HasOne(d => d.CommentStatus)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.CommentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_commentStatusId");
            });

            modelBuilder.Entity<CommentStatus>(entity =>
            {
                entity.ToTable("CommentStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.ToTable("Consultant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Message).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.HasOne(d => d.ContactStatus)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_contact_contactStatusId");
            });

            modelBuilder.Entity<ContactStatus>(entity =>
            {
                entity.ToTable("ContactStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.HasIndex(e => e.CountryCode, "UQ__Country__5D9B0D2C86712AA6")
                    .IsUnique();

                entity.Property(e => e.CountryCode).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_district_provinceId");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctors");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_Account");

                entity.HasOne(d => d.DoctorStatus)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.DoctorStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_DoctorStatus");

                entity.HasOne(d => d.DoctorType)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.DoctorTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_DoctorType");

                entity.HasOne(d => d.HealthFacility)
                    .WithMany(p => p.Doctor)
                    .HasForeignKey(d => d.HealthFacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctors_HealthFacility");
            });

            modelBuilder.Entity<DoctorStatus>(entity =>
            {
                entity.ToTable("DoctorStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<DoctorType>(entity =>
            {
                entity.ToTable("DoctorType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Email>(entity =>
            {
                entity.ToTable("Email");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Source).HasMaxLength(255);
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.ToTable("Entity");

                entity.HasIndex(e => e.EntityId, "UQ__Entity__9C892F9C38CDF0D8")
                    .IsUnique();

                entity.Property(e => e.CodeName).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<FavouritePost>(entity =>
            {
                entity.ToTable("FavouritePost");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FavouritePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_favouritePost_postID");
            });

            modelBuilder.Entity<FeaturedPost>(entity =>
            {
                entity.ToTable("FeaturedPost");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.FeaturedPostType)
                    .WithMany(p => p.FeaturedPosts)
                    .HasForeignKey(d => d.FeaturedPostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_featuredPost_featuredPostTypeId");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FeaturedPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_featuredPost_postId");
            });

            modelBuilder.Entity<FeaturedPostType>(entity =>
            {
                entity.ToTable("FeaturedPostType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<FolderUpload>(entity =>
            {
                entity.ToTable("FolderUpload");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<HealthFacility>(entity =>
            {
                entity.ToTable("HealthFacility");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FoundedYear).HasColumnType("datetime");

                entity.HasOne(d => d.HealthFacilityStatus)
                    .WithMany(p => p.HealthFacilities)
                    .HasForeignKey(d => d.HealthFacilityStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HealthFacility_HealthFacilityStatus");

                entity.HasOne(d => d.HealthFacilityType)
                    .WithMany(p => p.HealthFacilities)
                    .HasForeignKey(d => d.HealthFacilityTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HealthFacility_HealthFacilityType");
            });

            modelBuilder.Entity<HealthFacilityStatus>(entity =>
            {
                entity.ToTable("HealthFacilityStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<HealthFacilityType>(entity =>
            {
                entity.ToTable("HealthFacilityType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<HomepageContent>(entity =>
            {
                entity.ToTable("HomepageContent");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.HomepageContentType)
                    .WithMany(p => p.HomepageContents)
                    .HasForeignKey(d => d.HomepageContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_homepageContent_homepageContentTypeId");
            });

            modelBuilder.Entity<HomepageContentMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.HomepageContent)
                    .WithMany(p => p.HomepageContentMeta)
                    .HasForeignKey(d => d.HomepageContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_homepageContentMeta_homepageContentId");
            });

            modelBuilder.Entity<HomepageContentType>(entity =>
            {
                entity.ToTable("HomepageContentType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<LanguageConfig>(entity =>
            {
                entity.ToTable("LanguageConfig");

                entity.HasIndex(e => e.Code, "UQ__Language__A25C5AA7F234814F")
                    .IsUnique();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<MedicalProfile>(entity =>
            {
                entity.ToTable("MedicalProfile");

                entity.Property(e => e.Bmi).HasColumnName("Bmi");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.MedicalProfiles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicalProfile_Account");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MenuType)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.MenuTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menu_menuTypeId");
            });

            modelBuilder.Entity<MenuType>(entity =>
            {
                entity.ToTable("MenuType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Account");

                entity.HasOne(d => d.MessageStatus)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_message_messageStatusId");

                entity.HasOne(d => d.MessageType)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_message_messageTypeId");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Room");
            });

            modelBuilder.Entity<MessageStatus>(entity =>
            {
                entity.ToTable("MessageStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<MessageType>(entity =>
            {
                entity.ToTable("MessageType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.NotificationStatus)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notification_notificationStatusId");
            });

            modelBuilder.Entity<NotificationStatus>(entity =>
            {
                entity.ToTable("NotificationStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<OnlineStatus>(entity =>
            {
                entity.ToTable("OnlineStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastOnlineTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.OnlineStatuses)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_onlineStatus_accountId");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ShipRecipientPhone).HasMaxLength(15);

                entity.Property(e => e.Tax).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalShipFee).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.OrderPaymentStatus)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderPaymentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderPaymentStatus");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderStatus");

                entity.HasOne(d => d.OrderType)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderType");

                entity.HasOne(d => d.Account)
                  .WithMany(p => p.Orders)
                  .HasForeignKey(d => d.AccountId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Orders_Account");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.OrderDetailStatus)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderDetailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_OrderDetailStatus");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<OrderDetailStatus>(entity =>
            {
                entity.ToTable("OrderDetailStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderPaymentStatus>(entity =>
            {
                entity.ToTable("OrderPaymentStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderStatusShip>(entity =>
            {
                entity.ToTable("OrderStatusShip");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.ToTable("OrderType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.ToTable("Organization");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.HasOne(d => d.OrganizationStatus)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_organization_organizationStatusId");

                entity.HasOne(d => d.OrganizationType)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_organization_organizationTypeId");
            });

            modelBuilder.Entity<OrganizationStatus>(entity =>
            {
                entity.ToTable("OrganizationStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<OrganizationType>(entity =>
            {
                entity.ToTable("OrganizationType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.HasIndex(e => e.Url, "UQ__Post__C5B214319480D842")
                    .IsUnique();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GuId).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.PublishedTime).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_authorId");

                entity.HasOne(d => d.PostCategory)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_postCategoryId");

                entity.HasOne(d => d.PostCommentStatus)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostCommentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_postCommentStatusId");

                entity.HasOne(d => d.PostLayout)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostLayoutId)
                    .HasConstraintName("FK_post_postLayoutId");

                entity.HasOne(d => d.PostPublishStatus)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostPublishStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_postPublishStatusId");

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_postTypeId");
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.ToTable("PostCategory");

                entity.HasIndex(e => e.Slug, "UQ__PostCate__BC7B5FB60B405C9B")
                    .IsUnique();

                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(255);
            });

            modelBuilder.Entity<PostCommentStatus>(entity =>
            {
                entity.ToTable("PostCommentStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<PostLayout>(entity =>
            {
                entity.ToTable("PostLayout");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<PostMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostMeta)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postMeta_postId");
            });

            modelBuilder.Entity<PostPublishStatus>(entity =>
            {
                entity.ToTable("PostPublishStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.ToTable("PostTag");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postTag_postId");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTags)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_postTag_tagId");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ProductBrand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductBrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductBrand");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");

                entity.HasOne(d => d.ProductStatus)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductStatus");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductType");
            });

            modelBuilder.Entity<ProductMeta>(entity =>
            {
                entity.ToTable("ProductMeta");
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductBrand>(entity =>
            {
                entity.ToTable("ProductBrand");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductStatus>(entity =>
            {
                entity.ToTable("ProductStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<PromotionMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.PromotionMeta)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PromotionMeta_PromotionMeta");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                //entity.HasOne(d => d.Country)
                //    .WithMany(p => p.Provinces)
                //    .HasForeignKey(d => d.CountryId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_province_countryId");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .HasConstraintName("FK_question_questionTypeId");
            });

            modelBuilder.Entity<QuestionType>(entity =>
            {
                entity.ToTable("QuestionType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ReadedPost>(entity =>
            {
                entity.ToTable("ReadedPost");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ReadedPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_readedPost_postID");
            });

            modelBuilder.Entity<Right>(entity =>
            {
                entity.HasIndex(e => e.Code, "UQ__Rights__A25C5AA7DEAD31C7")
                    .IsUnique();

                entity.Property(e => e.Code).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.CodeName, "UQ__Role__404488D54B7E0242")
                    .IsUnique();

                entity.Property(e => e.CodeName).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<RoleMenu>(entity =>
            {
                entity.ToTable("RoleMenu");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roleMenu_menuId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleMenus)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roleMenu_roleId");
            });

            modelBuilder.Entity<RoleRight>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Right)
                    .WithMany(p => p.RoleRight)
                    .HasForeignKey(d => d.RightsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roleRights_rightsId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleRight)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_roleRights_roleId");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.HealthFacility)
                    .WithMany(p => p.Services)
                    .HasForeignKey(d => d.HealthFacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Services_HealthFacility");
            });

            modelBuilder.Entity<ShipAddress>(entity =>
            {
                entity.ToTable("ShipAddress");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.RecipientPhoneNumber).HasMaxLength(10);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShipAddress_Account");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_ShipAddress_District");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_ShipAddress_Province");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.ShipAddresses)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_ShipAddress_Ward");
            });

            modelBuilder.Entity<Subscribe>(entity =>
            {
                entity.ToTable("Subscribe");

                entity.HasIndex(e => e.Email, "UQ__Subscrib__A9D10534FC9D96BB")
                    .IsUnique();

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Survey");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Photo).HasMaxLength(255);

                entity.Property(e => e.PublishedTime).HasColumnType("datetime");

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.Property(e => e.Video).HasMaxLength(255);

                entity.HasOne(d => d.SurveyStatus)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.SurveyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_survey_surveyStatusId");

                entity.HasOne(d => d.SurveyType)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.SurveyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_survey_surveyTypeId");
            });

            modelBuilder.Entity<SurveyAccount>(entity =>
            {
                entity.ToTable("SurveyAccount");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyAccounts)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveyAccount_surveyId");
            });

            modelBuilder.Entity<SurveyAccountShareLink>(entity =>
            {
                entity.ToTable("SurveyAccountShareLink");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<SurveyMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Key).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyMeta)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveyMeta_surveyId");
            });

            modelBuilder.Entity<SurveySection>(entity =>
            {
                entity.ToTable("SurveySection");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveySections)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveySection_surveyId");
            });

            modelBuilder.Entity<SurveySectionAccount>(entity =>
            {
                entity.ToTable("SurveySectionAccount");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.SurveyAccount)
                    .WithMany(p => p.SurveySectionAccounts)
                    .HasForeignKey(d => d.SurveyAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveySectionAccount_surveyAccountId");
            });

            modelBuilder.Entity<SurveySectionAccountDetail>(entity =>
            {
                entity.ToTable("SurveySectionAccountDetail");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveySectionAccountDetails)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveySectionAccountDetail_Question");
            });

            modelBuilder.Entity<SurveySectionQuestion>(entity =>
            {
                entity.ToTable("SurveySectionQuestion");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveySectionQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveySectionQuestion_questionId");

                entity.HasOne(d => d.SurveySection)
                    .WithMany(p => p.SurveySectionQuestions)
                    .HasForeignKey(d => d.SurveySectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_surveySectionQuestion_surveySectionId");
            });

            modelBuilder.Entity<SurveyStatus>(entity =>
            {
                entity.ToTable("SurveyStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<SurveyType>(entity =>
            {
                entity.ToTable("SurveyType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.ToTable("SystemConfig");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.HasIndex(e => e.Slug, "UQ__Tag__BC7B5FB6BA4B3327")
                    .IsUnique();

                entity.Property(e => e.Color).HasMaxLength(255);

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Slug).HasMaxLength(255);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Orders");

                entity.HasOne(d => d.TransactionStatus)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.TransactionStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_TransactionStatus");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_TransactionType");
            });

            modelBuilder.Entity<TransactionMeta>(entity =>
            {
                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionMeta)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransactionMeta_Transactions");
            });

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.ToTable("TransactionStatus");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<UploadFiles>(entity =>
            {
                entity.ToTable("UploadFile");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.UploadFiles)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UploadFile_Account");

                entity.HasOne(d => d.FolderUpload)
                    .WithMany(p => p.UploadFiles)
                    .HasForeignKey(d => d.FolderUploadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UploadFile_FolderUpload");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_Voucher_Promotion");

                entity.HasOne(d => d.VoucherStatus)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.VoucherStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_VoucherStatus");

                entity.HasOne(d => d.VoucherType)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.VoucherTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Voucher_VoucherType");
            });

            modelBuilder.Entity<VoucherStatus>(entity =>
            {
                entity.ToTable("VoucherStatus");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<VoucherType>(entity =>
            {
                entity.ToTable("VoucherType");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.ToTable("Ward");

                entity.Property(e => e.CreatedTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Wards)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ward_districtId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
