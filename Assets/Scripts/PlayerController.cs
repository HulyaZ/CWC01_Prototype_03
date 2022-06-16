using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Rigidbody playerRB;
    Animator playerAnim;

    public ParticleSystem crashParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    AudioSource playerAudio;

    public float jumpForce = 1000f;
   
    public bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0.0f, -45f, 0f);        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            isOnGround = false;
                        
            dirtParticle.Stop();
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();

        } 

        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);

            gameOver = true;    
            
            float startVolume = playerAudio.volume;
            float fadeTime = 0.3f;
           
            
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
                       
            crashParticle.Play();
            StartCoroutine(waitFor(3f));
      
        }
    }

    IEnumerator waitFor(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);      
        SceneManager.LoadScene("Prototype 3");
    }

    }
