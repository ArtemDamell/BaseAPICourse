using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace App.Repository.ApiClient
{
    // 83. Создаём класс прослойки для конечных точек и приложением
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        //83.1 Получаем через конструктор класса необходимые параметры
        //          Нам нужен базовый адрес до конечной точки
        //          И функционал http клиента
        public WebApiExecuter(string baseUrl, HttpClient httpClient)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;

            // 83.2 Начинаем формировать заголовки для запроса
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
            response.EnsureSuccessStatusCode();

            // Возвращаем ответ клиенту
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // 83.6 Создаём универсальный метод для PUT запросов
        public async Task InvokePut<T>(string uri, T obj)
        {
            // Формируем ответ для клиента
            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            // Убеждаемся, что статус код ответа от конечной точки - 200 ОК
            response.EnsureSuccessStatusCode();
        }

        // 83.7 Создаём универсальный метод для DELETE запросов
        public async Task InvokeDelete(string uri)
        {
            // Формируем ответ для клиента
            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            // Убеждаемся, что статус код ответа от конечной точки - 200 ОК
            response.EnsureSuccessStatusCode();
        }

        // 83.4
        private string GetUrl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }
    }
}
