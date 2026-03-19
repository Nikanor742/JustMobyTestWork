using Game.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Game.DI
{
    public sealed class GameLTS : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputController>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameInitializer>();
        }
    }
}