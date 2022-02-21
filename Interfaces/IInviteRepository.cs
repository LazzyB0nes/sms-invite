using sms_invite.Models;
using System.Threading.Tasks;

namespace sms_invite.Interfaces
{
    public interface IInviteRepository 
    {
        Task<Invite> AddInvite(Invite invite);
    }
}