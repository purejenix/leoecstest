using System.Collections.Generic;
using Leopotam.EcsLite;

namespace ECSTest.Client
{
    public class ClientLinksInitSystem : IEcsInitSystem
    {
        private readonly IEnumerable<ButtonDoorLink> _links;

        public ClientLinksInitSystem(IEnumerable<ButtonDoorLink> links)
        {
            _links = links;
        }

        public void Init(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var doorPool = ecsWorld.GetPool<DoorComponent>();
            var buttonPool = ecsWorld.GetPool<ButtonComponent>();
            var doorViewPool = ecsWorld.GetPool<ClientDoorComponent>();
            foreach (var link in _links)
            {
                var linkEntity = ecsWorld.NewEntity();

                ref var doorComponent = ref doorPool.Add(linkEntity);
                doorComponent.Shift = 0;
                doorComponent.State = DoorState.Closed;

                ref var doorView = ref doorViewPool.Add(linkEntity);
                doorView.Door = link.door;

                ref var buttonComponent = ref buttonPool.Add(linkEntity);
                var position = link.button.Visual.position;
                buttonComponent.PosX = position.x;
                buttonComponent.PosY = position.z;
                buttonComponent.Radius = link.button.Radius;
                buttonComponent.IsPressed = false;
            }
        }
    }
}