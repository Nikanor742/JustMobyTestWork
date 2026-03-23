using UnityEngine;

namespace Source.Scripts.Views
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionFX;

        public GameObject ExplosionFX => _explosionFX;
    }
}