using ScanToKnowBusiness;
using ScanToKnowBusiness.Services;
using ScanToKnowDataAccess.Repositories;
using Supabase;

var builder = WebApplication.CreateBuilder(args);

//-----------------------------
// CORS
//-----------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// ----------------------------
// Initialize Supabase Client
// ----------------------------
var supabaseClient = new Supabase.Client(
    builder.Configuration["SupabaseUrl"],
    builder.Configuration["SupabaseKey"],
    new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    });

await supabaseClient.InitializeAsync();
builder.Services.AddSingleton(supabaseClient);

// ----------------------------
// Register Repositories and Services
// ----------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISupabaseService, SupabaseService>();

// ----------------------------
// Add Controllers & Swagger
// ----------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------------
// Redirect root "/" to Swagger
// ----------------------------
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

// ----------------------------
// Configure Middleware
// ----------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();