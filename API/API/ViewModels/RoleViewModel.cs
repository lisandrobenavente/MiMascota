namespace API.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
