using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Repository
{
    // 141. Для хранения токена нам потребуется новый репозиторий. Создать TokenRepository
    public class TokenRepository : ITokenRepository
    {
        public string Token { get; set; }
    }
    // Не забываем экстрактировать интерфейс. После, продолжаем реализовывать логику AuthenticationRepository, добавив в зависимости новый класс репозитория
}
