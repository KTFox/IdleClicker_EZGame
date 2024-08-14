using IdleClicker.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker.Battle
{
    public class BattleManager : MonoBehaviour
    {
        // Variables

        public static BattleManager Instance;

        [Header("Player info")]
        [SerializeField] private float playerCurrentStrength;
        [SerializeField] private float playerLiftSpeed;
        [SerializeField] private float playerEarningPerLift;

        [Header("Opponent info")]
        [SerializeField] private float opponentCurrentStrength;
        [SerializeField] private float opponentLiftSpeed;
        [SerializeField] private float opponentEarningperLift;

        [Header("UI")]
        [SerializeField] private TrainingToolHolderUI trainingToolHolderUI;

        private bool canLift;

        // Properties


        // Methods

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            HandlePlayerLift();
            HandleOpponentLift();
        }

        private void HandleOpponentLift()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                {
                    if (canLift)
                    {
                        canLift = false;
                        trainingToolHolderUI.RunCooldown();
                    }
                }
            }
        }

        private void HandlePlayerLift()
        {

        }

        public void GainPlayerStrength()
        {
            playerCurrentStrength += playerEarningPerLift;
        }
    }
}
