namespace EncantoApadrinhamento.Services.RequestModel.Auth
{
    public class ResetPasswordRequest
    {
        public Guid Id { get; set; }
        public required string Token { get; set; } 
        public required string NewPassword { get; set; }
    }
}
