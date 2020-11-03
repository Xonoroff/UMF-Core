using Zenject;

namespace Core.src.Utils
{
    public abstract class GlobalInstallerBase<TGlobalInstaller, TModuleInstaller> : MonoInstaller<TGlobalInstaller>
        where TGlobalInstaller : MonoInstaller<TGlobalInstaller>
        where TModuleInstaller : Installer
    {
        protected abstract string SubContainerName { get; }
        
        public override void InstallBindings()
        {
            var subcontainer = Container.CreateSubContainer();
            subcontainer.Install<TModuleInstaller>();
            
            Container.Bind<DiContainer>()
                .WithId(SubContainerName)
                .FromInstance(subcontainer)
                .AsCached();
        }
        
        protected void SubContainerInstaller(DiContainer subContainer)
        {
            subContainer.Install<TModuleInstaller>();
        }

        protected DiContainer SubContainerInstanceGetter(InjectContext containerContext)
        {
            return containerContext.Container.ResolveId<DiContainer>(SubContainerName);
        }
    }
}