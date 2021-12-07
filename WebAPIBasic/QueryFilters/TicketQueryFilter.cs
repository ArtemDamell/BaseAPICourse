namespace WebAPIBasic.QueryFilters
{
    // 79. Добавляем фильтр запроса для Ticket
    public class TicketQueryFilter
    {
        public int? Id { get; set; }

        // 87.3 Свойство Title стало TitleOrDescription, Description удаляем
        public string? TitleOrDescription { get; set; }
        //public string? Description { get; set; }
        // <-- 87.4 Редактируем TicketV2Controller
    }
}
