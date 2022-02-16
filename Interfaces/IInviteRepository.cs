using sms_invite.Models;

namespace sms_invite.Interfaces
{
    public interface IInviteRepository 
    {
        Invite AddInvite(Invite invite);
    }
}