using Microsoft.JSInterop;

namespace GopherNote.Services
{
    public class SpeechService : IDisposable
    {
        private readonly IJSRuntime _js;
        private DotNetObjectReference<SpeechService>? _objRef;

        // События, на которые будут подписываться компоненты
        public event Action<string, string>? OnRecognized;
        public event Action? OnEnded;
        public event Action<string>? OnError;

        public SpeechService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<bool> StartRecognitionAsync(string language = "ru-RU")
        {
            // Создаем ссылку на текущий C# объект, чтобы JS мог вызывать его методы
            _objRef = DotNetObjectReference.Create(this);
            return await _js.InvokeAsync<bool>("speechInterop.start", _objRef, language);
        }

        public async Task StopRecognitionAsync()
        {
            await _js.InvokeVoidAsync("speechInterop.stop");
        }

        // Этот атрибут позволяет вызывать метод из JavaScript
        [JSInvokable]
        public void OnSpeechRecognized(string finalTranscript, string interimTranscript)
        {
            OnRecognized?.Invoke(finalTranscript, interimTranscript);
        }

        [JSInvokable]
        public void OnSpeechError(string error)
        {
            OnError?.Invoke(error);
        }

        [JSInvokable]
        public void OnSpeechEnded()
        {
            OnEnded?.Invoke();
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}