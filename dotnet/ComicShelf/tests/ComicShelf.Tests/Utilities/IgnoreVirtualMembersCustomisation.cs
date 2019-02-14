using AutoFixture;

namespace ComicShelf.Tests.Utilities
{
	public class IgnoreVirtualMembersCustomisation : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Customizations.Add(new IgnoreVirtualMembers());
		}
	}
}
