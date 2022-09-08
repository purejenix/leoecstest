using UnityEngine;

namespace ECSTest.Client
{
    public class ButtonBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform visual;
        [SerializeField] private float radius;

        public float Radius => radius;
        public Transform Visual => visual;
    }
}
