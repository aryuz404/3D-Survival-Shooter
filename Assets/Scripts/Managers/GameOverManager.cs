using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject restartButton;       
    public float restartDelay = 5f;
    public Text warningText;
                


    Animator anim;                         
    float restartTimer;
    bool isDead = false;                    


    void Awake()
    {
        anim = GetComponent<Animator>();
        restartButton.SetActive(false);
    }


    void Update()
    {

        if (playerHealth.currentHealth <= 0 && !isDead)
        {
            anim.SetTrigger("GameOver");

            isDead = true;

            restartButton.SetActive(true);

        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}//