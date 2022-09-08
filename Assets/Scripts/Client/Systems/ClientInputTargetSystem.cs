using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest.Client
{
    public class ClientInputTargetSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Collider _collider;

        public ClientInputTargetSystem(Collider checkCollider)
        {
            _collider = checkCollider;
        }

        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var clientPlayerPool = ecsWorld.GetPool<ClientPlayerComponent>();
            foreach (var entity in ecsWorld.Filter<PlayerComponent>().End())
            {
                ref var cameraCollider = ref clientPlayerPool.Add(entity);
                cameraCollider.Camera = Camera.main;
                cameraCollider.Collider = _collider;
            }
        }

        public void Run(IEcsSystems ecsSystems)
        {
            if (!Input.GetButtonDown("Fire1")) return;

            var pointerPosition = Input.mousePosition;

            var ecsWorld = ecsSystems.GetWorld();
            var filter = ecsWorld.Filter<PlayerComponent>().Inc<ClientPlayerComponent>().End();
            var cameraColliderPool = ecsWorld.GetPool<ClientPlayerComponent>();

            foreach (var entity in filter)
            {
                ref var cameraCollider = ref cameraColliderPool.Get(entity);

                var ray = cameraCollider.Camera.ScreenPointToRay(pointerPosition);
                if (cameraCollider.Collider.Raycast(ray, out var hitInfo, 200f)) //_mainCamera.farClipPlane
                {
                    var targetPool = ecsWorld.GetPool<TargetComponent>();
                    ref var target = ref targetPool.Has(entity) ? ref targetPool.Get(entity) : ref targetPool.Add(entity);
                    target.PosX = hitInfo.point.x;
                    target.PosY = hitInfo.point.z;
                }
            }
        }
    }
}