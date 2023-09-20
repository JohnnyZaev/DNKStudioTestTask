using UnityEngine;

namespace Difficulty
{
    [CreateAssetMenu(menuName = "Difficulty")]
    public class DifficultyBase : ScriptableObject
    {
        public float pipeSpeed;
        public float timeToSpawn;
        public float middleDistance;
    }
}