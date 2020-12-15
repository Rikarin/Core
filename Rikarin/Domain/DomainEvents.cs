using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Rikarin.Domain {
    public sealed class DomainEvents {
        static IList<Type> _handlers;
        readonly IServiceProvider _serviceProvider;

        public DomainEvents(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch(IDomainEvent domainEvent) {
            if (_handlers == null) {
                _handlers = ExtensionManager.GetInterfaceImplementations(typeof(IHandler<>));
            }

            foreach (var x in _handlers) {
                var canHandle = x.GetInterfaces()
                    .Any(y => y.IsGenericType &&
                              y.GetGenericTypeDefinition() == typeof(IHandler<>) &&
                              y.GenericTypeArguments[0] == domainEvent.GetType());

                if (canHandle) {
                    dynamic handler = ActivatorUtilities.CreateInstance(_serviceProvider, x);
                    await handler?.Handle((dynamic)domainEvent);
                }
            }
        }
    }
}