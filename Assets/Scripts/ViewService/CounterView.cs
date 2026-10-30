using UnityEngine;

namespace ViewService
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private Transform[] _slots = new Transform[GameLogic.MatchGame.CounterCapacity];

        public Transform GetSlot(int index)
        {
            return _slots[index];
        }
    }
}
