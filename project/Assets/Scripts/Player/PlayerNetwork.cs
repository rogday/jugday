using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwork : NetworkBehaviour{
    public GameObject camera;

    public override void OnStartLocalPlayer() {
        GetComponent<PlayerController>().enabled = true;
        camera.SetActive(true);
    }
}
