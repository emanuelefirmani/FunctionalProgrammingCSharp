using LaYumba.Functional;
using LaYumba.Functional.Option;

namespace HonestFunctions
{
    public class Email
    {
        private string Body { get; set; }
        private Email(string body) => Body = body;

        private static bool IsValid(string body) => !string.IsNullOrEmpty(body);

        public static Option<Email> Of(string body) =>
            IsValid(body) ? new Email(body) : new Option<Email>();

        public static implicit operator string(Email email) => email.Body;
    }
}