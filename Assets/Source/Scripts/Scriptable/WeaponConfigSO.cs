using Source.Scripts.Enums;
using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Scriptable
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public sealed class WeaponConfigSO : ScriptableObject
    {
        [field: SerializeField] public EWeaponType WeaponType {get; private set;}
        [field: SerializeField] public WeaponView WeaponView {get; private set;}
        [field: SerializeField] public BulletView BulletView {get; private set;}
        [field: SerializeField] public LayerMask HitMask {get; private set;}
        [field: SerializeField] public float Damage {get; private set;}
        [field: SerializeField] public float FireRate {get; private set;}
        [field: SerializeField] public float BulletSpeed {get; private set;}
        
        [field: SerializeField] public Vector3 LocalPosition {get; private set;}
        [field: SerializeField] public Vector3 LocalRotation {get; private set;}
        [field: SerializeField] public Vector3 LocalScale {get; private set;}
    }
}