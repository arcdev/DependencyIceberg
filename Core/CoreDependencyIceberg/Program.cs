using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoreDependencyIceberg
{
    class Program
    {
        static void Main(string[] args)
        {
	        var serviceProvider = SetupDependencies();
	        var trunk = serviceProvider.GetService<ITrunk>();
        }

	    private static ServiceProvider SetupDependencies()
	    {
		    var services = new ServiceCollection();

			// for the final creation
		    services.AddScoped<ITrunk, Trunk>();
		    services.AddScoped<IBranch, Branch>();
		    services.AddScoped<ITwig, Twig>();
		    services.AddScoped<ILeaf, Leaf>();

			// for the Lazy creation
		    services.AddScoped(provider => new Lazy<IBranch>(provider.GetService<IBranch>));
		    services.AddScoped(provider => new Lazy<ITwig>(provider.GetService<ITwig>));
		    services.AddScoped(provider => new Lazy<ILeaf>(provider.GetService<ILeaf>));

		    return services.BuildServiceProvider();
	    }
    }

	public interface ITrunk
	{
		void Execute();
	}
	public interface IBranch{}
	public interface ITwig{}
	public interface ILeaf{}


	public class Trunk : ITrunk
	{
		public Trunk(Lazy<IBranch> branch)
		{
			Console.WriteLine("ctor ---> Trunk");
		}

		public void Execute()
		{
			// NOOP just a placholder
		}
	}

	public class Branch : IBranch
	{
		public Branch(Lazy<ITwig> twig)
		{
			Console.WriteLine("ctor ---> Branch");
		}
	}

	public class Twig : ITwig
	{
		public Twig(Lazy<ILeaf> leaf)
		{
			Console.WriteLine("ctor ---> Twig");
		}
	}

	public class Leaf : ILeaf
	{
		public Leaf()
		{
			Console.WriteLine("ctor ---> Leaf");
		}
	}
}
