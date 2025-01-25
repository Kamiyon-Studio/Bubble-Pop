using UnityEngine;

public class BubbleMovement : MonoBehaviour {
    [SerializeField] private float _upwardSpeed = 1f;
    [SerializeField] private float _sidewaysSpeed = 2f;
    [SerializeField] private float _sidewaysChangeInterval = 1f;

    private Rigidbody2D _rb;
    private float _sidewaysDirection;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _sidewaysDirection = Random.Range(-1f, 1f);
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (Time.time % _sidewaysChangeInterval < 0.1f) {
            _sidewaysDirection = Random.Range(-1f, 1f);
        }

        _rb.linearVelocity = new Vector2(_sidewaysDirection * _sidewaysSpeed, _upwardSpeed);
    }
}
