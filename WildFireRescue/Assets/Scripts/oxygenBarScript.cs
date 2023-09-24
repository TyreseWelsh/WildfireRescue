using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oxygenBarScript : MonoBehaviour
{
    private Slider slider;
    public float oxygenDepletionNum;
    public GameObject player;
    private PlayerController playerScript;

    public void reduceOxygen()
    {
        slider.value = slider.value - oxygenDepletionNum;
        if (slider.value == 0)
        {
            playerScript.enableEndScreen();
        }
    }

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1f;
        playerScript = player.GetComponent<PlayerController>();
    }


}
