using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sms_invite.Contracts;
using sms_invite.Interfaces;
namespace sms_invite.Controllers
{
    [ApiController]
    [Route("/api/invite/")]
    public class SmsInviteController : ControllerBase
    {
        private readonly ILogger<SmsInviteController> _logger;
        private readonly IInviteService _service;

        public SmsInviteController(ILogger<SmsInviteController> logger, IInviteService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("send")]
        public SmsInvite Invite(SmsInvite invite)
        {
            return _service.Invite(invite);
        }
    }
}
