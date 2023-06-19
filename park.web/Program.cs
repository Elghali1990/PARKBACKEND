using chrep.core.park.uof;
using chrep.data.park.SqlServer;
using chrep.data.park.uof;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar=true,
    Timeout=5000
});
builder.Services.AddMvc().AddRazorPagesOptions(
    options =>
    {
        options.Conventions.AddPageRoute("/Auth/Login", "");
    });
builder.Services.AddSession();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));
builder.Services.AddTransient<IUnitofworks, Unitofworks>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.UseNToastNotify();
app.MapRazorPages();

app.Run();
