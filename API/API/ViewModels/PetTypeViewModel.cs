namespace API.ViewModels
{
    public class PetTypeViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
