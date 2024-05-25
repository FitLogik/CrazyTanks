using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    [SerializeField] Collider2D _collider;


    private void Awake()
    {
        if (_collider == null)
        {
            _collider = GetComponent<PolygonCollider2D>();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Grounded!");
            IsGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Not grounded!");
            IsGrounded = false;
        }
    }

}
