using System;
using UnityEngine;

class TakeWeapon {
    public static readonly int SWORD = 0;
    public static readonly int SHIELD = 1;
    public static readonly int CROSSBOW = 2;

    private int state;

    void setWeapon(int state) {
        this.state = state;
    }

    void attack() {
        if (state == SWORD) {
            Debug.Log("칼을 휘두르다");
        } else if (state == SHIELD) {
           Debug.Log("방패로 밀친다");
        } else if (state == CROSSBOW) {
            Debug.Log("석궁을 발사하다");
        }
    }
}