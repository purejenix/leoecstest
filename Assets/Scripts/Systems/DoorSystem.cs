using Leopotam.EcsLite;

namespace ECSTest
{
    public class DoorSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();
            var deltaTime = gameData.DeltaTime;
            var openingSpeed = gameData.DoorOpeningSpeed;
            var closingSpeed = gameData.DoorClosingSpeed;
            var openHeight = gameData.DoorOpenHeight;

            var ecsWorld = ecsSystems.GetWorld();
            var doorPool = ecsWorld.GetPool<DoorComponent>();

            foreach (var entity in ecsWorld.Filter<DoorComponent>().End())
            {
                ref var door = ref doorPool.Get(entity);

                if (door.State == DoorState.Closed || door.State == DoorState.Opened) continue;

                var shiftSpeed = door.State switch
                {
                    DoorState.Opening => openingSpeed,
                    DoorState.Closing => -closingSpeed,
                    _ => 0f
                };

                door.Shift += shiftSpeed * deltaTime;
                if (door.Shift < 0f)
                {
                    door.Shift = 0f;
                    door.State = DoorState.Closed;
                }
                else if (door.Shift > openHeight)
                {
                    door.Shift = openHeight;
                    door.State = DoorState.Opened;
                }
            }
        }
    }
}