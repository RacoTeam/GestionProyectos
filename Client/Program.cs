using GestionProyectos.Client;
using GestionProyectos.Client.Services.Contrato;
using GestionProyectos.Client.Services.Implementacion;
using GestionProyectos.Shared.Models;
using GestionProyectos.Client.Extensions;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;

using MudBlazor;
using MudBlazor.Services;
using System.Threading;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IRecursoService, RecursoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();
builder.Services.AddScoped<IUsuarioGrupoService, UsuarioGrupoService>();
builder.Services.AddScoped<IUsuarioGrupoTareaService, UsuarioGrupoTareaService>();



// Añado MudBlazor
builder.Services.AddMudServices();

//Autorizacion
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider,AutenticacionExtension>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();