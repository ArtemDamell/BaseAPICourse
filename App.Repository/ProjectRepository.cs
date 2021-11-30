﻿using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    // 84. Создаём класс для доступа к проектам
    // Теперь нам надо использовать ранее созданный класс. Мы можем его использовать
    // Напрямую, или же через интерфейс. Переходим в WebApiExecuter, нажимаем на название класса ПКМ
    // И экстрактируем Interface (Extract Interface) и нажимаем OK
    public class ProjectRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        // 84.1 Внедряем зависимость WebApiExecuter через интерфейс IWebApiExecuter
        // Таким образом мы следуем основному принципу паттерна внедрения зависимостей,
        // Который гласит, что объекты высокого уровня не должны зависить от объектов уровнем ниже
        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        // 84.2 Создаём метод получения всех проектов
        public async Task<IEnumerable<Project>> Get()
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/project");
        }
    }
}