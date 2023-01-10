using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerManager : MonoBehaviour
{
    #region Singleton
    public static HungerManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public Hunger hunger;
    
}