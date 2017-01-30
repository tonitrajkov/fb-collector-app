using System;
using System.Collections.Specialized;
using System.Configuration;
using NHibernate;
using Configuration = NHibernate.Cfg.Configuration;

namespace NHibernateCfg
{
    public static class SessionFactory
    {
        private static readonly object Lock = new object();
        private static ISessionFactory _sessionFactory;

        public static void Init()
        {
            lock (SessionFactory.Lock)
            {
                Configuration local0 = new Configuration();
                local0.Configure();
                NameValueCollection local1 = (NameValueCollection)ConfigurationManager.GetSection("nhibernate.mapping");
                if (local1 != null)
                {
                    foreach (string item0 in (NameObjectCollectionBase)local1)
                    {
                        if (item0 != null)
                        {
                            string local3 = local1[item0];
                            if (!string.IsNullOrEmpty(local3))
                                local0.AddAssembly(local3);
                        }
                    }
                }
                local0.BuildMappings();
                NameValueCollection local4 = (NameValueCollection)ConfigurationManager.GetSection("nhibernate.interceptors");
                if (local4 != null)
                {
                    foreach (string item1 in (NameObjectCollectionBase)local4)
                    {
                        if (item1 != null)
                        {
                            Type local6 = Type.GetType(item1);
                            if (local6 != (System.Type)null)
                                local0.SetInterceptor((IInterceptor)Activator.CreateInstance(local6));
                        }
                    }
                }
                if (SessionFactory._sessionFactory != null)
                    return;
                SessionFactory._sessionFactory = local0.BuildSessionFactory();
            }
        }

        public static ISessionFactory GetSessionFactory()
        {
            if (SessionFactory._sessionFactory == null)
                SessionFactory.Init();
            return SessionFactory._sessionFactory;
        }

        public static ISession GetNewSession()
        {
            return SessionFactory.GetSessionFactory().OpenSession();
        }
    }
}
