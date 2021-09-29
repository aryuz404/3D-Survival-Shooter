using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;                                                
    bool damaged;                                               


    void Awake()
    {
        //mendapatkan reference komponen
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

        currentHealth = startingHealth;
    }


    void Update()
    {
        //jika terkena damage
        if (damaged)
        {
            //mengubah warna gambar menjadi value dari flashcolour
            damageImage.color = flashColour;
        }
        else
        {
            //fadeout damage image
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        //set damage to false
        damaged = false;
    }


    //fungsi untuk mendapatkan damage
    public void TakeDamage(int amount)
    {
        damaged = true;

        //mengurangi health
        currentHealth -= amount;

        //mengubah tampilan dari health slider
        healthSlider.value = currentHealth;

        //memainkan suara ketika terkena damage
        playerAudio.Play();

        //memanggil method death() jika darahnya <= 0 dan belum mati
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    
    //fungsi untuk mati
    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        //trigger animasi Die
        anim.SetTrigger("Die");
        
        //memainkan suara ketika mati
        playerAudio.clip = deathClip;
        playerAudio.Play();

        //mematikan script player movement
        playerMovement.enabled = false;

        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        //load ulang scene dgn index 0 pada build setting
        SceneManager.LoadScene(0);
    }
}//class
