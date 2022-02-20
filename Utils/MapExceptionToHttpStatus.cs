using System.Net;
using System;

public class MapExceptionToHttpStatus
{
        /// <summary>
        /// Маппинг исключений на http коды
        /// </summary>
        /// <param name="ExceptionName"></param>
        /// <returns></returns>
        public static (HttpStatusCode, string) Map(string ExceptionName)
        {
            return ExceptionName switch
            {
                nameof(ArgumentException) => (HttpStatusCode.BadRequest, "PHONE_NUMBERS_INVALID"),
                nameof(ArgumentNullException) => (HttpStatusCode.Unauthorized, "PHONE_NUMBERS_EMPTY"),
                nameof(ArgumentOutOfRangeException) => (HttpStatusCode.PaymentRequired, "PHONE_NUMBERS_INVALID"),
                nameof(Exception) => (HttpStatusCode.NotFound, "PHONE_NUMBERS_INVALID"),
                nameof(RankException) => (HttpStatusCode.MethodNotAllowed, "PHONE_NUMBERS_INVALID"),
                nameof(FormatException) => (HttpStatusCode.NotAcceptable, "MESSAGE_EMPTY"),
                nameof(OverflowException) => (HttpStatusCode.ProxyAuthenticationRequired, "MESSAGE_INVALID"),

                _ => (HttpStatusCode.InternalServerError, "SMS_SERVICE")
            };
        }
}


