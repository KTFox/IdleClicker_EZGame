using System;
using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker
{
    public class BattleInfoManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private List<OpponentSO> opponenets = new List<OpponentSO>();

        private OpponentConfig[] opponentConfigs;

        // Properties

        public List<OpponentSO> Opponenets => opponenets;
        public OpponentConfig[] OpponentConfigs => opponentConfigs;

        // Structs

        public class OpponentConfig
        {
            public OpponentSO Opponent;
            public bool IsUnlocked;
        }

        // events

        public event Action OnOpponentConfigUpdate;


        // Methods

        private void Awake()
        {
            opponentConfigs = new OpponentConfig[opponenets.Count];

            for (int i = 0; i < opponenets.Count; i++)
            {
                OpponentConfig config = new OpponentConfig();
                config.Opponent = opponenets[i];

                if (i == 0)
                {
                    config.IsUnlocked = true;
                }
                else
                {
                    config.IsUnlocked = false;
                }

                opponentConfigs[i] = config;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                UnlockOpponent(opponenets[1]);
            }
        }

        public void UnlockOpponent(OpponentSO opponent)
        {
            foreach (var opponentConfig in opponentConfigs)
            {
                if (opponentConfig.Opponent == opponent)
                {
                    opponentConfig.IsUnlocked = true;
                    OnOpponentConfigUpdate?.Invoke();
                }
            }
        }
    }
}
