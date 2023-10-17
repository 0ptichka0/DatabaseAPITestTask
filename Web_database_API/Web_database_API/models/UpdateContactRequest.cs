namespace Web_database_API.models
{
    public class UpdateContactRequest
    {

        public string SecondName { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }
    }
}
