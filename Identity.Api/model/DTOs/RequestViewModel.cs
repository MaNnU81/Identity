namespace Identity.Api.model.DTOs
{
    public class RequestViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public bool? Success { get; set; }
        

        public int UserId { get; set; }

    }
}
