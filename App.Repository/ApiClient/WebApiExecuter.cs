using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace MyApp.Repository.ApiClient
{
    // 83. Создаём класс прослойки для конечных точек и приложением
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        //83.1 Получаем через конструктор класса необходимые параметры
        //          Нам нужен базовый адрес до конечной точки
        //          И функционал http клиента

        // 121.1 Добавляем параметер с ключём string apiKey
        public WebApiExecuter(string baseUrl, HttpClient httpClient, string apiKey)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;

            // 83.2 Начинаем формировать заголовки для запроса

            // 121.2 Добавляем ключ в заголовки запроса
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
        }

        // 83.3 Создаём универсальный метод для GET запросов
        public async Task<T> InvokeGet<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        // 83.5 Создаём универсальный метод для POST запросов
        public async Task<T> InvokePost<T>(string uri, T obj)
        {
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

        // 83.6 Создаём универсальный метод для PUT запросов
        public async Task InvokePut<T>(string uri, T obj)
        {
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

    }
}
