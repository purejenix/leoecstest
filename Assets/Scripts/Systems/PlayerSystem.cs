using Leopotam.EcsLite;

namespace ECSTest
{
    public class PlayerSystem : IEcsInitSystem//, IEcsRunSystem
    {
        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var playerEntity = ecsWorld.NewEntity();
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
        }

        public void Run(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var filter = ecsWorld.Filter<PlayerComponent>().Inc<TargetComponent>().End();
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            var targetPool = ecsWorld.GetPool<TargetComponent>();
            var gameData = ecsSystems.GetShared<GameData>();

            var playerShift = gameData.DeltaTime * gameData.PlayerSpeed;

            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                ref var targetComponent = ref targetPool.Get(entity);

                var diffX = targetComponent.PosX - playerComponent.PosX;
                var diffY = targetComponent.PosY - playerComponent.PosY;
                var dist = (float)System.Math.Sqrt(diffX * diffX + diffY * diffY);
                if (dist > gameData.ProximityError)
                {
                    var shift = playerShift / dist;
                    if (shift < 1f)
                    {
                        playerComponent.PosX += diffX * dist;
                        playerComponent.PosY += diffY * dist;
                    }
                    else
                    {
                        playerComponent.PosX = targetComponent.PosX;
                        playerComponent.PosY = targetComponent.PosY;
                    }
                }
            }
        }
    }
}