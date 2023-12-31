using BlazorSpinner;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using LibraryAssignment.BlazorWASM;
using LibraryAssignment.BlazorWASM.Interfaces;
using LibraryAssignment.BlazorWASM.Services;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<SpinnerService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

await builder.Build().RunAsync();