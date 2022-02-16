using System;
using System.Text;
using sms_invite.Contracts;
using sms_invite.Models;
using sms_invite.Interfaces;

using Mediaburst.Text;
namespace sms_invite.Servcie
{
    public class SmsInviteService : IInviteService
    {
        private IInviteRepository _repository;

        /* public SmsInviteService(IInviteRepository repository) 
        {
            _repository = repository;
        } */

        public SmsInvite Invite(SmsInvite invite)
        {
            throw new NotImplementedException();
        }

        private Invite AddInvite(Invite invite) 
        {
            return _repository.AddInvite(invite);
        }
    }
}