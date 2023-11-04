using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Pri, increase this value to make it less smooth and vice-versa
    public float smoothTime = 0.01f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // If player is found, then we "SmoothDamp" to the player. Look up SmoothDamp, Lerps and Slerps in Unity docs, they are essential to 
        // good camera work and smooth animations
        if (player)
        {
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
