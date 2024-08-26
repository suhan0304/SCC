using UnityEngine;

public class TouchPad : MonoBehaviour
{
    public void OnTouch() {
        Events.OnTouchScreen?.Invoke();
    }
}
