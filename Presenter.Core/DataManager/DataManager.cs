using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using Presenter.Core.Entities;
using System.IO;

namespace Presenter.Core.DataManager
{
    public class DataManager : IDataManager, IObservable
    {
        private FileSystemWatcher _fileWatcher;
        private List<IObserver> _observers;

        public DataManager(IConfigurationManager configurationManager)
        {
            _observers = new List<IObserver>();

            _fileWatcher = new FileSystemWatcher(configurationManager.DbFilesPath);
            _fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _fileWatcher.Filter = "*.*";
            _fileWatcher.Changed += new FileSystemEventHandler(OnChanged);
            _fileWatcher.EnableRaisingEvents = true;
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public List<Product> GetProductsByFamily(int familyId)
        {
            // TODO: retrieve from ACCESS DB
            throw new NotImplementedException();
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            foreach (var observer in _observers)
            {
                observer.Notify();
            }
        }
    }
}
