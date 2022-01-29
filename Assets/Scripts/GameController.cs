using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public Camera cam;
    public GameObject ball;
    public GameObject gameOverOverlay;
    public TextMeshProUGUI timerText;
    public float timeLeft;

    [Range(1.0f, 1.8f)] public float minSpawn;
    [Range(2.0f, 3.0f)] public float maxSpawn;

    private Rigidbody2D _rigidbody;
    private Renderer _ballRenderer;
    private float maxWidth;

    private void Start()
    {
        if (cam == null) cam = Camera.main;

        // init in case we forget to set it in the inspector
        if (timeLeft == 0) timeLeft = 10;

        _rigidbody = GetComponent<Rigidbody2D>();
        _ballRenderer = ball.GetComponent<Renderer>();

        var upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        var targetWidth = cam.ScreenToWorldPoint(upperCorner);
        var ballWidth = _ballRenderer.bounds.extents.x;
        maxWidth = targetWidth.x - ballWidth;

        StartCoroutine(Spawn());
        updateTimerText();
    }

    void FixedUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = 0;
        }

        updateTimerText();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2);

        while (timeLeft > 0)
        {
            var spawnPosition = new Vector3(Random.Range(-maxWidth, +maxWidth), transform.position.y, 0.0f);
            var spawnRotation = Quaternion.identity;

            Instantiate(ball, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(Random.Range(minSpawn, maxSpawn));
        }

        // Give some delay when appearing so it doesn't happen
        // immediately
        yield return new WaitForSeconds(2);
        gameOverOverlay.SetActive(true);

    }

    void updateTimerText()
    {
        timerText.text = "Time Left:\n" + Mathf.RoundToInt(timeLeft);
    }
}