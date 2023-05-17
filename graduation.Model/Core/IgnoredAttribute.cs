namespace graduation.Model.Core
{
    public class IgnoredAttribute : System.Attribute
    {
        public string SomeProperty { get; set; } //veri tabanında olmayıp içerde tuttuğumuz veirleri ignored etmek için kullanıcaz (ihtiyaç olursa)
    }
}
