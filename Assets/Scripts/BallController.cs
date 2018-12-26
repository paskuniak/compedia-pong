using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour {

    private IGameScoreManager manager;
    private Settings settings;
    private Rigidbody2D rb;

	void Awake () {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(IGameScoreManager sm, Settings newSettings) {
        manager = sm;
        settings = newSettings;
    }

    public void StartNewPoint(bool delay) {

        CancelInvoke("UpdateVelocity");
        CancelInvoke("StartNewPointDelayed");

        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        if (delay) {
            Invoke("StartNewPointDelayed", settings.messageDisplayTime);
        } else {
            StartNewPointDelayed();
        }
    }

    private void StartNewPointDelayed() {

        do {
            rb.velocity = Random.insideUnitCircle * settings.maxBallSpeed;
        } while ((Mathf.Abs(rb.velocity.y) < settings.minBallSpeed) ||
                 (Mathf.Abs(rb.velocity.x) < settings.minBallSpeed));

        Invoke("UpdateVelocity", settings.updateBallSpeedInterval);
    }

    private void UpdateVelocity() {
        rb.velocity *= settings.updateBallSpeedFactor;
        Invoke("UpdateVelocity", settings.updateBallSpeedInterval);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Goal") {
            if (collision.name.Contains("1")) {
                manager.AddPoints(1, settings.pointsPerGoal);
            } else {
                manager.AddPoints(2, settings.pointsPerGoal);
            }
        }
    }

}
