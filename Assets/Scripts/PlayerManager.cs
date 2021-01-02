using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public bool IsMove;
    public bool[] canMoveArr = new bool[4];
    public GameObject ar1;
    public GameObject ar2;
    public GameObject ar3;
    public GameObject ar4;
    public int playerDir;
    RaycastHit hit;         //hit를 감지하는 변수
    public Vector3 MoveVector;

    public int TotalMoveTick;
    public int currentMoveTick;
    public float Speed;
    private IEnumerator coroutine;
    public int count;

    public int totalTick;
    public int nowTick;
    public bool reStartCoroutine=false;

    public GameObject QuestObject;
    public GameObject QuestBomb;

    public GameObject UIObject;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = ShowCanMove();
    }

    // Update is called once per frame

    public void FindCanMove()
    {
        Debug.DrawRay(transform.position, transform.forward*5f, Color.red, 1f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canMoveArr[0] = false;

            }
        }
        else
        {
            canMoveArr[0] = true;
        }
        Debug.DrawRay(transform.position, transform.right * 5f, Color.red, 1f);

        if (Physics.Raycast(transform.position, transform.right, out hit, 5f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canMoveArr[1] = false;

            }
        }
        else
        {
            canMoveArr[1] = true;
        }
        Debug.DrawRay(transform.position, -transform.forward * 5f, Color.red, 1f);

        if (Physics.Raycast(transform.position, -transform.forward, out hit, 5f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canMoveArr[2] = false;

            }
        }
        else
        {
            canMoveArr[2] = true;
        }
        Debug.DrawRay(transform.position, -transform.right * 5f, Color.red, 1f);

        if (Physics.Raycast(transform.position, -transform.right, out hit, 5f))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                canMoveArr[3] = false;

            }
        }
        else
        {
            canMoveArr[3] = true;
        }
    }
    public GameObject ShowArrow(int dir)
    {
        if(dir==0)
        {
            playerDir = dir;
           
            ar1.transform.position =
                new Vector3(transform.position.x+transform.forward.x*5, transform.position.y+transform.forward.y*5, transform.position.z+transform.forward.z*5);
            ar2.transform.position = new Vector3(1000, 1000, 1000);
            ar3.transform.position = new Vector3(1000, 1000, 1000);
            ar4.transform.position = new Vector3(1000, 1000, 1000);
            return ar1;
        }
        else if(dir ==1)
        {
            playerDir = dir;


            ar2.transform.position =
                new Vector3(transform.position.x + transform.right.x * 5, transform.position.y + transform.right.y * 5, transform.position.z + transform.right.z * 5);
            ar1.transform.position = new Vector3(1000, 1000, 1000);
            ar3.transform.position = new Vector3(1000, 1000, 1000);
            ar4.transform.position = new Vector3(1000, 1000, 1000);
            return ar2;
        }
        else if(dir==2)
        {
  
            playerDir = dir;


            ar3.transform.position =
                new Vector3(transform.position.x - transform.forward.x * 5, transform.position.y - transform.forward.y * 5, transform.position.z - transform.forward.z * 5);
            ar2.transform.position = new Vector3(1000, 1000, 1000);
            ar1.transform.position = new Vector3(1000, 1000, 1000);
            ar4.transform.position = new Vector3(1000, 1000, 1000);
            return ar3;
        }
        else
        {
            playerDir = dir;


            ar4.transform.position =
                new Vector3(transform.position.x - transform.right.x * 5, transform.position.y - transform.right.y * 5, transform.position.z - transform.right.z * 5);
            ar2.transform.position = new Vector3(1000, 1000, 1000);
            ar3.transform.position = new Vector3(1000, 1000, 1000);
            ar1.transform.position = new Vector3(1000, 1000, 1000);
            return ar4;
        }
    }
    public IEnumerator ShrinkObject(GameObject ShrinkObject)
    {
        while (totalTick > nowTick)
        {
            ShrinkObject.gameObject.transform.localScale =
                new Vector3(ShrinkObject.gameObject.transform.localScale.x-0.05f, ShrinkObject.gameObject.transform.localScale.y-0.05f, ShrinkObject.gameObject.transform.localScale.z-0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void InitialArrowScale()
    {
        ar1.transform.localScale = new Vector3(5, 5, 5);
        ar2.transform.localScale = new Vector3(5, 5, 5);
        ar3.transform.localScale = new Vector3(5, 5, 5);
        ar4.transform.localScale = new Vector3(5, 5, 5);
    }
    public IEnumerator ShowCanMove()
    {
        FindCanMove();
        while(count>0)
        {
            reStartCoroutine = false;
            if (IsMove==false)
            {
                for (int i = 0; i < 4; i++)
                {
                    FindCanMove();
                    if (canMoveArr[i])
                    {
                        
                        StartCoroutine(ShrinkObject(ShowArrow(i)));
                        while(totalTick>nowTick)
                        {
                            if(reStartCoroutine)
                            {
                                nowTick = 500;
                                i = 50;
                                InitialArrowScale();
                            }
                            yield return new WaitForSeconds(0.01f);
                            nowTick++;
                        }
                        nowTick = 0;

                    }
                    InitialArrowScale();
                }
            }

            count--;
            
        }
        if(count<=0)
        {
            SceneManager.LoadScene("SampleScene");

        }

    }
    public void SetMoveVector()
    {
        if(playerDir==0)
        {
            MoveVector = new Vector3(transform.forward.x / 40, transform.forward.y / 40, transform.forward.z / 40);
        }
        else if(playerDir==1)
        {
            MoveVector = new Vector3(transform.right.x / 40, transform.right.y / 40, transform.right.z / 40);
        }
        else if (playerDir == 2)
        {
            MoveVector = new Vector3(-transform.forward.x / 40, -transform.forward.y / 40, -transform.forward.z / 40);

        }
        else if (playerDir == 3)
        {
            MoveVector = new Vector3(-transform.right.x / 40, -transform.right.y / 40, -transform.right.z / 40);
        }
    }
    public IEnumerator termCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        IsMove = false;
    }
    public void MovePlayer()
    {

        SetMoveVector();

        transform.Translate(MoveVector.x * Speed * 20, MoveVector.y * Speed * 20, MoveVector.z * Speed * 20);
        reStartCoroutine = true;
        IsMove = false;
        FindCanMove();
        count = 3;


    }
    //public IEnumerator MoveCoroutine()
    //{
    //    SetMoveVector();
    //    count = 3;
    //    while (TotalMoveTick > currentMoveTick)
    //    {

    //        transform.Translate(MoveVector.x * Speed, MoveVector.y * Speed, MoveVector.z * Speed);
    //        currentMoveTick++;
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    currentMoveTick = 0;

    //    IsMove = false;
    //}
    
    public void FindBomb()
    {
        if(QuestBomb!=null)
        {
            Debug.Log("Allonsy");
            if (Mathf.Abs(QuestBomb.transform.position.x - transform.position.x) <= 2 && Mathf.Abs(QuestBomb.transform.position.z+1.6f-transform.position.z)<=2)
            {
                Debug.Log("Brilliant");
                Destroy(QuestBomb);
                UIObject.GetComponent<UIManager>().timerLeftTimer = 10;  //타이머 재시작
                UIObject.GetComponent<UIManager>().AddPoint(1);     //1점 추가
                QuestObject.GetComponent<QuestManager>().ShowQuestBox();
            }


        }

    }

}
