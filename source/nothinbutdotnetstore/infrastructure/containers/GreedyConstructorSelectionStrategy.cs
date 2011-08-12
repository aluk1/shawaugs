using System;
using System.Linq;
using System.Reflection;

namespace nothinbutdotnetstore.infrastructure.containers
{
	public class GreedyConstructorSelectionStrategy : IPickTheConstructorToCreateTheItemWith
	{
		public ConstructorInfo get_the_applicable_constructor_on(Type type)
		{
			return type.GetConstructors().
                OrderBy(c => c.GetParameters().Count()).Last();
		}
	}
}