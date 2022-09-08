using Leopotam.EcsLite;

namespace ECSTest
{
    public class ButtonDoorLinkSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var doorPool = ecsWorld.GetPool<DoorComponent>();
            var buttonPool = ecsWorld.GetPool<ButtonComponent>();

            foreach (var entity in ecsWorld.Filter<DoorComponent>().Inc<ButtonComponent>().End())
            {
                ref var buttonComponent = ref buttonPool.Get(entity);
                ref var doorComponent = ref doorPool.Get(entity);

                doorComponent.State = buttonComponent.IsPressed switch
                {
                    true when doorComponent.State != DoorState.Opened => DoorState.Opening,
                    false when doorComponent.State != DoorState.Closed => DoorState.Closing,
                    _ => doorComponent.State
                };
            }
        }
    }
}