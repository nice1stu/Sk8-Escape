using UnityEngine;
using UnityEngine.UI;

public class BackgroundView : MonoBehaviour
{
    public Transform player;
    public float parallaxSpeed = 0.5f;

    private Vector3 previousPlayerPosition;

    void Start () {
        previousPlayerPosition = player.position;
    }

    void Update () {
        float parallax = (previousPlayerPosition.x - player.position.x) * parallaxSpeed;
        transform.position = new Vector3(transform.position.x + parallax, transform.position.y, transform.position.z);
        previousPlayerPosition = player.position;
    }
}