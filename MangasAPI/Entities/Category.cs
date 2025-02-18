namespace MangasAPI.Entities
{
    using MangasAPI.Validation;

    public sealed class Category : Entity
    {
        public Category()
        { }

        public string? Name { get; private set; }

        public string? IconCSS { get; private set; }

        public Category(string name, string iconCss)
        {
            ValidateDomain(name, iconCss);        
        }

        public Category(int id, string name, string iconCss)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id");
            Id = id;
            ValidateDomain(name, iconCss);
        }

        public void Update(string name, string iconCss)
        {
            ValidateDomain(name, iconCss);
        }

        private void ValidateDomain(string name, string iconCss)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), 
                "Name is mandatory.");

            DomainExceptionValidation.When(name.Length < 3, 
                "Invalid name");

            DomainExceptionValidation.When(string.IsNullOrEmpty(iconCss), 
                "Icon is mandatory.");

            DomainExceptionValidation.When(iconCss.Length < 3, 
                "Invalid icon");

            Name = name;
            IconCSS = iconCss;
        }

        public IEnumerable<Manga>? Mangas { get; set; }
    }
}
