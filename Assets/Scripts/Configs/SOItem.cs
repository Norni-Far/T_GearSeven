using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Configs/Item", order = 0)]
[System.Serializable]
public class SOItem : ScriptableObject
{
    [field: SerializeField][field: ReadOnly] public int ItemID { get; private set; }
    [field: SerializeField] public Sprite itemIcon { get; private set; }

    public void SetItemID(int id)
    {
        ItemID = id;
    }
}