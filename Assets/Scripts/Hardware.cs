using UnityEngine;
using UnityEngine.Networking;

public class Hardware : NetworkBehaviour {

    public AudioClip ItemPickup; // audio for when bird picks up an item

    // Update is called once per frame
    void Update() {

        // rotate object on y-axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * 50f, 0f), Space.World);
    }

    // decide what to do when a player hits a hardware piece
    void OnCollisionEnter(Collision other) {

        // collision with blue hardware and player 1 (host)
        if (gameObject.name.Contains("p1") && other.collider.gameObject.name.Contains("1")) {

            // get colliding player
            GameObject player = other.gameObject;

            // get colliding player's point value
            Points point = player.GetComponent<Points>();

            // add point to player 1, play pickup sound and remove the hardware from server
            if (point != null) {
                point.addPoint(1);
                AudioSource.PlayClipAtPoint(ItemPickup, transform.position);
                NetworkServer.Destroy(gameObject);
            }
        }

        // otherwise collision with red hardware and player 2 (guest)
        if (gameObject.name.Contains("p2") && other.collider.gameObject.name.Contains("2")) {

            // get colliding player
            GameObject player = other.gameObject;

            // get colliding player's point value
            Points point = player.GetComponent<Points>();

            // add point to player 2, play pickup sound and remove the hardware from server
            if (point != null) {
                point.addPoint(2);
                AudioSource.PlayClipAtPoint(ItemPickup, transform.position);
                NetworkServer.Destroy(gameObject);
            }
        }
    }
}
