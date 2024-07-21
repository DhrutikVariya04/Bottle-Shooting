using UnityEngine;

[RequireComponent(typeof(Explodable))]
public class Ball : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject,4);
        Slingshot.Checking = true;
    }
}
