using Source.Scripts.Views;
using Unity.AI.Navigation;
using UnityEngine;

namespace Source.Scripts.Systems
{
    public sealed class LevelSpawnSystem
    {
        private readonly LevelView _levelView;
        private readonly NavMeshSurface _navMeshSurface;
        
        public LevelSpawnSystem(LevelView levelView, NavMeshSurface  surface)
        {
            _levelView = levelView;
            _navMeshSurface = surface;
        }

        public void Initialize()
        {
            _levelView.transform.position = Vector3.zero;
            _navMeshSurface.BuildNavMesh();
        }
    }
}