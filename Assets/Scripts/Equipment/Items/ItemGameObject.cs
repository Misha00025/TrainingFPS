using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGameObject : MonoBehaviour
{
    [SerializeField] private Item _item;

    public Item Pickup()
    {
        //throw new System.NotImplementedException();
        Destroy(gameObject);
        return _item;
    }
}
