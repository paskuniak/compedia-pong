using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInput : MonoBehaviour, IPaddleInput {

    private bool initialized = false;
    private Transform verticalTarget;

    public float axisValue {
        get {
            if (initialized) {
                if (Mathf.Abs(transform.position.y - verticalTarget.position.y) > 0.5f) {
                    return (transform.position.y > verticalTarget.position.y) ? -1 : 1;
                }
            } 
            return 0;
        }
    }

    public void SetTarget(Transform target) {
        verticalTarget = target;
        initialized = true;
    }

    public void Destroy() {
        DestroyImmediate(this);
    }
}
