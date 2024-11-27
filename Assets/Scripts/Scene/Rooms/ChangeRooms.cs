using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRooms : MonoBehaviour
{
    public Camera cam;

    public WaveManager waveManager;

    private GameObject player;

    private bool changeCam = false;
    private bool triggered = false;

    private Vector3 initialPosition;

    public void Update()
    {
        if (changeCam && cam.transform.position != initialPosition + new Vector3(0, 11, 0)) {
            cam.transform.position = Vector3.Slerp(cam.transform.position, initialPosition + new Vector3(0, 11, 0), 10 * Time.deltaTime);
            player.transform.position = transform.position + new Vector3(0, 2, 0);
        } else if (changeCam && cam.transform.position == initialPosition + new Vector3(0, 11, 0)) {
            player.GetComponent<PlayerMovement>().enabled = true;
            waveManager.changeRoom();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) { return; }
        triggered = true;
        initialPosition = cam.transform.position;
        changeCam = true;
        player = collision.gameObject;
    }
}
