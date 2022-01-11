namespace WebAPIBasic.QueryFilters
{
    // 105.5 Создаём новый фильтр для строки запроса
    public class ProjectTicketQueryFilter
    {
        public string? Owner { get; set; }
    }
    // --> Далее, внедряем функционал в ProjectV2Controller
}
