using sms_invite.Contracts;

namespace sms_invite.Interfaces
{
    public interface IInviteService
    {
        SmsInvite Invite(SmsInvite invite);
    }
}