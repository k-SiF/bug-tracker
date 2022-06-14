namespace bug_tracker_web.Models
{
    public class TicketView
    {
        public int currentPageIndex { get; set; }
        public int pageCount { get; set; }
        public List<Ticket>? TicketList { get; set; }
    }
}
