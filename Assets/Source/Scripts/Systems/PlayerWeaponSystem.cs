using System;
using R3;
using Source.Scripts.Enums;
using Source.Scripts.Interfaces;
using Source.Scripts.Models;
using Source.Scripts.Scriptable;

namespace Source.Scripts.Systems
{
    public sealed class PlayerWeaponSystem : IDisposable
    {
        private readonly PlayerInputSystem _playerInputSystem;
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponConfigSO[] _weaponConfigs;
        private readonly GameStateModel _gameStateModel;

        private IWeapon _currentWeapon;
        private IWeapon[] _weapons;

        private readonly CompositeDisposable _disposables = new();

        public PlayerWeaponSystem(
            PlayerInputSystem playerInputSystem,
            IWeaponFactory weaponFactory,
            WeaponConfigSO[] weaponConfigs,
            GameStateModel gameStateModel)
        {
            _playerInputSystem = playerInputSystem;
            _weaponFactory = weaponFactory;
            _weaponConfigs = weaponConfigs;
            _gameStateModel = gameStateModel;
        }

        public void Initialize()
        {
            _playerInputSystem.OnWeaponChangeInputEvent
                .Subscribe(SelectWeapon)
                .AddTo(_disposables);
            
            _weapons = new IWeapon[_weaponConfigs.Length];

            for (int i = 0; i < _weaponConfigs.Length; i++)
            {
                _weapons[i] = _weaponFactory.Create(_weaponConfigs[i]);
            }

            Observable.EveryUpdate()
                .Where(_ => _playerInputSystem.ShootInput)
                .Subscribe(_ =>
                {
                    if(!_gameStateModel.UpgradesIsOpen.Value)
                        _currentWeapon?.Shoot();
                })
                .AddTo(_disposables);
        }

        public void SelectWeapon(int weaponIndex)
        {
            if(_gameStateModel.UpgradesIsOpen.Value)
                return;
            
            _currentWeapon?.Deselect();

            foreach (var weapon in _weapons)
            {
                if (weapon.WeaponConfig.WeaponType == (EWeaponType)weaponIndex)
                {
                    _currentWeapon = weapon;
                    _currentWeapon.Select();
                    return;
                }
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}