namespace graduation.Model
{
    public class RolePage : Core.ModelBase
    {
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public string Route { get; set; }

    }
}
