using UnityEngine;

namespace ViewService
{
    public class ShelfView : MonoBehaviour
    {
        [SerializeField] private Transform[] _slots = new Transform[GameLogic.MatchGame.ShelfCapacity];

        public Transform GetSlot(int index)
        {
            return _slots[index];
        }
    }
}
