﻿namespace EncantoApadrinhamento.Services.RequestModel.Auth
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
