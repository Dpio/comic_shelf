using AutoFixture;
using System.Linq;

namespace ComicShelf.Tests.Utilities
{
	public static class FixtureExtensions
	{
		public static IFixture WithStandardCustomization(this IFixture fixture)
		{
			fixture = new Fixture().Customize(new IgnoreVirtualMembersCustomisation());
			fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => fixture.Behaviors.Remove(b));
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());
			return fixture;
		}
	}
}
