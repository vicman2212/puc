using BuildDataFromDb.Interface;
using BuildDataFromDb.Services;
using Templates;
using Message;
using Message.Mail;
using Microsoft.AspNetCore.Authentication.Cookies;
using MiscApp.Interface;
using MiscApp.Services;
using Webpuc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
////Libreria externa para correr modificar en tiempo de ejecucion el programa
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
//*********************************************************************
//Servicio para inyectar modelos del contexto de la base de datos
builder.Services.AddDbContext<DbempleadosContext>();
builder.Services.AddDbContext<AccesoFircoContext>();
//********************************************************************
//***********SERVICIO INYECTADOS DE LAS BIBLIOTECAS*******************
builder.Services.AddTransient<ICRUD<UserIdentityModel>,UserService>();
builder.Services.AddTransient<ICRUD<List<ModulosPerfilModel>>, AccesoModulosService>();
builder.Services.AddTransient<ICRUD<EmpleadoModel>,EmpleadoService>();
builder.Services.AddTransient<ICRUD<Empleado>, EmpleadoDbService>();
builder.Services.AddTransient<IUpdateData<UserIdentityModel>, UpdateUserPassService>();
builder.Services.AddTransient<ICatalogosdb<LCatalogosModel>, CatalologosDbEmpService>();
builder.Services.AddTransient<IMessageBuilder<MessageModel>, SendMailService>();
builder.Services.AddTransient<IExistData<UserIdentityModel>, SearchUsrService>();
builder.Services.AddTransient<IEncrypt<string>, SecureDataService>();




//***************************AGREGAMOS NUESTRO SERVICIO DE AUTENTICACION********************
//Recuerda para inyectar las dependencias en las vistas hay que registralas en Program.cs
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();//Servicio para el acceso en Razor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                    .AddCookie(options => { 
                                        options.Cookie.HttpOnly = true;
                                        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                                        options.LoginPath = "/Home/Login";
                                        options.AccessDeniedPath = "/Home/Error";
                                        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                                        options.SlidingExpiration = true;
                                    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PA003", policy => policy.RequireRole("RPA_004", "RUA_006", "RUI_005"));//Politica de usuario
    options.AddPolicy("PS001", policy => policy.RequireRole("RSA_001", "RSR_003", "RST_002"));//politica de Erisc
    //politica combinada:
    options.AddPolicy("Logeado", policy => policy.RequireRole("RPA_004", "RUA_006", "RUI_005", "RSA_001", "RSR_003", "RST_002"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//*********************middleware para autenticar y autorizar************************
app.UseAuthentication();
app.UseAuthorization();
//***********************************************************************************

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
