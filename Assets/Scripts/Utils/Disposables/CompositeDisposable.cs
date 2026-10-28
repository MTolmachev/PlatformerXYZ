using System;
using System.Collections.Generic;

namespace Utils.Disposables
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }

            disposables.Clear();
        }
    }
}
