using FluentValidation;

namespace Rikarin {
    public static class ModelValidation {
        public static void Validate<T, U>(U input) where T : AbstractValidator<U>, new() {
            var validationResult = new T().Validate(input);
            if (!validationResult.IsValid) {
                throw new ModelValidationException(validationResult);
            }
        }
    }
}
