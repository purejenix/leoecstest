using Leopotam.EcsLite;
using UnityEngine;

namespace ECSTest
{
    public class Playground : MonoBehaviour
    {
        [SerializeField] public Client.PlayerController player;
        [SerializeField] private Collider checkCollider;
        [SerializeField] private ButtonDoorLink[] links;

        private EcsWorld _ecsWorld;
        private GameData _gameData;
        private IEcsSystems _systems;

        private void Start()
        {
            _ecsWorld = new EcsWorld();
            _gameData = new GameData
            {
                PlayerSpeed = 2f,
                ProximityError = 0.1f,
                DoorClosingSpeed = 1f,
                DoorOpeningSpeed = 1f,
                DoorOpenHeight = 2f
            };

            _systems = new EcsSystems(_ecsWorld, _gameData)
                .Add(new ButtonSystem())
                .Add(new DoorSystem())
                .Add(new ButtonDoorLinkSystem())
                .Add(new PlayerSystem())
                .Add(new Client.ClientPlayerSystem(player))
                .Add(new Client.ClientInputTargetSystem(checkCollider))
                .Add(new Client.ClientLinksInitSystem(links))
                .Add(new Client.ClientDoorsUpdateSystem());

            _systems.Init();
        }

        private void Update()
        {
            _gameData.DeltaTime = Time.deltaTime;
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems.Destroy();
            _ecsWorld.Destroy();
        }
    }
}