using Zenject;

public class AdsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(new AdsController()).AsSingle().NonLazy();
    }
}