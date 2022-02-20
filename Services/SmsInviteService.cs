using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

using sms_invite.Contracts;
using sms_invite.Models;
using sms_invite.Interfaces;

using Mediaburst.Text;
using AutoMapper;
namespace sms_invite.Servcie
{
    public class SmsInviteService : IInviteService
    {
        private IInviteRepository _repository;
        private readonly IMapper _map;

        private readonly char[] _forbiddenChars = { '+', '(', ')' };
        private const char _internationalCode = '7';
        public SmsInviteService(IMapper mapper)
        {
            //_repository = repository;
            _map = mapper;
        }

        private bool ValidateNumbers(string[] numbers)
        {
            bool isNumbersEmpty = numbers.Length == 0;
            bool isNumbersMore = numbers.Length > 16;
            bool isDuplicatedNumber = numbers.Distinct().Count() < numbers.Length;

            bool isInvalidNumber = numbers.Any(num =>
                   num.First() != _internationalCode
                || num.Length > 11
                || _forbiddenChars.Any(c => num.Contains(c)));

            if (isNumbersEmpty)
            {
                throw new ArgumentNullException("Phone numbers are missing");
            }
            if (isNumbersMore)
            {
                throw new ArgumentOutOfRangeException("Too much phone numbers, should be less or equal to 16 per request");
            }
            if (isDuplicatedNumber)
            {
                throw new Exception("Duplicate numbers detected");
            }
            if (isInvalidNumber)
            {
                throw new ArgumentException("One or several phone numbers do not match with international format");
            }


            return true;
        }
        private bool ValidateMessage(string message)
        {
            Encoding gsmEnc = new Mediaburst.Text.GSMEncoding();
            Encoding utf8Enc = new System.Text.UTF8Encoding();

            bool isMessageMore = message.Length > 128;

            if (!message.Any())
            {
                throw new RankException("Invite message is missing");
            }
            else if (isMessageMore)
            {
                throw new OverflowException("Invite message too long, should be less or equal to 128 characters of 7-bit GSM charset");
            }
            else
            {
                byte[] gsmBytes = utf8Enc.GetBytes(message);
                byte[] utf8Bytes = Encoding.Convert(gsmEnc, utf8Enc, gsmBytes);
                string body = utf8Enc.GetString(utf8Bytes);

                if (!body.Any() || body.Length < message.Length)
                {
                    throw new FormatException("Invite message should contain only characters in 7-bit GSM encoding or Cyrillic letters as well");
                }
            }

            return true;
        }

        public SmsInvite Invite(SmsInvite smsInvite)
        {
            List<Invite> addedInvites = new();

            if (ValidateNumbers(smsInvite.Numbers) && ValidateMessage(smsInvite.Message))
            {
                var invites = _map.Map<IEnumerable<Invite>>(smsInvite);

                foreach (var invite in invites)
                {
                    //var addedInvite = _repository.AddInvite(invite);
                    addedInvites.Add(invite);
                }
            }

            return _map.Map<SmsInvite>(smsInvite);
        }
    }
}