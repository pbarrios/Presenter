using Presenter.Core.Interfaces;
using System;
using System.Collections.Generic;
using Presenter.Core.Entities;
using System.IO;
using System.Data.OleDb;

namespace Presenter.Core.DataManager
{
    public class DataManager : IDataManager, IObservable
    {
        private FileSystemWatcher _fileWatcher;
        private List<IObserver> _observers;

        public DataManager(IConfigurationManager configurationManager)
        {
            _observers = new List<IObserver>();

            _fileWatcher = new FileSystemWatcher(configurationManager.Configuration.DbFilesPath);
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
            //var products = new List<Product>();
            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Access\Database11.accdb";
            //using (OleDbConnection connection = new OleDbConnection(connString))
            //{
            //    connection.Open();
            //    OleDbDataReader reader = null;
            //    var command = new OleDbCommand("SELECT Nombre, Price from  Product WHERE FamilyId = @1", connection);
            //    command.Parameters.AddWithValue("@1", familyId);
            //    reader = command.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        products.Add(new Product
            //        {
            //            Name = reader[0].ToString(),
            //            Price = decimal.Parse(reader[1].ToString())
            //        });
            //    }
            //}

            //return products;


            if (familyId == 1)
                return new List<Product> {
                new Product
                {
                    Name = "Rigatti",
                    ImagePath = "rigatti.png",
                    Price = 10.0M
                },
                new Product
                {
                    Name = "Spaguetti",
                    ImagePath = "Spaguetti.png",
                    Price = 15.0M
                },
            };

            return new List<Product> {
                new Product
                {
                    Name = "agnelotti",
                    ImagePath = "agnelotti.png",
                    Price = 25.0M
                },
                new Product
                {
                    Name = "Caseritos",
                    ImagePath = "Spaguetti.png",
                    Price = 15.0M
                },
            };
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
