using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class Stage1Manager : MonoBehaviour
{
    public GameObject wallPre;
    public GameObject roadPre;
    public GameObject Player;
    public Vector2 playerSpawnLocation;
    public Vector2 offset;
    public GameObject mainCamera;
    public Vector3 mainCameraPosition;
    public Vector3 mainCameraRotation;

    //퀘스트 관련
    public GameObject QuestObject;

    //ui 관련
    public GameObject UIObject;

    void Start()
    {
        int[,] mapArray = { { 0, 0,0,0,0,0,0,0,0,0},
                            { 0, 0,0,0,0,0,0,0,0,0},
                            { 3, 3,3,3,3,3,3,3,3,3},
                            { 3, 1,1,1,1,1,1,1,1,3},
                            { 3, 1,3,1,3,3,3,3,1,3},
                            { 3, 1,1,1,1,1,1,1,1,3},
                            { 3, 1,3,1,3,3,1,3,3,3},
                            { 3, 1,3,1,3,3,1,3,0,0},
                            { 3, 1,1,1,1,1,1,3,0,0},
                            { 3, 3,3,3,3,3,3,3,0,0}};       //1은 길 2는 시작 구역
        int x_idx = 10;
        int y_idx = 10;

        MakeMap(mapArray, x_idx, y_idx);        //동적으로 맵을 만든다.
        SpawnPlayer();
        SetMainCamera();
        Player.GetComponent<PlayerManager>().FindCanMove();
        StartCoroutine(Player.GetComponent<PlayerManager>().ShowCanMove());
        //퀘스트 관련 초기화
        QuestObject.GetComponent<QuestManager>().StartSceneQuest(); //해당 레벨의 퀘스트 시작

        //ui 관련 초기화
        UIObject.GetComponent<UIManager>().nowPoint = 0;
        UIObject.GetComponent<UIManager>().pointText.text = "POINT = 0";
        UIObject.GetComponent<UIManager>().timerText.text = "LEFT TIMER = 10";
        UIObject.GetComponent<UIManager>().timerLeftTimer = 10;
        StartCoroutine(UIObject.GetComponent<UIManager>().StartTimer());
    }
    void SetMainCamera()
    {
        mainCamera.transform.position = new Vector3(mainCameraPosition.x, mainCameraPosition.y, mainCameraPosition.z);
        mainCamera.transform.rotation = Quaternion.Euler(mainCameraRotation.x, mainCameraRotation.y, mainCameraRotation.z);
    }
    void SpawnPlayer()
    {

        Player.transform.position = new Vector3(playerSpawnLocation.x * 10 + offset.x, 1f, playerSpawnLocation.y * 10 + offset.y);
    }
    void MakeMap(int[,] mapArray, int x_idx, int y_idx)
    {
        for (int i = 0; i < y_idx; i++)
        {
            for (int j = 0; j < x_idx; j++)
            {
                if(mapArray[i,j]==3)
                {
                    Instantiate(wallPre, new Vector3(i * 10, 0.1f, j * 10), Quaternion.identity);
                }
                else if(mapArray[i, j] == 1)
                {
                    Instantiate(roadPre, new Vector3(i * 10, 0.1f, j * 10), Quaternion.identity);
                }


            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerManager>().IsMove==false)
        {
            //플레이어 움직이고 있음
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //스페이스바 히트
                //플레이어 안 움직이고 있음
                Player.GetComponent<PlayerManager>().IsMove = true;
                Player.GetComponent<PlayerManager>().MovePlayer();
                //Player.GetComponent<PlayerManager>().FindCanMove();

                //StartCoroutine(Player.GetComponent<PlayerManager>().MoveCoroutine());

            }
            else if(Input.GetKeyDown(KeyCode.Return))
            {
                //엔터키 히트
                Player.GetComponent<PlayerManager>().FindBomb();

            }
        }
    
    }
}
