namespace graduation.Data.infrastructure.Entities
{
    public class DataResult //işlem yaptım sonucunda noldu bilmek istiyorum 
    {
        public DataResult(bool isSucceed, string message)
        {
            IsSucceed = isSucceed;
            Message = message;
        }
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }
}
