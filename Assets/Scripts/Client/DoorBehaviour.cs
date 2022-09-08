using UnityEngine;

namespace ECSTest.Client
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform visual;

        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = visual.localPosition;
        }

        public void UpdatePosition(float positionShift)
        {
            visual.transform.localPosition = _initialPosition + new Vector3(0f, positionShift, 0f);
        }
    }
}