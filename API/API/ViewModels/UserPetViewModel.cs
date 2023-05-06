namespace API.ViewModels
{
    public class UserPetViewModel
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; }
        public Guid UserTypeId { get; set; }
        public Guid PetTypeId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string SizeOrBreed { get; set; }
        public bool Neutered { get; set; }
        public bool HasPatologies { get; set; }
    }
}
