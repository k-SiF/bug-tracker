
namespace bug_tracker_web.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title field required")]
        [StringLength(maximumLength: 150, MinimumLength = 5)]
        public string Title { get; set; } = String.Empty;
        [Required(ErrorMessage = "Author field required")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string Author { get; set; } = String.Empty;
        [Required(ErrorMessage = "Description field required")]
        [StringLength(maximumLength: 500, MinimumLength = 10)]
        public string Description { get; set; } = String.Empty;
        public string Status { get; set; } = "Pending";
        [Required(ErrorMessage = "Priority field required")]
        public string Priority { get; set; } = String.Empty;
        [Required(ErrorMessage = "Developer field required")]
        public string Developer { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        [Required(ErrorMessage = "Deadline field required")]
        public DateTime? Deadline { get; set; } = DateTime.Today.Date;

        public Ticket()
        {

        }

        public enum Priorities
        {
            Urgent,
            High,
            Mid,
            Low
        }

        public enum Types
        {
            Issue,
            Bugfixing,
            Testing,
            Request,
            OS,
            Question
        }

        public enum Developers
        {
            John,
            Mike,
            Paul,
            Olivia,
            Preston,
            William,
            Frederik,
            Elizabeth
        }

    }
}
