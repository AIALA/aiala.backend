using System;
using FluentAssertions.Equivalency;

namespace aiala.Backend.UnitTests
{
    public static class FluentAssertionExtensions
    {
        public static EquivalencyAssertionOptions<T> ExcludingIds<T>(this EquivalencyAssertionOptions<T> options)
        {
            return options.Excluding(x => x.SelectedMemberPath.EndsWith("Id"));
        }
    }
}
