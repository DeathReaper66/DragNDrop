using UnityEngine;
using System;

public class DragNDropManager : MonoBehaviour
{
    public static DragNDropManager Instance { get; private set; }
    [NonSerialized] public Action OnSelectItem;
    [NonSerialized] public Action OnDropItem;

    private void Awake()
    {
        Instance = this;
    }
}
