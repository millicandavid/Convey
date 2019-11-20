using System;
using System.Linq;
using Convey.CQRS.Commands.Dispatchers;
using Microsoft.Extensions.DependencyInjection;

namespace Convey.CQRS.Commands
{
    public static class Extensions
    {
        public static IConveyBuilder AddCommandHandlers(this IConveyBuilder builder, string rootNamespace = null)
        {
            builder.Services.Scan(s =>
                s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => (rootNamespace == null) ? true : a.FullName.StartsWith(rootNamespace)))
                    .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            return builder;
        }        

        public static IConveyBuilder AddInMemoryCommandDispatcher(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            return builder;
        }
    }
}