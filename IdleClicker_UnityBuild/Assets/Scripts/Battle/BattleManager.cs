using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker.Battle
{
    public class BattleManager : MonoBehaviour
    {
        // Variables

        public static BattleManager Instance;

        private float ORIGINAL_TIME_OF_CHARACTER_TRAINING_ANIMATION = 2f;

        [Header("Player info")]
        [SerializeField] private float playerCurrentStrength;
        [SerializeField] private float playerLiftSpeed = 2f;
        [SerializeField] private float playerEarningPerLift;
        [SerializeField] private PlayerAnimationTrigger playerAnimationTrigger;

        [Header("Opponent info")]
        [SerializeField] private float opponentCurrentStrength;
        [SerializeField] private float opponentLiftSpeed;
        [SerializeField] private float opponentEarningperLift;
        [SerializeField] private OpponentAnimationTrigger opponentAnimationTrigger;

        [Header("UI")]
        [SerializeField] private ToolHolderUI toolHolderUI;

        private bool canPlayerLift = true;
        private bool canOpponentLift = true;

        // Properties

        public float PlayerLiftSpeed => playerLiftSpeed;


        // Methods

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            playerAnimationTrigger.GetComponent<Animator>().speed = playerLiftSpeed;
            opponentAnimationTrigger.GetComponent<Animator>().speed = opponentLiftSpeed;
        }

        private void Update()
        {
            HandlePlayerLift();
            HandleOpponentLift();
        }

        private void HandleOpponentLift()
        {
            if (canOpponentLift)
            {
                canOpponentLift = false;
                opponentAnimationTrigger.AnimationTrigger(opponentEarningperLift);
            }
        }

        private void HandlePlayerLift()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                {
                    if (canPlayerLift)
                    {
                        canPlayerLift = false;
                        toolHolderUI.RunCooldown();
                        playerAnimationTrigger.AnimationTrigger(playerEarningPerLift);
                    }
                }
            }
        }

        public void GainPlayerStrength()
        {
            playerCurrentStrength += playerEarningPerLift;
        }

        public void ResetCanPlayerLift()
        {
            canPlayerLift = true;
        }

        public void GainOpponentStrength()
        {
            opponentCurrentStrength += opponentEarningperLift;
        }

        public void ResetCanOpponentLift()
        {
            canOpponentLift = true;
        }
    }
}
