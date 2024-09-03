using Amazon;
using Amazon.S3;
using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ITouristAttractionService, TouristAttractionService>();
builder.Services.AddSingleton<IAmazonS3, AmazonS3Client>();
builder.Services.AddSingleton<ITouristAttractionImageService, TouristAttractionImageService>();

var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();


builder.Services.AddDbContext<MVC_DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (args.Length ==1 && args[0].ToLower() == "seeddata")
    Seed.SeedData(app);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
