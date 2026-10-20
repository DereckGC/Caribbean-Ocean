using CaribbeanOcean.API.Middlewares;
using CaribbeanOcean.API.Services;
using CaribbeanOcean.Application.Services;
using CaribbeanOcean.Application.Settings;
using CaribbeanOcean.Application.UseCases;
using CaribbeanOcean.Application.UseCases.Authentication;
using CaribbeanOcean.Application.UseCases.Gallery;
using CaribbeanOcean.Domain.Interfaces;
using CaribbeanOcean.Infraestructure.Data;
using CaribbeanOcean.Infraestructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ── Cultura ──────────────────────────────────────────────────────────────────
var culture = new System.Globalization.CultureInfo("en-US");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

// ── Configuración tipada ─
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;

// ── Controladores y OpenAPI ───────────────────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("Token-Expired"));
});

// ── JWT ───────────────────────────────────────────────────────────────────────
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(jwt.Secret)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = ctx =>
            {
                if (ctx.Exception is SecurityTokenExpiredException)
                    ctx.Response.Headers.Append("Token-Expired", "true");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

// ── Base de datos ─────────────────────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DbConnectionFactory(connectionString!));

// ── Repositorios ──────────────────────────────────────────────────────────────
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddScoped<IPageContentRepository, PageContentRepository>();
builder.Services.AddScoped<IHotelContactRepository, HotelContactRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();
builder.Services.AddScoped<IAddRepository, AddRepository>();
builder.Services.AddScoped<IReservationNightDetailRepository, ReservationNightDetailRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

// ── Servicios de autenticación ────────────────────────────────────────────────
builder.Services.AddScoped<Register>();                     
builder.Services.AddScoped<Authenticate>();               
builder.Services.AddScoped<AuthService>();

// ── Servicios de aplicación ───────────────────────────────────────────────────
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<AboutUsService>();
builder.Services.AddScoped<RoomTypeService>();
builder.Services.AddScoped<HeroService>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddScoped<HotelContactService>();
builder.Services.AddScoped<OfferService>();
builder.Services.AddScoped<AddService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<FacilityService>();
builder.Services.AddScoped<GalleryService>();
builder.Services.AddScoped<SeasonService>();
builder.Services.AddScoped<ReservationPricingService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ActivityLogService>();


// ── Casos de uso ──────────────────────────────────────────────────────────────
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.CreateReservation>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.UpdateReservation>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetAllReservations>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetReservationById>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.CheckRoomReservationAvailability>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Reservation.CheckRoomReservationAvailabilityForUpdate>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetAllAvailableRoomTypes>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetAvailableRoomTypeById>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.CreateRoomType>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetAllAvailableRoomTypesCount>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.GetAllRoomTypesUseCase>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.AboutUs.GetAboutUs>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.AboutUs.UpdateAboutUs>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.DeleteReservation>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.GetHero>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.UpdateRoomType>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.HotelContacts.GetAllHotelContact>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.HotelContacts.GetHotelContactById>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.HotelContacts.CreateHotelContact>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.HotelContacts.UpdateHotelContact>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.HotelContacts.DeleteHotelContact>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.GetOffers>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.GetAdminOffers>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.UpdateOffer>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.CreateOffer>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.DeleteOffer>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Offers.ValidateOffer>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Add.CreateAdd>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Add.DeleteAdd>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Add.GetAdds>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Add.UpdateAdd>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Locations.GetLocations>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.GetPageFacilities>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.RoomType.GetClientPageRoomType>();
builder.Services.AddScoped<CreateRoom>();
builder.Services.AddScoped<UpdateRoom>();
builder.Services.AddScoped<DeleteRoom>();
builder.Services.AddScoped<GetRoomById>();
builder.Services.AddScoped<GetAllRooms>();
builder.Services.AddScoped<GetRoomStatusToday>();
builder.Services.AddScoped<GetGallery>();
builder.Services.AddScoped<AddGalleryImage>();
builder.Services.AddScoped<UpdateGalleryImage>();
builder.Services.AddScoped<DeleteGalleryImage>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Season.GetAllSeasons>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Season.UpdateSeason>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Season.CreateSeason>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Season.DeleteSeason>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Season.GetSeasonById>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.CalculatePrice>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.GetHeroImages>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.AddHeroImage>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.UpdateHeroImage>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.DeleteHeroImage>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.DeleteRoomType>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Hero.UpdateHeroText>();

builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.CreateFacility>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.DeleteFacility>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.UpdateFacility>();
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.GetFacilityById>(); 
builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Facility.GetAllFacilities>();

builder.Services.AddScoped<CaribbeanOcean.Application.UseCases.Locations.UpdateLocations>();
// ==========================================
// 2. Construcción de la Aplicación
// ==========================================
// ═════════════════════════════════════════════════════════════════════════════
var app = builder.Build();
// ═════════════════════════════════════════════════════════════════════════════

// ── Middleware de excepciones ───────────────────────────────
app.UseMiddleware<ExceptionHandlingMiddleware>();

// ── OpenAPI solo en desarrollo ────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors("AllowAll");        // 1. CORS 
if (!app.Environment.IsDevelopment())
    app.UseHttpsRedirection();        // 2. solo en producción
app.UseAuthentication();              // 3. identifica al usuario
app.UseAuthorization();               // 4. verifica permisos
app.MapControllers();


app.Run();