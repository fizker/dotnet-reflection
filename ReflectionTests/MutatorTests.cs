using Reflection;

namespace ReflectionTests;

public class MutatorTests
{
	class C {
		public string? RegularSetter { get; set; }
		public string? InitSetter { get; init; }
	}

	record R
	{
		public string? RegularSetter { get; set; }
		public string? InitSetter { get; init; }
	}

	struct S
	{
		public string? RegularSetter { get; set; }
		public string? InitSetter { get; init; }
	}

	[Fact]
	public void Mutate__classPassedIn_regularSetterMutated__valueIsChanged()
	{
		var mutator = new Mutator();

		var c = new C {
			RegularSetter = "original value",
		};

		mutator.Mutate(c, x => x.RegularSetter, "new value");

		Assert.Equal("new value", c.RegularSetter);
	}

	[Fact]
	public void Mutate__classPassedIn_initSetterMutated__valueIsNotChanged()
	{
		var mutator = new Mutator();

		var c = new C
		{
			InitSetter = "original value",
		};

		// The following does not compile, because the prop is { get; init; }
		//c.InitSetter = "";

		// but this mutates it anyway, because { init; } is a compile-time check and does nothing at runtim
		mutator.Mutate(c, x => x.InitSetter, "new value");

		Assert.Equal("original value", c.InitSetter);
	}

	[Fact]
	public void Mutate__structPassedIn_regularSetter__valueIsNotMutated()
	{
		var mutator = new Mutator();

		var s = new S
		{
			RegularSetter = "original value",
		};

		// this still mutates the passed-in struct, but since it is a copy, the original is unchanged
		mutator.Mutate(s, x => x.RegularSetter, "new value");

		Assert.Equal("original value", s.RegularSetter);
	}

	[Fact]
	public void Mutate__structPassedIn_initSetter__valueIsNotMutated()
	{
		var mutator = new Mutator();

		var s = new S
		{
			InitSetter = "original value",
		};

		// this still mutates the passed-in struct, but since it is a copy, the original is unchanged
		mutator.Mutate(s, x => x.InitSetter, "new value");

		Assert.Equal("original value", s.InitSetter);
	}

	[Fact]
	public void Mutate__recordPassedIn_regularSetter__valueIsChanged()
	{
		var mutator = new Mutator();

		var r = new R
		{
			RegularSetter = "original value",
		};

		mutator.Mutate(r, x => x.RegularSetter, "new value");

		Assert.Equal("new value", r.RegularSetter);
	}

	[Fact]
	public void Mutate__recordPassedIn_initSetter__valueIsNotMutated()
	{
		var mutator = new Mutator();

		var r = new R
		{
			InitSetter = "original value",
		};

		// The following does not compile, because the prop is { get; init; }
		//r.InitSetter = "";

		// but this mutates it anyway, because { init; } is a compile-time check and does nothing at runtim
		mutator.Mutate(r, x => x.InitSetter, "new value");

		Assert.Equal("original value", r.InitSetter);
	}
}
