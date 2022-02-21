using sms_invite.Contracts;
using System.Threading.Tasks;

namespace sms_invite.Interfaces
{
    public interface IInviteService
    {
        Task<SmsInvite> Invite(SmsInvite invite);
    }
}