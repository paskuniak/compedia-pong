using UnityEngine;

public class PaddleMotor : MonoBehaviour {

    private IPaddleInput myInput;
    private bool inputInitialized = false;
    private Vector3 tempPosition = new Vector3();
    private Settings settings;

    public void Init(Settings newSettings) {
        settings = newSettings;
    }

    public void InitInput() {
        myInput = GetComponent<IPaddleInput>();
        inputInitialized = true;
    }

    void Update () {
        if (inputInitialized) {
            transform.Translate(0, myInput.axisValue * settings.paddleSpeed * Time.deltaTime, 0);
            ClampY();
        }
	}

    private void ClampY() {
        tempPosition = transform.position;
        tempPosition.y = Mathf.Clamp(tempPosition.y, settings.paddleMinPositionY , settings.paddleMaxPositionY);
        transform.position = tempPosition;
    }

}
