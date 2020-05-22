namespace Cw4.DTOs.Requests
{
    public class LoginRequest
    {

        public LoginRequest(string login, string haslo)
        {
            this.Login = login;
            this.Haslo = haslo;
        }

        public LoginRequest() { }

        public string Login { get; set; }
        public string Haslo { get; set; }

    }
}
