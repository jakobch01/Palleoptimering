
namespace Palleoptimering.Models
{
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }

        public bool RememberMe { get; set; }
    }
}