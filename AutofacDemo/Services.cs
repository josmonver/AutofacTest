﻿
using Autofac;
using System;
using System.Collections.Generic;
namespace AutofacDemo
{
    public interface ISalesRepository { }
    public class SalesRepository : ISalesRepository
    {
        public SalesRepository()
        {

        }
    }
    
    public interface ISalesService { }
    public class SalesService : ISalesService
    {
        ISalesRepository _repo;

        public SalesService(ISalesRepository repo)
        {
            _repo = repo;
        }
    }

    public interface IHandle<T>
    {
        void Handle();
    }

    public class SalesActionHandle : IHandle<string>
    {
        ISalesRepository _repo;
        public SalesActionHandle(ISalesRepository repo)
        {
            _repo = repo;
        }

        public void Handle()
        {
        }
    }

    public interface IAppEvents
    {
        void Raise<T>();
    }

    public class AppEvents : IAppEvents
    {
        private readonly IContainer _container;

        public AppEvents(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
        }

        public void Raise<T>()
        {
            using (var scope = _container.BeginLifetimeScope("AutofacWebRequest"))
            {
                var handlers = scope.Resolve<IEnumerable<IHandle<T>>>();

                foreach (var handler in handlers)
                    handler.Handle();
            }

            //var handlers = _container.Resolve<IEnumerable<IHandle<T>>>();
            //foreach (var handler in handlers)
            //    handler.Handle();
        }
    }
}