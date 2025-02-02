using UnityEngine;

public class MCamera : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);
    }
}
