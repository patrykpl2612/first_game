using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite healthPotionSprite;
    public Sprite coinSprite;
    public Sprite muszla1Sprite;
    public Sprite muszla2Sprite;
    public Sprite muszla3Sprite;
    public Sprite muszla4Sprite;
    public Sprite muszla5Sprite;

}
