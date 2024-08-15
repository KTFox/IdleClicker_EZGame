using UnityEngine;

namespace IdleClicker
{
    [CreateAssetMenu(menuName = "PersistenceData")]
    public class PersistenceData : ScriptableObject
    {
        // Variables

        public float playerAnimatorSpeed;
        public float playerEarningPerLift;
        public Sprite playerToolVisual;
        public OpponentSO opponentSO;
    }
}
