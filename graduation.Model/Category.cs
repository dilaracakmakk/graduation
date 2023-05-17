namespace graduation.Model
{
    public class Category : Core.ModelBase //id tuttugum için id yazmıyorum modelbase den alıyorum
    {
        public string Name { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
