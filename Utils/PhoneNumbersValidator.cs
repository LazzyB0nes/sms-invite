using System;
using System.Linq;

namespace sms_invite.Validators
{
    public static class PhoneNumbersValidation
    {
        private static readonly char[] _forbiddenChars = { '+', '(', ')' };
        private const char _internationalCode = '7';

        public static bool ValidateNumbers(string[] numbers)
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
                throw new ArgumentNullException(message:"Phone numbers are missing", paramName:nameof(numbers));
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

    }
}