using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private Vector3 spawnValues;
    [SerializeField] private float spawnWait;
    [SerializeField] private int startWait;
    [SerializeField] private bool stopSpawn;
    [SerializeField] private bool pauseBool;
    [SerializeField] private int randShapeLimit = 0;
    private int randObstacles;

    [SerializeField] private Image PowerBar;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text pauseText;
    public GameObject gameOverText;
    public int score;
    public float ObstacleSpeed;
    public float CharacherMoveSpeed = 10f;
    public float CharacherJumpSpeed = 7f;

    private void Start()
    {
        StartCoroutine(WaitSpawner());
    }

    // Objelerin üretilmesi
    private IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stopSpawn)
        {
            randObstacles = Random.Range(0, randShapeLimit);

            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);

            Instantiate(obstacles[randObstacles], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);

            yield return new WaitForSeconds(spawnWait);
        }
    }
    
    public void GetPoint()
    {
       score++;
       scoreText.text = "" + score.ToString();
       PowerBar.fillAmount = score *Time.deltaTime * 1;
    }
    
    public void PauseButton()
    {
        if (pauseBool == false)
        {
             Time.timeScale = 0;
             pauseBool = true;
             pauseText.text = "Play";
        }
        else
        {
            Time.timeScale = 1;
             pauseBool = false;
             pauseText.text = "Pause";
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MenuScene");
    }  
 }
