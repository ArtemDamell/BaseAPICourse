//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace WebAPIBasic.Filters
//{
//    // 27.1 
//    /*
//        Для того, чтобы получить весь необходимый функционал, мы должны
//        Не только унаследоваться от базового класса Attribute, но и реализовать
//        Интерфейс IResourseFilter, что и даст весь необходимый функционал
//        Фильтра ресурсов
//     */
//    public class DiscontinueVersion1ResourseFilter : Attribute, IResourceFilter
//    {
//        // 27.2 Имплементируем все методы интерфейса для их реализации
//        public void OnResourceExecuted(ResourceExecutedContext context)
//        {
//            /*
//                Этот метод выполняется тогда, когда наше приложение обработало
//                Запрос и отправляет ответ пользователю. Он нам не нужен, 
//                Поэтому, оставляем его пустым.
//             */
//        }

//        public void OnResourceExecuting(ResourceExecutingContext context)
//        {
//            /*
//                Этот метод выполняется при получении запроса от пользователя
//                И выполняется вторым после авторизации и аутентификации
//                В цепочке фильтров запроса.
//             */
//            // 27.3 Реализовываем метод
//            /*
//                Мы не имеем доступа к данным, но мы имеем полный доступ к
//                Http запросу. Обратимся к нему и проверим, содержит ли путь запроса
//                В себе "v2", если нет (обращаются к первой версии метода), то выдаём
//                ошибку пользователю
//             */
//            if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
//            {
//                context.Result = new BadRequestObjectResult(
//                    new
//                    {
//                        Versioning = new[] { "This version of API has expired, please use the latest version." }
//                    }
//                );
//            }
//        }
//    }
//}
