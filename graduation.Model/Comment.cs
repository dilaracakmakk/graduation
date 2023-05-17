namespace graduation.Model
{
    public class Comment : Core.ModelBase
    {
        public int ContentId { get; set; }
        public string Fullname { get; set; }
        public string Mail { get; set; }
        public string Text { get; set; }
        public bool IsApprovied { get; set; }
    }
}
