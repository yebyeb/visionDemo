using UnityEngine;
public class CharacterControl : MonoBehaviour
{  
    private GameControl GM;
    private bool obstacleTouchGround = true;
    private Rigidbody rb;
    private Vector3 touchPosition;
    private Vector3 mousePosition;
    private Vector3 direction;

    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;

    public float swipeRange;
    public float tapRange;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GM = FindObjectOfType<GameControl>(); 
    }

     private void Update()
    {
        // Karakter aşağı düşerse oyun biter.
        if (transform.localPosition.y < -1f)
        {
            GM.gameOverText.SetActive(true);
            StartCoroutine(GM.GameOver());
        }

        TouchControl();  
        MouseControl();
    }


    // Cihaz için sağ, sol hareket ve tek dokunuşla zıplama.
    public void TouchControl()
    {
#if UNITY_ANDROID        

     if (Input.touchCount > 0)
       {
        if (Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.y < Screen.height / 1.2f )
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        
        if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).position.y < Screen.height / 1.2f )
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentPosition - startTouchPosition;

                if (Distance.x < -swipeRange)
                {
                    rb.velocity = new Vector2(-GM.CharacherMoveSpeed,rb.velocity.y);
                }
                else if (Distance.x > swipeRange)
                {
                    rb.velocity = new Vector2(GM.CharacherMoveSpeed,rb.velocity.y);

                }

        }
        

        if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).position.y < Screen.height / 1.2f )
        {
            

            endTouchPosition = Input.GetTouch(0).position;

            rb.velocity = new Vector2(0,rb.velocity.y);

            Vector2 Distance = endTouchPosition - startTouchPosition;

              if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange && obstacleTouchGround)
            {
                rb.AddForce(Vector3.up * GM.CharacherJumpSpeed, ForceMode.Impulse);
                     obstacleTouchGround = false;
            }

        }
      }   

#endif
    }
    // Editörde test edebilmek için sağa, sola ve yukarı zıplama özelliklerini farenin 0,1,2 komutları ile eşleştirdim. 
    public void MouseControl()
    {
#if UNITY_EDITOR        
           Vector3 mousePos = Input.mousePosition;
         if (mousePos.y < Screen.height / 1.2f )   
        {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - transform.position).normalized;
        if (Input.GetMouseButton(0))
        {
            rb.velocity = new Vector2(GM.CharacherMoveSpeed,rb.velocity.y);
        }
        else if (Input.GetMouseButton(1))
        {
            rb.velocity = new Vector2(-GM.CharacherMoveSpeed,rb.velocity.y);
        }
        else
        {
           rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetMouseButton(2) && obstacleTouchGround)
        {          
            rb.AddForce(Vector3.up * GM.CharacherJumpSpeed, ForceMode.Impulse);
            obstacleTouchGround = false;
        }   
        }
#endif    
    }

    // Engellerin üstünden atlaması halinde puan kazanması ve engellere dokunması halinde oyunun bitnesi
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
           GM.gameOverText.SetActive(true);
           StartCoroutine(GM.GameOver());
        }
        if (collision.collider.tag == "Terrain")
        {
           obstacleTouchGround = true;
        }
    }
}
