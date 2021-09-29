using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        //mendapatkan reference komponen
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        //set current health
        currentHealth = startingHealth;
    }


    void Update ()
    {
        //cek jika sinking
        if (isSinking)
        {
            //memindahkan objek ke bawah
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        //cek jika dead
        if (isDead)
            return;
        
        //play audio
        enemyAudio.Play();

        //kurangi health
        currentHealth -= amount;

        //ganti posisi partikel
        hitParticles.transform.position = hitPoint;

        //play particle system
        hitParticles.Play();

        //dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death()
    {
        //set isDead
        isDead = true;

        //set CapsuleCollider ke trigger 
        capsuleCollider.isTrigger = true;

        //trigger play animation dead
        anim.SetTrigger("Dead");

        //play sound dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


    public void StartSinking()
    {
        //disable navMesh component
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

        //set rigidbody to kinematic
        GetComponent<Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}//class
