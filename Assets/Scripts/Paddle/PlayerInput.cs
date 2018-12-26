using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPaddleInput {

    private KeyCode upKey;
    private KeyCode downKey;
    private float inputValue;

    private bool initialized = false;

    public float axisValue {
        get {
            inputValue = 0;
            if (initialized) {
                if (Input.GetKey(upKey)) inputValue++;
                if (Input.GetKey(downKey)) inputValue--;
            }
            return inputValue;
        }
    }

    public void SetInputKeys(KeyCode up, KeyCode down) {
        upKey = up;
        downKey = down;
        initialized = true;
    }

    public void Destroy() {
        DestroyImmediate(this);
    }

}
