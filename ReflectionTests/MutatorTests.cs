using Reflection;

namespace ReflectionTests;

public class MutatorTests
{
	class C {
		public string? Value { get; init; }
	}

	record R
	{
		public string? Value { get; init; }
	}

	struct S
	{
		public string? Value { get; init; }
	}

	[Fact]
	public void Mutate__classPassedIn__valueIsMutated()
	{
		var mutator = new Mutator();

		var c = new C {
			Value = "original value",
		};

		// The following does not compile, because the prop is { get; init; }
		//c.Value = "";

		// but this mutates it anyway, because { init; } is a compile-time check and does nothing at runtim
		mutator.Mutate(c, x => x.Value, "new value");

		Assert.Equal("new value", c.Value);
	}

	[Fact]
	public void Mutate__structPassedIn__valueIsNotMutated()
	{
		var mutator = new Mutator();

		var s = new S
		{
			Value = "original value",
		};

		// this still mutates the passed-in struct, but since it is a copy, the original is unchanged
		mutator.Mutate(s, x => x.Value, "new value");

		Assert.Equal("original value", s.Value);
	}

	[Fact]
	public void Mutate__recordPassedIn__valueIsMutated()
	{
		var mutator = new Mutator();

		var r = new R
		{
			Value = "original value",
		};

		// The following does not compile, because the prop is { get; init; }
		//r.Value = "";

		// but this mutates it anyway, because { init; } is a compile-time check and does nothing at runtim
		mutator.Mutate(r, x => x.Value, "new value");

		Assert.Equal("new value", r.Value);
	}
}
