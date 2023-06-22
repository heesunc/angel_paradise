using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private UIManager uiManager;
    //Player Movment
    public float verticalInput, horizonInput;
    public float speed, runSpeed;
    private Vector3 vector;
    private bool keyDown = false;
    int walkCount = 10;
    private Animator animator;

    //Player Ability
    public enum PlayerAbility
    {
        normal,
        superPower,
        electricity,
        magnetic,
        hacking
    }
    private PlayerAbility currentAbility;
    public void SetPlayerAbility(PlayerAbility a)
    {
        currentAbility = a;
        Debug.Log(currentAbility);
    }
    public PlayerAbility GetPlayerAbility()
    {
        return currentAbility;
    }

    //Player Emotion
    public enum PlayerEmotion
    {
        fine,
        glad,
        sad,
        joy,
        angry
    }
    private PlayerEmotion currentEmotion;

    public void SetPlayerAbility(PlayerEmotion e)
    {
        currentEmotion = e;
        Debug.Log(currentEmotion);
    }
    public PlayerEmotion GetPlayerEmotion()
    {
        return currentEmotion;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    IEnumerator MoveCoroutine()
    {
       while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift)) //달리기
            {
                animator.SetBool("Running", true);
                runSpeed = speed * 0.5f;
            }
            else
            {
                runSpeed = 0;
                animator.SetBool("Running", false);
            }

            verticalInput = Input.GetAxisRaw("Vertical");
            horizonInput = Input.GetAxisRaw("Horizontal");
            vector.Set(horizonInput * (speed + runSpeed), verticalInput * (speed + runSpeed), transform.position.z);

            //if (vector.x != 0)  //대각선 이동 방지
            //    vector.y = 0;
         
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            for (int i = 0; i < walkCount; i++)
            {
                transform.Translate(vector);
                yield return new WaitForSeconds(0.01f);
            }
        }

        animator.SetBool("Walking", false);
        animator.SetBool("Running", false);
        keyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!keyDown && !uiManager.isActiveUI)
        {
            
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Walking", true);
                keyDown = true;
                StartCoroutine(MoveCoroutine());
            }
        }

        if (Input.GetKey(KeyCode.Escape) && currentAbility != PlayerAbility.normal)
            SetPlayerAbility(PlayerAbility.normal);

        if (Input.GetKey(KeyCode.Alpha1))
            SetPlayerAbility(PlayerAbility.superPower);
        else if (Input.GetKey(KeyCode.Alpha2))
            SetPlayerAbility(PlayerAbility.electricity);
        else if (Input.GetKey(KeyCode.Alpha3))
            SetPlayerAbility(PlayerAbility.magnetic);
        else if (Input.GetKey(KeyCode.Alpha4))
            SetPlayerAbility(PlayerAbility.hacking);
    }
}
