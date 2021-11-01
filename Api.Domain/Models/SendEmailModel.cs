using System;
namespace Api.Domain.Models
{
    public class SendEmailModel
    {
        private string _sendTo;
        public string SendTo
        {
            get { return _sendTo; }
            set { _sendTo = value; }
        }

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        private string _bodyEmail;
        public string BodyEmail
        {
            get { return _sendTo; }
            set { _sendTo = value; }
        }
    }
}

