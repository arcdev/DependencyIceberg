using System;
using Autofac;

namespace DependencyIceberg
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var container = SetupDependencies();

			var trunk = container.Resolve<ITrunk>();
		}

		private static IContainer SetupDependencies()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<Trunk>().As<ITrunk>().InstancePerLifetimeScope();
			builder.RegisterType<Branch>().As<IBranch>().InstancePerLifetimeScope();
			builder.RegisterType<Twig>().As<ITwig>().InstancePerLifetimeScope();
			builder.RegisterType<Leaf>().As<ILeaf>().InstancePerLifetimeScope();

			return builder.Build();
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
		public Trunk(IBranch branch)
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
		public Branch(ITwig twig)
		{
			Console.WriteLine("ctor ---> Branch");
		}
	}

	public class Twig : ITwig
	{
		public Twig(ILeaf leaf)
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