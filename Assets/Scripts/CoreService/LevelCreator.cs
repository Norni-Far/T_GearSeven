using UnityEngine;
using VContainer;

namespace CoreService
{
    public class LevelCreator : MonoBehaviour
    {
        [Inject] private IItemHolder _itemHolder;

        private void Start()
        {
            if (_itemHolder != null)
            {
                Debug.Log("Items loaded successfully = " + _itemHolder.GetItems().Count);
            }
            else
            {
                Debug.LogError("No items found");
            }
        }
    }
}