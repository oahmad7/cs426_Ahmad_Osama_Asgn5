using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HardwareSpawner : NetworkBehaviour {

    public GameObject prefab_1, prefab_2;

    public GameObject spawn0, spawn1, spawn2, spawn3, spawn4, spawn5, spawn6, spawn7, spawn8, spawn9;

    public const int MAX_HARDWARE = 10;

    public override void OnStartServer() {

        GameObject[] prefabs = {prefab_1, prefab_2};

        GameObject pf;

        // list of possible Hardware locations
        List<Vector3> locs = new List<Vector3>();
        locs.Add(spawn0.transform.position);
        locs.Add(spawn1.transform.position);
        locs.Add(spawn2.transform.position);
        locs.Add(spawn3.transform.position);
        locs.Add(spawn4.transform.position);
        locs.Add(spawn5.transform.position);
        locs.Add(spawn6.transform.position);
        locs.Add(spawn7.transform.position);
        locs.Add(spawn8.transform.position);
        locs.Add(spawn9.transform.position);

        // spawn a new Hardware for each of the two players
        for (int i = 0; i < MAX_HARDWARE; i++) {

            // get an unused spawn location and then remove it
            Vector3 pos = locs[Random.Range(0, locs.Count)];
            locs.Remove(pos);

            // set blue Hardware for player 1
            if (i % 2 == 0) {
                pf = prefabs[0];
            }

            // otherwise set red for player 2
            else {
                pf = prefabs[1];
            }

            // create and spawn the Hardware at the specified position
            GameObject hardware = (GameObject) Instantiate(pf, pos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
            NetworkServer.Spawn(hardware);
        }
    }
}
