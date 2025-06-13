namespace Identity.Api.model
{
    public class Request
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string Url { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }

        public int UserId { get; set; }



    }
}


// id, text , url, created at, status, user id