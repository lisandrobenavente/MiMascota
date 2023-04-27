using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace Domain.Common.Exceptions
{
    [Serializable]
    public class CommandValidationException : Exception
    {
        public IList<ValidationFailure> ValidationFailures { get; }

        public CommandValidationException(IList<ValidationFailure> validationFailures)
        {
            ValidationFailures = validationFailures;
        }

        public CommandValidationException(SerializationInfo info, StreamingContext context,
            IList<ValidationFailure> validationFailures) : base(info, context)
        {
            ValidationFailures = validationFailures;
        }
    }
}
