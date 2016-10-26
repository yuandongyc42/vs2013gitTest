using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Castle.MicroKernel.Registration;

namespace RedisTest.redisM
{
    public class CacheManager
    {

        public static void Init()
        {
            var container = IocManager.IocContainer;
            var si = IocManager.Resolve<SiteInfo>();
            //注册redis
            if (!container.Kernel.HasComponent(typeof(ConnectionMultiplexer)))
                container.Register(Component.For<ConnectionMultiplexer>().UsingFactoryMethod((ioc) =>
                {
                    return ConnectionMultiplexer.Connect(si.RedisHost);
                }).LifestyleSingleton());


            if (!container.Kernel.HasComponent(typeof(IDatabase)))
                container.Register(Component.For<IDatabase>().UsingFactoryMethod((ioc) =>
                    ioc.Resolve<ConnectionMultiplexer>().GetDatabase())
                    .LifestylePerWebRequest());
            ConnectionMultiplexer.Connect(si.RedisHost + ",AllowAdmin=true").GetServer(si.RedisHost).FlushDatabase();
        }

    }
}