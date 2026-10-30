using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Item", menuName = "Configs/Item", order = 0)]
    public class SOItem : ScriptableObject
    {
        [field: SerializeField][field: Min(0)] public int ItemID { get; private set; } = 0;
        [field: SerializeField] public Sprite ItemIcon { get; private set; }
    }
}
