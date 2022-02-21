using System.Linq;
using System.Collections;
using System.Collections.Generic;

using AutoMapper;
using AutoMapper.Configuration;

using sms_invite.Contracts;
using sms_invite.Models;

namespace sms_invite.Mappers
{
    public class InviteProfile : Profile
    {
        public InviteProfile()
        {
            CreateMap<SmsInvite, Invite>();

            CreateMap<SmsInvite, IEnumerable<Invite>>().ConvertUsing<ConvertSmsInviteToInvites>();
            CreateMap<IEnumerable<Invite>, SmsInvite>().ConvertUsing<ConvertSmsInviteToInvites>();
        }
    }

    public class ConvertSmsInviteToInvites : ITypeConverter<SmsInvite, IEnumerable<Invite>>, ITypeConverter<IEnumerable<Invite>, SmsInvite>
    {
        public IEnumerable<Invite> Convert(SmsInvite source, IEnumerable<Invite> destination, ResolutionContext context)
        {
            IEnumerable<Invite> invitesWithNumbers = source.Numbers.Select(num => new Invite() { PhoneNumber = num, Message = source.Message });
            
            return invitesWithNumbers;
        }

        public SmsInvite Convert(IEnumerable<Invite> source, SmsInvite destination, ResolutionContext context)
        {
            IEnumerable<string> numbers = source.Select(inv => inv.PhoneNumber);

            destination.Numbers = numbers.ToArray();
            destination.Message = source.First().Message;

            return destination;
        }
    }
}