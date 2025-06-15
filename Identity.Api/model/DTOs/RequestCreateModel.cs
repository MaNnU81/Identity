namespace Identity.Api.model.DTOs
{
    public class RequestCreateModel
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
       
        public bool? Success { get; set; }
        

        
    }
}
