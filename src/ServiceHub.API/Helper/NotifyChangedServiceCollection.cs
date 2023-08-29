//using System;
//using System.Collections;

//namespace ServiceHub.API.Helper
//{
//	public class NotifyChangedServiceCollection : IServiceCollection
//	{
//        private readonly IServiceCollection _services;

//        /// <summary>
//        /// Fired when a descriptor is added to the collection
//        /// </summary>
//        public event EventHandler<ServiceDescriptor> ServiceAdded;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="NotifyChangedServiceCollection"/> class.
//        /// </summary>
//        /// <param name="services">The services.</param>
//        public NotifyChangedServiceCollection(IServiceCollection services)
//        {
//            _services = services;
//        }

//        /// <summary>
//        /// Get the value at index
//        /// Setting is not supported
//        /// </summary>
//        public ServiceDescriptor this[int index]
//        {
//            get => _services[index];
//            set => throw new NotSupportedException("Inserting services in collection is not supported");
//        }

//        /// <summary>
//        /// Count of services in the collection
//        /// </summary>
//        public int Count { get => _services.Count; }

//        /// <summary>
//        /// Obviously not
//        /// </summary>
//        public bool IsReadOnly { get => false; }

//        /// <summary>
//        /// Adding a service descriptor will fire the ServiceAdded event
//        /// </summary>
//        /// <param name="item"></param>
//        public void Add(ServiceDescriptor item)
//        {
//            _services.Add(item);
//            ServiceAdded.Invoke(this, item);
//        }

//        /// <summary>
//        /// Clear the collection is not supported
//        /// </summary>
//        public void Clear() => throw new NotSupportedException("Removing services from collection is not supported");

//        /// <summary>
//        /// True is the item exists in the collection
//        /// </summary>
//        public bool Contains(ServiceDescriptor item) => _services.Contains(item);

//        /// <summary>
//        /// Copy items to array of service descriptors
//        /// </summary>
//        public void CopyTo(ServiceDescriptor[] array, int arrayIndex) => _services.CopyTo(array, arrayIndex);

//        /// <summary>
//        /// Enumerator for service descriptors
//        /// </summary>
//        public IEnumerator<ServiceDescriptor> GetEnumerator() => _services.GetEnumerator();

//        /// <summary>
//        /// Index of item in the list
//        /// </summary>
//        public int IndexOf(ServiceDescriptor item) => _services.IndexOf(item);

//        /// <summary>
//        /// Inserting is not supported
//        /// </summary>
//        public void Insert(int index, ServiceDescriptor item) => throw new NotSupportedException("Inserting services in collection is not supported");

//        /// <summary>
//        /// Removing items is not supported
//        /// </summary>
//        public bool Remove(ServiceDescriptor item) => throw new NotSupportedException("Removing services from collection is not supported");

//        /// <summary>
//        /// Removing items is not supported
//        /// </summary>
//        public void RemoveAt(int index) => throw new NotSupportedException("Removing services from collection is not supported");

//        /// <summary>
//        /// Enumerator for objects
//        /// </summary>
//        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_services).GetEnumerator();
//    }
//}

