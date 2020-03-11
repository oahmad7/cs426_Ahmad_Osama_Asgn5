using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMove : NetworkBehaviour {

    string player_name = "";

    float jump_force = 200f;

    public Text playerName;
    public AudioSource BirdFlap;  // audio for when the bird flaps its wings
    public AudioSource BirdTired; // audio for when the bird cant flap wings and is trired
    public AudioSource Music; // Game Music

    Camera camera;
    Rigidbody rigidbody;
    Transform transform;

    // Start is called before the first frame update
    void Start() {
        rigidbody = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {

        // affect other non-local player
        if (!isLocalPlayer) {

            // if current is host, set other as player 2
            if (isServer) {
                gameObject.name = "Player 2";
                playerName.color = Color.red;
            }

            // otherwise other is host, set as player 1
            else {
                gameObject.name = "Player 1";
                playerName.color = Color.blue;
            }

            // update its name tag
            playerName.text = gameObject.name;

            return;
        }

        // get x direction movement only
        var x = Input.GetAxis("Horizontal") * 0.1f;

        // TODO
        // make bird face left when moving left and right when moving right

        // jump action
        if (Input.GetKeyDown(KeyCode.Space)) {

            // random chance of 1 in 10 to get tired and not jump
            if (Random.Range(0, 10) == 0) {
                Debug.Log("Bird is tired!");
                BirdTired.Play();
            }

            // otherwise bird not tired so jump
            else {
                rigidbody.AddForce(transform.up * jump_force);
                BirdFlap.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            Music.Play();
        }

        // move in world space
        transform.Translate(x, 0, 0);
    }

    // disable reference to camera from new player that joined
    // http://www.doofah.com/tutorials/networking/unity-5-network-tutorial-part-3/
    private void Awake() {
        camera = GetComponentInChildren<Camera>();
        camera.gameObject.SetActive(false);
    }

    // local player connected to server
    public override void OnStartLocalPlayer() {

        // if player is also host, set name as player 1
        if (isServer) {
            gameObject.name = "Player 1";
            playerName.color = Color.blue;
        }

        // otherwise set guest name as player 2
        else {
            gameObject.name = "Player 2";
            playerName.color = Color.red;
        }

        //enable original camera for this player
        camera.gameObject.SetActive(true);

        // update player's object and UI with name
        player_name = gameObject.name;
        playerName.text = gameObject.name;
    }
}
