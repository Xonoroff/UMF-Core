using Core.src.Infrastructure;
using Core.src.Messaging;
using Core.src.Signals;
using Core.src.Utils;
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
            
            Container.Bind<ApplicationCycleTracker>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("ApplicationCycleTracker")
                .AsCached()
                .NonLazy();
            
            Container.DeclareSignal<OnApplicationQuitSignal>();
        }
    }
}
