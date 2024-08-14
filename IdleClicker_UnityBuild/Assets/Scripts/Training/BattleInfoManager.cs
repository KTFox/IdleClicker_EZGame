using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker
{
    public class BattleInfoManager : MonoBehaviour
    {
        // Variables

        [SerializeField] private List<OpponentSO> opponenets = new List<OpponentSO>();

        // Properties

        public List<OpponentSO> Opponenets => opponenets;

        // Methods
    }
}
