using System;
using Autofac;

namespace LazyDependencyIceberg
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

			// for the final creation
			builder.RegisterType<Trunk>().As<ITrunk>().InstancePerLifetimeScope();
			builder.RegisterType<Branch>().As<IBranch>().InstancePerLifetimeScope();
			builder.RegisterType<Twig>().As<ITwig>().InstancePerLifetimeScope();
			builder.RegisterType<Leaf>().As<ILeaf>().InstancePerLifetimeScope();

			// for the Lazy creation
			builder.Register(context =>
			{
				Console.WriteLine("resolving Lazy<IBranch>");
				return new Lazy<IBranch>(() =>
				{
					Console.WriteLine("resolving IBranch");
					return context.Resolve<IBranch>();
				});
			}).As<Lazy<IBranch>>();

			builder.Register(context =>
			{
				Console.WriteLine("resolving Lazy<ITwig>");
				return new Lazy<ITwig>(() =>
				{
					Console.WriteLine("resolving ITwig");
					return context.Resolve<ITwig>();
				});
			}).As<Lazy<ITwig>>();

			builder.Register(context =>
			{
				Console.WriteLine("resolving Lazy<ILeaf>");
				return new Lazy<ILeaf>(() =>
				{
					Console.WriteLine("resolving ILeaf");
					return context.Resolve<ILeaf>();
				});
			}).As<Lazy<ILeaf>>();

			var rr = builder.Register(context => new Lazy<IBranch>(context.Resolve<IBranch>)).As<Lazy<IBranch>>();

			return builder.Build();
		}


		//public static IRegistrationBuilder<Lazy<TInterface>, SimpleActivatorData, SingleRegistrationStyle> RegisterLazy<TInterface>(ContainerBuilder builder){
		//	return builder.Register(context => new Lazy<TInterface>(context.Resolve<TInterface>)).As<Lazy<TInterface>>();
		//}
	}


	public interface ITrunk
	{
		void Execute();
	}

	public interface IBranch
	{
	}

	public interface ITwig
	{
	}

	public interface ILeaf
	{
	}

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