//using System;

//namespace ServiceHub.API.Helper
//{

//    // IAdvancedServiceProvider either injected 
//    // or resolved via serviceProvider.GetService<IAdvancedServiceProvider>
//    // or even serviceProvider as IAdvancedServiceProvider
//    //advancedServiceProvider.ServiceCollection.AddSingleton...


//    public class AdvancedServiceProvider : IAdvancedServiceProvider, IDisposable
//	{
//        private readonly List<ServiceProvider> _serviceProviders;
//        private readonly NotifyChangedServiceCollection _services;
//        private readonly object _servicesLock = new object();
//        private List<ServiceDescriptor> _newDescriptors;
//        private Dictionary<Type, object> _resolvedObjects;

//        public AdvancedServiceProvider(IServiceCollection services)
//        {
//            // registers itself in the list of services
//            services.AddSingleton<IAdvancedServiceProvider>(this);

//            _serviceProviders = new List<ServiceProvider>();
//            _newDescriptors = new List<ServiceDescriptor>();
//            _resolvedObjects = new Dictionary<Type, object>();
//            _services = new NotifyChangedServiceCollection(services);
//            _services.ServiceAdded += ServiceAdded;
//            _serviceProviders.Add(services.BuildServiceProvider(true));
//        }

//        private void ServiceAdded(object sender, ServiceDescriptor item)
//        {
//            lock (_servicesLock)
//            {
//                _newDescriptors.Add(item);
//            }
//        }

//        /// <summary>
//        /// Add services to this collection
//        /// </summary>
//        public IServiceCollection ServiceCollection { get => _services; }

//        /// <summary>
//        /// Gets the service object of the specified type.
//        /// </summary>
//        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
//        /// <returns>A service object of type serviceType. -or- null if there is no service object of type serviceType.</returns>
//        public object GetService(Type serviceType)
//        {
//            lock (_servicesLock)
//            {
//                // go through the service provider chain and resolve the service
//                var service = GetServiceInternal(serviceType);
//                // if service was not found and we have new registrations
//                if (service == null && _newDescriptors.Count > 0)
//                {
//                    // create a new service collection in order to build the next provider in the chain
//                    var newCollection = new ServiceCollection();
//                    foreach (var descriptor in _services)
//                    {
//                        foreach (var descriptorToAdd in GetDerivedServiceDescriptors(descriptor))
//                        {
//                            ((IList<ServiceDescriptor>)newCollection).Add(descriptorToAdd);
//                        }
//                    }
//                    var newServiceProvider = newCollection.BuildServiceProvider(true);
//                    _serviceProviders.Add(newServiceProvider);
//                    _newDescriptors = new List<ServiceDescriptor>();
//                    service = newServiceProvider.GetService(serviceType);
//                }
//                if (service != null)
//                {
//                    _resolvedObjects[serviceType] = service;
//                }
//                return service;
//            }
//        }

//        private IEnumerable<ServiceDescriptor> GetDerivedServiceDescriptors(ServiceDescriptor descriptor)
//        {
//            if (_newDescriptors.Contains(descriptor))
//            {
//                // if it's a new registration, just add it
//                yield return descriptor;
//                yield break;
//            }

//            if (!descriptor.ServiceType.IsGenericTypeDefinition)
//            {
//                // for a non open type generic singleton descriptor, register a factory that goes through the service provider
//                yield return ServiceDescriptor.Describe(
//                                        descriptor.ServiceType,
//                                        _ => GetServiceInternal(descriptor.ServiceType),
//                                        descriptor.Lifetime
//                                    );
//                yield break;
//            }
//            // if the registered service type for a singleton is an open generic type
//            // we register as factories all the already resolved specific types that fit this definition
//            if (descriptor.Lifetime == ServiceLifetime.Singleton)
//            {
//                foreach (var servType in _resolvedObjects.Keys.Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == descriptor.ServiceType))
//                {

//                    yield return ServiceDescriptor.Describe(
//                            servType,
//                            _ => _resolvedObjects[servType],
//                            ServiceLifetime.Singleton
//                        );
//                }
//            }
//            // then we add the open type registration for any new types
//            yield return descriptor;
//        }

//        private object GetServiceInternal(Type serviceType)
//        {
//            foreach (var serviceProvider in _serviceProviders)
//            {
//                var service = serviceProvider.GetService(serviceType);
//                if (service != null)
//                {
//                    return service;
//                }
//            }
//            return null;
//        }

//        /// <summary>
//        /// Dispose the provider and all resolved services
//        /// </summary>
//        public void Dispose()
//        {
//            lock (_servicesLock)
//            {
//                _services.ServiceAdded -= ServiceAdded;
//                foreach (var serviceProvider in _serviceProviders)
//                {
//                    try
//                    {
//                        serviceProvider.Dispose();
//                    }
//                    catch
//                    {
//                        // singleton classes might be disposed twice and throw some exception
//                    }
//                }
//                _newDescriptors.Clear();
//                _resolvedObjects.Clear();
//                _serviceProviders.Clear();
//            }
//        }

//    }
//}

