using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Points : NetworkBehaviour {

    [SyncVar]
    public int points = 0;
    public const int MAX_POINTS = 5;
    public Text score;
    public AudioSource GameWin; // audio for when a player wins

    // inform players of objective
    void Start() {

        // display message only on local player
        if (isLocalPlayer) {

            // blue text for player 1
            if (isServer) {
                score.color = Color.blue;
            }

            // red text for player 2
            else {
                score.color = Color.red;
            }
            score.text = "Collect " + MAX_POINTS + " chips!";
        }
    }

    // add a point to the specified player and check for win condition
    public void addPoint(int player) {

        // if current player is host and got the point
        if (isServer && player == 1) {

            // update player 1 score text
            score.text = "Computer Chips:  " + ++points;
            score.color = Color.blue;

            Debug.Log("p1:\t" + points);
        }

        // if current player is not host and got the point
        if (!isServer && player == 2) {

            // update player 2 score text
            score.text = "Computer Chips:  " + ++points;
            score.color = Color.red;

            Debug.Log("p2:\t" + points);
        }

        // if a player got max points, notify them
        if (points == MAX_POINTS) {

            // reset points
            points = 0;

            // notify of completion
            GameWin.Play();
            score.text = " Computer Hacked!";
            // TODO

            // play sound effect, balloon particles, or teleport to poduium/trophy room

            // don't use
            //RpcWin(2);
        }
    }

    // has issues when non-host calls this function, don't use
    [ClientRpc]
    void RpcWin(int player) {

        // update score text with winning player
        score.text = "P" + player + " wins!";
    }
}
