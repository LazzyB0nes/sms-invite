using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sms_invite.Models;

namespace sms_invite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SmsInviteController : ControllerBase
    {
        private readonly ILogger<SmsInviteController> _logger;

        public SmsInviteController(ILogger<SmsInviteController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public SmsInvite Invite(SmsInvite invite)
        {
            throw new NotImplementedException();
        }
    }
}
