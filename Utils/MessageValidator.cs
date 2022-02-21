using System;
using System.Linq;

using System.Text;

namespace sms_invite.Validators
{
    public static class MessageValidator
    {
        public static bool ValidateMessage(string message)
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
    }
}