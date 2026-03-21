using Source.Scripts.Scriptable;
using Source.Scripts.Views;
using UnityEngine;

namespace Source.Scripts.Interfaces
{
    public interface IBulletFactory
    {
        BulletView Create(WeaponConfigSO weaponConfig);
    }
}