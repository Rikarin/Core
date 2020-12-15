using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Rikarin {
    public class MethodNotAllowedException : Exception {
        public MethodNotAllowedException() {
        }

        public MethodNotAllowedException(string message) : base(message) {
        }
    }

    public class NotFoundException : Exception {
        public NotFoundException(string message) : base(message) {
        }
    }

    public class BadRequestException : Exception {
        public BadRequestException(string message) : base(message) {
        }
    }

    public class ForbiddenException : Exception {
        public ForbiddenException(string message) : base(message) {
        }
    }

    public class ConflictException : Exception {
        public ConflictException(string message) : base(message) {
        }
    }

    public class UnauthorizedException : Exception {
        public UnauthorizedException(string message) : base(message) {
        }
    }

    public class ModelValidationEntry {
        public string PropertyName { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }

    public class ModelValidationException : Exception {
        public IList<ModelValidationEntry> Validations { get; }

        public ModelValidationException(ValidationResult validationResult) : base("validation") {
            Validations = new List<ModelValidationEntry>();

            foreach (var x in validationResult.Errors) {
                Validations.Add(new ModelValidationEntry {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                });
            }
        }
    }
}