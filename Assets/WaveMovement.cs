using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveMovement : MonoBehaviour
{
    private SpriteRenderer _waveSprite;
    private bool _isMovingLeft;
    private float _speed = 1;
    private PolygonCollider2D _collider;

    private void Start()
    {
        _waveSprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (Camera.main) Camera.main.transform.position = new Vector3(0, transform.position.y, -10);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMovingLeft = !_isMovingLeft;
        }

        if (_isMovingLeft)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
            transform.position += transform.right * (Time.deltaTime * _speed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
            transform.position += -transform.right * (Time.deltaTime * _speed);
        }

        if (IsOutOfCameraBounds())
        {
            RestartGame();
        }

        _speed += Time.deltaTime * 0.1f;
        Debug.Log(_speed);
    }

    private bool IsOutOfCameraBounds()
    {
        var cam = Camera.main;

        var viewportPosition = cam.WorldToViewportPoint(transform.position);

        // Check if the object is out of the camera bounds
        return viewportPosition.x < 0 || viewportPosition.x > 1 || viewportPosition.y < 0 || viewportPosition.y > 1;
    }

    private static void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RestartGame();
    }
}
