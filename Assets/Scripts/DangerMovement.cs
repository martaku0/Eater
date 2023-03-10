using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    Vector3 lastVelocity;
    Vector3 direction;
    float rot;

    private void Awake()
    {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        otherObjects = GameObject.FindGameObjectsWithTag("Danger");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        otherObjects = GameObject.FindGameObjectsWithTag("Button");

        foreach (GameObject obj in otherObjects)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    void Start()
    {
        rot = Random.Range(-20f, 20f);
        while (rot < 5f && rot > -5f)
        {
            rot = Random.Range(-20f, 20f);
        }
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(rot * Time.deltaTime * moveSpeed, rot * Time.deltaTime * moveSpeed));
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveSpeed = lastVelocity.magnitude;
            direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(moveSpeed, 0f);
        }
    }
}
