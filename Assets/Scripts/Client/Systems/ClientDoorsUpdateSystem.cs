using Leopotam.EcsLite;

namespace ECSTest.Client
{
    public class ClientDoorsUpdateSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var filterDoor = ecsWorld.Filter<DoorComponent>().Inc<ClientDoorComponent>().End();
            var doorPool = ecsWorld.GetPool<DoorComponent>();
            var doorViewPool = ecsWorld.GetPool<ClientDoorComponent>();
            foreach (var entity in filterDoor)
            {
                ref var door = ref doorPool.Get(entity);
                ref var doorView = ref doorViewPool.Get(entity);
                doorView.Door.UpdatePosition(door.Shift);
            }
        }
    }
}