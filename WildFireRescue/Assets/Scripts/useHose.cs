using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class useHose : MonoBehaviour
{
    public PlayerController playerScript;
    public Transform playerSprite;
    public Transform itemCastPoint;
    public Animator spriteAnimator;
    public oxygenBarScript oxygenBarScript;
    private int layerMask = 1 << 9;
    public GameObject treeObject;
    public Animator burningTreeAnimator;
    private GameObject hitObject;
    IEnumerator hoseAnimationDelay()
    {
        yield return new WaitForSeconds(0.25f);
        spriteAnimator.SetBool("isUsingHose", false);
        oxygenBarScript.reduceOxygen();

        Transform burningTreePos = hitObject.transform;
        Destroy(hitObject);
        Object.Instantiate(treeObject, burningTreePos.position, burningTreePos.rotation);

        playerScript.playerScore = playerScript.playerScore + 30;
        playerScript.isUsingEquipment = false;
    }

    IEnumerator hoseSpraySoundDelay()
    {
        yield return new WaitForSeconds(0);
        playerScript.hoseSpraySound.Play();
    }

    public void usingHose()
    {
        RaycastHit2D hit = Physics2D.Raycast(itemCastPoint.position, playerSprite.transform.TransformDirection(Vector2.up), 1f, layerMask);
        Debug.DrawRay(itemCastPoint.position, playerSprite.transform.TransformDirection(Vector2.up));

        if (hit.collider.gameObject.name == "BurningTree")
        {
            playerScript.isUsingEquipment = true;
            spriteAnimator.SetBool("isUsingHose", true);
            burningTreeAnimator.SetBool("isBeingPutOut", true);
            hitObject = hit.collider.gameObject;
            StartCoroutine(hoseAnimationDelay());
            StartCoroutine(hoseSpraySoundDelay());
        }
    }
    
}
