using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace IdleClicker.Battle
{
    public class BattleManager : MonoBehaviour
    {
        // Variables

        private const float MATCH_TIME = 10f;

        [Header("Player info")]
        private float playerCurrentStrength;
        private float playerEarningPerLift;
        [SerializeField] private SpriteRenderer playerToolSprite;
        [SerializeField] private PlayerAnimationTrigger playerAnimationTrigger;

        [Header("Opponent info")]
        private float opponentCurrentStrength;
        private float opponentEarningPerLift;
        [SerializeField] private OpponentAnimationTrigger opponentAnimationTrigger;
        [SerializeField] private Animator opponentAnimator;
        [SerializeField] private SpriteRenderer opponentToolSprite;

        [Header("UI")]
        [SerializeField] private GameObject prepareTitlePanel;
        [SerializeField] private ResultPanel resultPanel;

        private bool canPlayerLift = true;
        private bool canOpponentLift = true;

        private float currentTime;

        private BattleState currentState = BattleState.Preparing;

        private PersistenceData persistenceData;

        // Properties

        public float PlayerCurrentStrength => playerCurrentStrength;
        public float OpponentCurrentStrength => opponentCurrentStrength;
        public int CurrentTime => (int)currentTime;

        // enum

        private enum BattleState
        {
            Preparing, Fighting, Ending
        }


        // Methods

        private void Awake()
        {
            persistenceData = Resources.Load<PersistenceData>("PersistenceData");
            if (persistenceData == null)
            {
                Debug.LogError("Persistence data is not found");
            }
        }

        private void Start()
        {
            playerAnimationTrigger.GetComponent<Animator>().speed = persistenceData.LiftSpeed;
            playerEarningPerLift = persistenceData.CurrentTrainingTool.EarningPerLift * persistenceData.EarningBonus;
            playerToolSprite.sprite = persistenceData.CurrentTrainingTool.ToolVisual;

            opponentAnimationTrigger.GetComponent<Animator>().speed = persistenceData.Fighter.LiftSpeed;
            opponentEarningPerLift = persistenceData.Fighter.TrainingTool.EarningPerLift;
            opponentAnimator.runtimeAnimatorController = persistenceData.Fighter.AnimatorController;
            opponentToolSprite.sprite = persistenceData.Fighter.TrainingTool.ToolVisual;

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
                        if (playerCurrentStrength >= opponentCurrentStrength)
                        {
                            persistenceData.Money += persistenceData.Fighter.Reward;

                            for (int i = 0; i < persistenceData.OpponentConfigs.Length; i++)
                            {
                                if (persistenceData.OpponentConfigs[i].Opponent == persistenceData.Fighter)
                                {
                                    if (i < persistenceData.OpponentConfigs.Length - 1)
                                    {
                                        persistenceData.OpponentConfigs[i + 1].IsUnlocked = true;
                                    }
                                }
                            }

                            resultPanel.TurnOnResultPanel(true, persistenceData.Fighter.Reward);
                        }
                        else
                        {
                            resultPanel.TurnOnResultPanel(false);
                        }

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
                        playerAnimationTrigger.AnimationTrigger((int)playerEarningPerLift);
                    }
                }
            }
        }

        private void HandleOpponentLift()
        {
            if (canOpponentLift)
            {
                canOpponentLift = false;
                opponentAnimationTrigger.AnimationTrigger((int)opponentEarningPerLift);
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
            opponentCurrentStrength += opponentEarningPerLift;
        }

        public void ResetCanOpponentLift()
        {
            canOpponentLift = true;
        }

        public void ReturnTrainingRoom()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
