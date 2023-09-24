using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public GameObject Player;
    public GameObject playerSprite;
    public Transform spriteTransform;
    public Animator spriteAnimator;
    public Transform castPointTop;
    public Transform castPointLeft;
    public Transform castPointRight;
    public Transform castPointBottom;
    public Rigidbody2D rb2d;
    public Collider2D col;
    public AudioSource axeChopSound;
    public AudioSource hoseSpraySound;
    public float moveDistance;
    private int playerDirection = 4;
    public float numAvailableAnimals;
    private int numAnimalsRescued;
    public int playerScore;
    public AnimalScript animalScript;
    public useAxe axeScript;
    public useHose hoseScript;
    public GameObject oxygenBar;
    public oxygenBarScript oxygenBarScript;
    private Slider oxygenBarSlider;
    private static int tileLayerMask = 1 << 9;
    public GameObject endScreenCanvas;
    public GameObject pauseMenuCanvas;
    private bool isPauseScreenUp = false;
    private Text rescueText;
    private Text scoreText;
    public bool isUsingEquipment;
    private bool inputEnabled = true;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb2d = GetComponent<Rigidbody2D>();
        axeScript = GetComponent<useAxe>();
        col = GetComponent<Collider2D>();
        rescueText = endScreenCanvas.transform.GetChild(3).GetComponent<Text>();
        scoreText = endScreenCanvas.transform.GetChild(2).GetComponent<Text>();
        oxygenBarSlider = oxygenBar.GetComponent<Slider>();
        

    }

    //Function to enable level end screen when called
    public void enableEndScreen()
    {
        Time.timeScale = 0;
        endScreenCanvas.SetActive(true);
        scoreText.text = "Score: " + playerScore;
        rescueText.text = "You Rescued " + numAnimalsRescued + " Animal/s!";
    }

    IEnumerator axeChopSoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        axeChopSound.Play();
    }

    // Update is called once per frame
    void Update()
    {

        //player moves left
        if (Input.GetKeyDown(KeyCode.A) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                //Casting raycast from a castpoint in its direction, which is stored as variable "hit"
                RaycastHit2D hit = Physics2D.Raycast(castPointLeft.transform.position, Vector2.left, 1f, tileLayerMask);
                Debug.DrawRay(castPointLeft.transform.position, Player.transform.TransformDirection(Vector2.left));

                //If raytrace hits object tagged "GroundTile" or "Animal" do the following
                if (hit.collider.gameObject.CompareTag("GroundTile") || hit.collider.gameObject.CompareTag("Animal"))
                {
                    //Movement and setting facing direction variable
                    rb2d.MovePosition(transform.position - transform.right * moveDistance);
                    spriteTransform.localEulerAngles = new Vector3(0, 0, 90);
                    playerDirection = 2;

                    oxygenBarScript.reduceOxygen();
                }

            }
        }

        //Player moves right
        else if (Input.GetKeyDown(KeyCode.D) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                //Casting raycast from a castpoint in its direction, which is stored as variable "hit"
                RaycastHit2D hit = Physics2D.Raycast(castPointRight.transform.position, Vector2.right, 1f, tileLayerMask);
                Debug.DrawRay(castPointRight.transform.position, Player.transform.TransformDirection(Vector2.right));

                //If raytrace hits object tagged "GroundTile" or "Animal" do the following
                if (hit.collider.gameObject.CompareTag("GroundTile") || hit.collider.gameObject.CompareTag("Animal"))
                {
                    //Movement and setting facing direction variable
                    rb2d.MovePosition(transform.position + transform.right * moveDistance);
                    spriteTransform.localEulerAngles = new Vector3(0, 0, -90);
                    playerDirection = 4;

                    oxygenBarScript.reduceOxygen();
                }
            }
        }

        //Player moves up
        else if (Input.GetKeyDown(KeyCode.W) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                //Casting raycast from a castpoint in its direction, which is stored as variable "hit"
                RaycastHit2D hit = Physics2D.Raycast(castPointTop.transform.position, Vector2.up, 1f, tileLayerMask);
                Debug.DrawRay(castPointTop.transform.position, Player.transform.TransformDirection(Vector2.up));

                //If raytrace hits object tagged "GroundTile" or "Animal" do the following
                if (hit.collider.gameObject.CompareTag("GroundTile") || hit.collider.gameObject.CompareTag("Animal"))
                {
                    //Movement and setting facing direction variable
                    rb2d.MovePosition(transform.position + transform.up * moveDistance);
                    spriteTransform.localEulerAngles = new Vector3(0, 0, 0);
                    playerDirection = 1;

                    oxygenBarScript.reduceOxygen();
                }
            }
        }

        //Player moves down
        else if (Input.GetKeyDown(KeyCode.S) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                //Casting raycast from a castpoint in its direction, which is stored as variable "hit"
                RaycastHit2D hit = Physics2D.Raycast(castPointBottom.transform.position, Vector2.down, 1f, tileLayerMask);
                Debug.DrawRay(castPointBottom.transform.position, Player.transform.TransformDirection(Vector2.down));

                //If raytrace hits object tagged "GroundTile" do the following
                if (hit.collider.gameObject.CompareTag("GroundTile") || hit.collider.gameObject.CompareTag("Animal"))
                {
                    //Movement and setting facing direction variable
                    rb2d.MovePosition(transform.position - transform.up * moveDistance);
                    spriteTransform.localEulerAngles = new Vector3(0, 0, 180);
                    playerDirection = 3;

                    oxygenBarScript.reduceOxygen();
                }
            }
        }

        //Player uses axe
        if (Input.GetKeyDown(KeyCode.Space) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                axeScript.usingAxe();
                axeChopSoundDelay();
            }
        }

        //Player uses hose
        if (Input.GetKeyDown(KeyCode.LeftShift) && inputEnabled == true)
        {
            if (isUsingEquipment == false)
            {
                hoseScript.usingHose();
            }
        }
        
        //Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPauseScreenUp == false)
            {
                pauseMenuCanvas.SetActive(true);
                isPauseScreenUp = true;
                inputEnabled = false;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenuCanvas.SetActive(false);
                isPauseScreenUp = false;
                inputEnabled = true;
                Time.timeScale = 1;
            }
            
        }

    }

    //General collision
    void OnTriggerEnter2D(Collider2D other)
    {     
        //If collided object has tag "Animal" do the following
        if (other.gameObject.CompareTag("Animal"))
        {
            //Increment numAnimalsRescued variable by 1
            numAnimalsRescued = numAnimalsRescued + 1;
            playerScore = playerScore + 100;

            if (numAnimalsRescued >= numAvailableAnimals)
            {
                enableEndScreen();
            }
        }
    }
}
