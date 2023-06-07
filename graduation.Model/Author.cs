namespace graduation.Model
{
    using System.ComponentModel.DataAnnotations.Schema;
    public class Author : Core.ModelBase
    {
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Fullname { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }

       
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
