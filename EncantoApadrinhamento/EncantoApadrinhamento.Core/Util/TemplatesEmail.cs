namespace EncantoApadrinhamento.Core.Util
{
    public static class TemplatesEmail
    {
        public static string ConfirmacaoEmail(string name, string link)
        {
            return $@"<html>
                        <body>
                            <h1>Olá {name}!</h1>
                            <p>Seja bem-vindo ao encanto apadrinhamento!</p>
                            <p>Para confirmar seu email, clique no link abaixo:</p>
                            <a href='{link}'>Confirmar email</a>
                        </body>
                    </html>";
        }

        public static string RecuperacaoSenha(string name, string link)
        {
            return $@"<html>
                        <body>
                            <h1>Olá {name}!</h1>
                            <p>Para recuperar sua senha, clique no link abaixo:</p>
                            <a href='{link}'>Recuperar senha</a>
                        </body>
                    </html>";
        }

        public static string SenhaAlterada(string name)
        {
            return $@"<html>
                        <body>
                            <h1>Olá {name}!</h1>
                            <p>Sua senha foi alterada com sucesso!</p>
                        </body>
                    </html>";
        }
    }
}
