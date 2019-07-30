using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasButton : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    public void ToggleGas()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        playerController.gasDown = !playerController.gasDown;
    }
}
