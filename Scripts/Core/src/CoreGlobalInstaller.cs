using System;
using System.Collections.Generic;
using Core.src.Infrastructure;
using Core.src.Messaging;
using Core.src.Signals;
using Core.src.Utils;
using MF.Core.Scripts.Core.src;
using MF.Core.Scripts.Core.src.Utils;
using Scripts.Core.src.Infrastructure;
using Zenject;

namespace Core.src
{
    public class CoreGlobalInstaller : GlobalInstallerBase<CoreGlobalInstaller, CoreInstaller>
    {
        protected override string SubContainerName => "CoreModuleContainer";

        public override void InstallBindings()
        {
            base.InstallBindings();

            SignalBusInstaller.Install(Container);
            Container.Bind<ICommandExecutor>().To<BaseCommandExecutor>().AsTransient();
            Container.Bind<IBigNumberFormatter>().To<BigNumberFormatter>().AsTransient();
            Container.Bind<IEventBus>().To<ZenjectSignalEventBus>().AsCached();
            
            Container.Bind(typeof(ISessionRepository<>)).To(typeof(SessionRepository<>)).AsCached();
            Container.Bind(typeof(IPlayerPrefsRepository<>)).To(typeof(PlayerPrefsRepository<>)).AsCached();
            
            Container.Bind(typeof(IKeysProvider<Type, string>)).To<TypeKeysProviderDefault>().AsCached();
            
            Container.Bind(typeof(IFactorySync<>)).To(typeof(ActivatorObjectFactory<>))
                .AsCached();
            Container.Bind<ISceneHelper>().To<SceneHelper>().AsCached();

            Container.Bind<ApplicationCycleTracker>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("ApplicationCycleTracker")
                .AsCached()
                .NonLazy();

            BindSerializers(Container);
            
            Container.DeclareSignal<OnApplicationQuitSignal>();
        }

        private void BindSerializers(DiContainer container)
        {
            var isNewtonsoft = BindNewtonsoftSerializer(container);
            if (!isNewtonsoft)
            {
                BindUnitySerializer(container);
            }
        }

        private bool BindNewtonsoftSerializer(DiContainer container)
        {
#if NEWTONSOFT_JSON
            container.Bind(typeof(ISerializer<,>))
                .FromMethodUntyped(context =>
                {
                    var members = context.MemberType.GenericTypeArguments;
                    var keyType = members[0];
                    var objectType = members[1];

                    if (keyType != typeof(string))
                    {
                        return null;
                    }

                    var resultType = typeof(NewtonsoftSerializer<>).MakeGenericType(objectType);
                    var result = Activator.CreateInstance(resultType);
                    return result;
                })
                .AsTransient();
            
            return true;
#endif

            return false;
        }        
        
        private bool BindUnitySerializer(DiContainer container)
        {
            container.Bind(typeof(ISerializer<,>))
                .FromMethodUntyped(context =>
                {
                    var members = context.MemberType.GenericTypeArguments;
                    var keyType = members[0];
                    var objectType = members[1];

                    if (keyType != typeof(string))
                    {
                        return null;
                    }

                    var resultType = typeof(UnitySerializer<>).MakeGenericType(objectType);
                    var result = Activator.CreateInstance(resultType);
                    return result;
                })
                .AsTransient();
            
            return true;
        }
    }
}
