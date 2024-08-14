using UnityEngine;

namespace IdleClicker
{
    public class BattleManager : MonoBehaviour
    {
        // Variables

        [Header("Player info")]
        [SerializeField] private float playerCurrentStrength;
        [SerializeField] private float playerLiftSpeed;
        [SerializeField] private float playerEarningPerLift;

        [Header("Opponent info")]
        [SerializeField] private float opponentCurrentStrength;
        [SerializeField] private float opponentLiftSpeed;
        [SerializeField] private float opponentEarningperLift;

        // Properties


        // Methods

        private void Update()
        {
            HandlePlayerLift();
            HandleOpponentLift();
        }

        private void HandleOpponentLift()
        {

        }

        private void HandlePlayerLift()
        {

        }
    }
}
