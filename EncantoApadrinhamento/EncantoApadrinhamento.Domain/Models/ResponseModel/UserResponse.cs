namespace EncantoApadrinhamento.Domain.ResponseModel
{
    public class UserResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{Name} {LastName}";
        public string? NickName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
