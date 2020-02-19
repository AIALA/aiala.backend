using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using aiala.Backend.Resources;

namespace aiala.Backend
{
    public static class InitializationExtensions
    {
        public static MvcOptions ConfigureModelBindingMessages(this MvcOptions options, IServiceCollection services)
        {
            var provider = options.ModelBindingMessageProvider;
            var messages = services.BuildServiceProvider().GetService<IStringLocalizerFactory>().Create(typeof(ModelBindingMessages));

            provider.SetAttemptedValueIsInvalidAccessor((a, b) => messages[nameof(ModelBindingMessages.AttemptedValueIsInvalid), a, b]);

            provider.SetMissingBindRequiredValueAccessor((a) => messages[nameof(ModelBindingMessages.MissingBindRequiredValue), a]);

            provider.SetMissingKeyOrValueAccessor(() => messages[nameof(ModelBindingMessages.MissingKeyOrValue)]);

            provider.SetMissingRequestBodyRequiredValueAccessor(() => messages[nameof(ModelBindingMessages.MissingRequestBodyRequiredValue)]);

            provider.SetNonPropertyAttemptedValueIsInvalidAccessor((a) => messages[nameof(ModelBindingMessages.NonPropertyAttemptedValueIsInvalid), a]);

            provider.SetNonPropertyUnknownValueIsInvalidAccessor(() => messages[nameof(ModelBindingMessages.NonPropertyUnknownValueIsInvalid)]);

            provider.SetNonPropertyValueMustBeANumberAccessor(() => messages[nameof(ModelBindingMessages.NonPropertyValueMustBeANumber)]);

            provider.SetUnknownValueIsInvalidAccessor((a) => messages[nameof(ModelBindingMessages.UnknownValueIsInvalid), a]);

            provider.SetValueIsInvalidAccessor((a) => messages[nameof(ModelBindingMessages.ValueIsInvalid), a]);

            provider.SetValueMustNotBeNullAccessor((a) => messages[nameof(ModelBindingMessages.ValueMostNotBeNull), a]);

            provider.SetValueMustBeANumberAccessor((a) => messages[nameof(ModelBindingMessages.ValueMustBeANumber), a]);

            return options;
        }
    }
}
