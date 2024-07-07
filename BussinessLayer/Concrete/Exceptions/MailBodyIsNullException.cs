namespace GonulInsanlari.Exceptions.Newsletter
{
    public class MailBodyIsNullException : Exception
    {

        public MailBodyIsNullException() : base("Body of the mail was being tried to sent was null.")
        {

        }


    }
}
