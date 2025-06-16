using identity.service.model;

namespace Identity.Api.model
{
    public class Request
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExecutedAt { get; set; }
        public bool? Success { get; set; }
        //public string Status { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}


// id, text , url, created at, status, user id