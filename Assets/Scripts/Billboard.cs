using UnityEngine;

public class Billboard : MonoBehaviour {

    // Update is called once per frame
    void Update() {

        // show player name tag
        transform.LookAt(Camera.main.transform);
    }
}
