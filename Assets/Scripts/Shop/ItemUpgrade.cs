﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUpgrade : Item
{
    protected Upgrade upgrade;

    public Upgrade ObtainItem() {
        available = false;
        return upgrade;
    }
}