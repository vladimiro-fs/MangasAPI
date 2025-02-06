namespace MangasAPI.Entities
{
    using MangasAPI.Validation;

    public sealed class Category : Entity
    {
        public string? Name { get; private set; }

        public Category(string name)
        {
            ValidateDomain(name);        
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id");
            Id = id;
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), 
                "Name is mandatory.");

            DomainExceptionValidation.When(name.Length < 3, 
                "Invalid name");

            Name = name;
        }

        public IEnumerable<Manga>? Mangas { get; set; }
    }
}
