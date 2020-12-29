namespace Shared.Models
{
    public class LoginResultModel
    {
        [Newtonsoft.Json.JsonProperty()]
        public string UserId { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string UserRoles { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string FullName { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string AccessToken { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string TokenType { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public double ExpiresIn { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string Issued { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string Expires { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string Error { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string ErrorDescription { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string phone { get; set; }

        [Newtonsoft.Json.JsonProperty()]
        public string email { get; set; }
    }
}
