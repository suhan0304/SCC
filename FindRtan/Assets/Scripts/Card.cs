using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public SpriteRenderer frontImage;

    public void Setting(int number) {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");
    }
}
