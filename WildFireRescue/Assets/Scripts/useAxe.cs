using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useAxe : MonoBehaviour
{
    public GameObject Player;
    public PlayerController playerScript;
    public Transform playerSprite;
    public Transform itemCastPoint;
    public Animator spriteAnimator;
    public oxygenBarScript oxygenBarScript;
    private int layerMask = 1 << 9;
    private GameObject hitObject;

    public void usingAxe()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(itemCastPoint.position, playerSprite.transform.TransformDirection(Vector2.up) , 1f, layerMask);
        Debug.DrawRay(itemCastPoint.position, playerSprite.transform.TransformDirection(Vector2.up));
        if (hit.collider.gameObject.name == "Tree" || hit.collider.gameObject.name == "Tree(Clone)") 
        {
            hitObject = hit.collider.gameObject;
            playerScript.isUsingEquipment = true;
            spriteAnimator.SetBool("isUsingAxe", true);
            StartCoroutine(axeAnimationDelay());
            StartCoroutine(axeChopSoundDelay());
        }
    }

    IEnumerator axeAnimationDelay()
    {
        yield return new WaitForSeconds(0.75f);
        spriteAnimator.SetBool("isUsingAxe", false);
        oxygenBarScript.reduceOxygen();
        Destroy(hitObject);
        playerScript.playerScore = playerScript.playerScore + 20;
        playerScript.isUsingEquipment = false;
    }

    IEnumerator axeChopSoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        playerScript.axeChopSound.Play();
    }
}
