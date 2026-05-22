using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using GopherNote;
using GopherNote.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//Регистрируем сервисы
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<QuoteService>(); // сервиc для контекстных цитат
builder.Services.AddScoped<SpeechService>(); //connect SpeechService

await builder.Build().RunAsync();