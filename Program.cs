using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Components;
using SchoolLibraryApp.Data;
using SchoolLibraryApp.Repositories;
using SchoolLibraryApp.Services;
using SchoolLibraryApp.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<LibraryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILibraryService, LibraryService>();
builder.Services.AddValidatorsFromAssemblyContaining<BookFormValidator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<LibraryDbContext>>();
    await using var db = await factory.CreateDbContextAsync();
    await db.Database.MigrateAsync();
    await DbInitializer.SeedAsync(db);
}

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
