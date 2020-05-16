using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    private ShopSlot[] shopSlots;

    // Start is called before the first frame update
    void Start()
    {
        shopSlots = GetComponents<ShopSlot>();
    }


}
