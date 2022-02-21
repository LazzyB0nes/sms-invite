using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using sms_invite.Interfaces;
using sms_invite.Models;

namespace sms_invite.Repository
{
    public class InviteRepository : IInviteRepository
    {
        private readonly DatabaseContext _dbContext;

        public InviteRepository(DatabaseContext context)
        {
            _dbContext  = context;
        }

        public async Task<Invite> AddInvite(Invite invite)
        {
            invite.ApiId = "4";
            invite.Created = DateTime.UtcNow;
            invite.Id = Guid.NewGuid();

            bool isLimit = await IsDayLimitRequests(invite.Created, invite.ApiId);
            if (isLimit)
            {
                throw new TimeoutException("Too much phone numbers, should be less or equal to 128 per day");
            }

            EntityEntry<Invite> result = await _dbContext.AddAsync(invite);
            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        private async Task<bool> IsDayLimitRequests(DateTime time, string apiId)
        {
            int requestsCount = await _dbContext.Set<Invite>().Where(x => x.Created.Day == time.Day && x.ApiId == apiId).CountAsync();

            return requestsCount >= 128;
        }
    }
}