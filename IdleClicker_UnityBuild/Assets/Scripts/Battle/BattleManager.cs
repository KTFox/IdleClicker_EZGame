using UnityEngine;
using UnityEngine.EventSystems;

namespace IdleClicker.Battle
{
    public class BattleManager : MonoBehaviour
    {
        // Variables

        public static BattleManager Instance;

        private const float MATCH_TIME = 20f;

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
        [SerializeField] private GameObject prepareTitlePanel;

        private bool canPlayerLift = true;
        private bool canOpponentLift = true;

        private float currentTime;

        private BattleState currentState = BattleState.Preparing;

        // Properties

        public float PlayerLiftSpeed => playerLiftSpeed;
        public int CurrentTime => (int)currentTime;

        // enum

        private enum BattleState
        {
            Preparing, Fighting, Ending
        }


        // Methods

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            playerAnimationTrigger.GetComponent<Animator>().speed = playerLiftSpeed;
            opponentAnimationTrigger.GetComponent<Animator>().speed = opponentLiftSpeed;

            currentTime = MATCH_TIME;
        }

        private void Update()
        {
            switch (currentState)
            {
                case BattleState.Preparing:

                    prepareTitlePanel.SetActive(true);

                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                        {
                            prepareTitlePanel.SetActive(false);
                            currentState = BattleState.Fighting;
                        }
                    }

                    break;

                case BattleState.Fighting:

                    currentTime -= Time.deltaTime;
                    HandlePlayerLift();
                    HandleOpponentLift();

                    if (currentTime <= 0f)
                    {
                        currentState = BattleState.Ending;
                    }

                    break;

                case BattleState.Ending:

                    Debug.Log("End battle!!!");

                    break;

                default:
                    break;
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
                        playerAnimationTrigger.AnimationTrigger(playerEarningPerLift);
                    }
                }
            }
        }

        private void HandleOpponentLift()
        {
            if (canOpponentLift)
            {
                canOpponentLift = false;
                opponentAnimationTrigger.AnimationTrigger(opponentEarningperLift);
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
