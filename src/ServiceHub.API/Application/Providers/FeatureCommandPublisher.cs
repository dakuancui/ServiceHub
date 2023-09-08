using ServiceHub.API.Application.Models.FeatureControl;

namespace ServiceHub.API.Application.Providers
{
    public class FeatureCommandPublisher : IObservable<FeatureCommand>
	{
        private List<IObserver<FeatureCommand>> _observers;

		public FeatureCommandPublisher()
		{
            _observers = new List<IObserver<FeatureCommand>>();
		}

        public IDisposable Subscribe(IObserver<FeatureCommand> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<FeatureCommand>> _observers;
            private IObserver<FeatureCommand> _observer;

            public Unsubscriber(List<IObserver<FeatureCommand>> observers, IObserver<FeatureCommand> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public void SendCommand(FeatureCommand command)
        {
            foreach (var observer in _observers)
            {
                if (string.IsNullOrWhiteSpace(command.ProfileName)
                    || string.IsNullOrWhiteSpace(command.FeatureName)
                    || string.IsNullOrWhiteSpace(command.Command))
                    observer.OnError(new UnknownCommandException());
                else
                    observer.OnNext(command);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in _observers.ToArray())
                if (_observers.Contains(observer))
                    observer.OnCompleted();

            _observers.Clear();
        }
    }

    public class UnknownCommandException : Exception
    {
        internal UnknownCommandException()
        { }
    }
}
