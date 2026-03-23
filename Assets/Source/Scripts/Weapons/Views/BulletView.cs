using UnityEngine;

namespace Source.Scripts.Weapons.Views
{
    public class BulletView : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionFX;

        public GameObject ExplosionFX => _explosionFX;
    }
}