using Microsoft.Practices.Unity;
using NHibernate;

namespace NHibernateCfg
{
    public class NhConfiguration
    {
        private readonly System.Type _defaultRepositoryType = typeof(NhRepository<>);

        public NhConfiguration Configure(IUnityContainer container)
        {
            UnityContainerExtensions.RegisterType(container, typeof(IUnitOfWork), typeof(NhUnitOfWork), new InjectionMember[0]);
            UnityContainerExtensions.RegisterType(container, typeof(IRepository<>), this._defaultRepositoryType, new InjectionMember[0]);
            return this;
        }

        public NhConfiguration WithSessionFactory(IUnityContainer container, ISessionFactory factory)
        {
            UnityContainerExtensions.RegisterInstance(container, typeof(ISessionFactory), (object)factory);
            return this;
        }
    }
}
