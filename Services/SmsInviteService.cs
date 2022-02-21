using System;
using System.Collections.Generic;

using sms_invite.Contracts;
using sms_invite.Models;
using sms_invite.Interfaces;
using static sms_invite.Validators.PhoneNumbersValidation;
using static sms_invite.Validators.MessageValidator;

using AutoMapper;
using NickBuhro.Translit;

namespace sms_invite.Servcie
{
    public class SmsInviteService : IInviteService
    {
        private readonly IInviteRepository _repository;
        private readonly IMapper _map;

        public SmsInviteService(IMapper mapper, IInviteRepository repository)
        {
            _repository = repository;
            _map = mapper;
        }

        public SmsInvite Invite(SmsInvite smsInvite)
        {
            List<Invite> addedInvites = new();
            smsInvite.Message = Transliteration.CyrillicToLatin(smsInvite.Message);

            if (ValidateNumbers(smsInvite.Numbers) && ValidateMessage(smsInvite.Message))
            {
                var invites = _map.Map<IEnumerable<Invite>>(smsInvite);

                foreach (var invite in invites)
                {
                    var addedInvite = _repository.AddInvite(invite);
                    addedInvites.Add(invite);
                }
            }

            return _map.Map<SmsInvite>(smsInvite);
        }
    }
}