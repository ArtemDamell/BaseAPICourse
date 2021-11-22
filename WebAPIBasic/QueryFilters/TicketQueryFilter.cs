namespace WebAPIBasic.QueryFilters
{
    // 79. Добавляем фильтр запроса для Ticket
    public class TicketQueryFilter
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
