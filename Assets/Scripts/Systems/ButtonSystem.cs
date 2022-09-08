using Leopotam.EcsLite;

namespace ECSTest
{
    public class ButtonSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var buttonPool = ecsWorld.GetPool<ButtonComponent>();
            var playerPool = ecsWorld.GetPool<PlayerComponent>();

            foreach (var playerEntity in ecsWorld.Filter<PlayerComponent>().End())
            {
                ref var player = ref playerPool.Get(playerEntity);

                foreach (var buttonEntity in ecsWorld.Filter<ButtonComponent>().End())
                {
                    ref var button = ref buttonPool.Get(buttonEntity);

                    var diffX = player.PosX - button.PosX;
                    var diffY = player.PosY - button.PosY;
                    var isInside = diffX * diffX + diffY * diffY <= button.Radius * button.Radius;
                    button.IsPressed = isInside;
                }
            }
        }
    }
}