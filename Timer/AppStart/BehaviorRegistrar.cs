using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Timer.AppStart
{
    // remove when PR https://github.com/jbogard/MediatR.Extensions.Microsoft.DependencyInjection/pull/75
    // is merged
    public static class BehaviorRegistrar
    {
        public static IServiceCollection AddBehaviorsForRequest<T>(this IServiceCollection services,
            params Type[] handlerAssemblyMarkerTypes)
            where T : class
        {
            var assemblies = handlerAssemblyMarkerTypes.Select(t => IntrospectionExtensions.GetTypeInfo(t).Assembly).ToArray();

            if (!assemblies.Any())
            {
                throw new ArgumentException(
                    "No assemblies found to scan. Supply at least one assembly to scan for handlers.");
            }

            RegisterBehaviorForCovariantRequest(typeof(T), services, assemblies);

            return services;
        }

        private static void RegisterBehaviorForCovariantRequest(
            Type assignableRequestType,
            IServiceCollection services,
            IEnumerable<Assembly> assembliesToScan)
        {
            var types = assembliesToScan.SelectMany(a => a.DefinedTypes).Where(t => t.IsConcrete()).ToArray();

            var handlerTypes = types.Where(t => t.GetInterface(typeof(IRequestHandler<,>)) != null);

            var requestResponseTypes = handlerTypes.Select(h =>
                    h.GetInterface(typeof(IRequestHandler<,>)).GetGenericArguments()[0])
                .Where(assignableRequestType.IsAssignableFrom)
                .Select(request => (request, request.GetInterface(typeof(IRequest<>)).GetGenericArguments()[0]));

            var behaviorTypes = types.Where(t =>
                {
                    var @interface = t.GetInterface(typeof(IPipelineBehavior<,>));
                    var firstGenericArgument = @interface?.GetGenericArguments()[0];
                    return firstGenericArgument != null &&
                           (firstGenericArgument.IsAssignableFrom(assignableRequestType)
                            || (firstGenericArgument.IsGenericParameter
                                && firstGenericArgument
                                    .GetInterfaces()
                                    .Any(i => i.IsAssignableFrom(assignableRequestType))));
                })
                .ToArray();

            foreach (var (requestType, responseType) in requestResponseTypes)
            {
                var typeArgs = new[] {requestType, responseType};
                var closedInterfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(typeArgs);

                foreach (var behaviorType in behaviorTypes)
                {
                    if (behaviorType.GenericTypeParameters.Length == 1)
                    {
                        var closedImplementationType = behaviorType.MakeGenericType(responseType);
                        services.AddTransient(closedInterfaceType, closedImplementationType);
                    }
                    else if (behaviorType.GenericTypeParameters.Length == 2)
                    {
                        var closedImplementationType = behaviorType.MakeGenericType(requestType, responseType);
                        services.AddTransient(closedInterfaceType, closedImplementationType);
                    }
                }
            }
        }

        private static Type GetInterface(this Type pluggedType, Type interfaceType)
        {
            return pluggedType
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
        }

        private static bool IsConcrete(this Type type)
        {
            return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
        }
    }
}