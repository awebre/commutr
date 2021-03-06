﻿using SimpleInjector;

namespace commutr.Services
{
    public class DependencyResolver
    {
        private readonly Container container;
        public DependencyResolver(Container container)
        {
            this.container = container;
        }

        public T Resolve<T>() where T : class
        {
            return container.GetInstance<T>();
        }
    }
}
