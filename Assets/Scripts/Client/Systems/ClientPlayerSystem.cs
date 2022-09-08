using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Client
{
    public class ClientPlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly PlayerController _playerController;

        public ClientPlayerSystem(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var playerPosition = _playerController.transform.position;
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            foreach (var entity in ecsWorld.Filter<PlayerComponent>().End())
            {
                ref var player = ref playerPool.Get(entity);
                player.PosX = playerPosition.x;
                player.PosY = playerPosition.z;
            }
        }

        public void Run(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();

            var playerPosition = _playerController.transform.position;
            var playerPosition2d = new Vector2(playerPosition.x, playerPosition.z);

            var filter = ecsWorld.Filter<PlayerComponent>().Inc<TargetComponent>().End();
            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            var playerTargetPool = ecsWorld.GetPool<TargetComponent>();
            var gameData = ecsSystems.GetShared<GameData>();
            foreach (var entity in filter)
            {
                ref var playerComponent = ref playerPool.Get(entity);
                playerComponent.PosX = playerPosition2d.x;
                playerComponent.PosY = playerPosition2d.y;

                ref var playerTarget = ref playerTargetPool.Get(entity);

                var diff = new Vector2(playerTarget.PosX, playerTarget.PosY) - playerPosition2d;
                if (diff.magnitude < gameData.ProximityError)
                {
                    playerTargetPool.Del(entity);
                    _playerController.move = Vector2.zero;
                }
                else
                {
                    _playerController.move = diff;
                }
            }
        }
    }
}