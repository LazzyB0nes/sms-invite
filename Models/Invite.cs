using System;

namespace sms_invite.Models
{
    public class Invite
    {
        public Guid Id;
        public DateTime Created;
        public string PhoneNumber;
        public string Message;
        public string ApiId;
    }
}