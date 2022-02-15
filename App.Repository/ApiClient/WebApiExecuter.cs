using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Core.StaticData;

namespace MyApp.Repository.ApiClient
{
    // 83. Создаём класс прослойки для конечных точек и приложением
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;

        //83.1 Получаем через конструктор класса необходимые параметры
        //          Нам нужен базовый адрес до конечной точки
        //          И функционал http клиента

        // 121.1 Добавляем параметер с ключём string apiKey
        // 126.1 Вносим правки в WebExecuter, добавив параметер string clientId
        //public WebApiExecuter(string baseUrl, HttpClient httpClient, string clientId, string apiKey)
        //{
        //    _baseUrl = baseUrl;
        //    _httpClient = httpClient;

        //    // 83.2 Начинаем формировать заголовки для запроса

        //    // 121.2 Добавляем ключ в заголовки запроса
        //    httpClient.DefaultRequestHeaders.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Clear();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    // 126.2 Добавляем новый заголовок в запрос
        //    httpClient.DefaultRequestHeaders.Add(SD.ClientIdHeader, clientId);
        //    httpClient.DefaultRequestHeaders.Add(SD.ApiKeyHeader, apiKey);
        //}

        // 142.1 Изменить класс WebApiExecuter, старый метод - выше закомментирован
        public WebApiExecuter(string baseUrl, HttpClient httpClient, ITokenRepository tokenRepository)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;

            // 83.2 Начинаем формировать заголовки для запроса

            // 121.2 Добавляем ключ в заголовки запроса
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Теперь мы не можем единажды инициализировать токен в конструкторе, т.к. он не будет менятся и присваивается ТОЛЬКО после логина
            // А этот класс инициализируется единажды после первого запроса и не сможет больше получать или менять токен
            // Поэтому, добавляем в каждый вызов конечной точки логику токенов
        }

        // 83.3 Создаём универсальный метод для GET запросов
        public async Task<T> InvokeGet<T>(string uri)
        {
            // 145.3 добавляем в каждый вызов конечной точки логику токенов
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);

            return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        // 83.5 Создаём универсальный метод для POST запросов
        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            // 145.7 добавляем в каждый вызов конечной точки логику токенов
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);

            // Формируем ответ для клиента
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            // Убеждаемся, что статус код ответа от конечной точки - 200 ОК
            // 86.1 Комментируем оригинальную реализацию и добавляем ошибку
            //response.EnsureSuccessStatusCode();

            // 86.2 Это реализация метода обработки ошибки, извлечём из него метод
            //      Так, как мы будем использовать эту логику в нескольких методах
            //      Для этого нажимаем ПКМ -> Extract method
            //if (!response.IsSuccessStatusCode)
            //{
            //    var error = await response.Content.ReadAsStringAsync();
            //    throw new HttpRequestException(error);
            //}
            await HandleError(response);

            // Возвращаем ответ клиенту
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // 140.1 Создать новый метод InvokePostReturnString
        public async Task<string?> InvokePostReturnString<T>(string url, T? obj)
        {
            // 145.6 добавляем в каждый вызов конечной точки логику токенов
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);

            var response = await _httpClient.PostAsJsonAsync(GetUrl(url), obj);
            await HandleError(response);

            return await response.Content.ReadAsStringAsync();

            // <-- 140.2 Возвращаемся в репозиторий
        }

        // 83.6 Создаём универсальный метод для PUT запросов
        public async Task InvokePut<T>(string uri, T obj)
        {
            // 145.5 добавляем в каждый вызов конечной точки логику токенов
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);

            // Формируем ответ для клиента
            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            // Убеждаемся, что статус код ответа от конечной точки - 200 ОК
            // 86.3
            //response.EnsureSuccessStatusCode();
            await HandleError(response);
        }

        // 83.7 Создаём универсальный метод для DELETE запросов
        public async Task InvokeDelete(string uri)
        {
            // 145.4 добавляем в каждый вызов конечной точки логику токенов
            var token = await _tokenRepository.GetToken();
            AddTokenHeader(token);
            // Формируем ответ для клиента
            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            // Убеждаемся, что статус код ответа от конечной точки - 200 ОК
            // 86.4
            //response.EnsureSuccessStatusCode();
            await HandleError(response);
        }

        // 83.4
        private string GetUrl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }

        private static async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }

        // 145.2 добавляем в каждый вызов конечной точки логику токенов
        void AddTokenHeader(string token)
        {
            // 154.1 Исправляем ошибки после удаления свойства из интерфейса репозитория ITokenRepository
            if (_tokenRepository != null && !string.IsNullOrWhiteSpace(token))    /*_tokenRepository.Token*/
            {
                _httpClient.DefaultRequestHeaders.Remove(SD.TokenHeader);
                _httpClient.DefaultRequestHeaders.Add(SD.TokenHeader, token);   /*_tokenRepository.Token*/
            }
        }
    }
}
