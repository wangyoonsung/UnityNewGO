using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public GameObject questPrefab;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowQuestBox()
    {
        int cycleNum = 50;
        Transform rayTrans = new GameObject().GetComponent<Transform>();
        Transform tempQuestBoxTrans = new GameObject().GetComponent<Transform>();
        RaycastHit hit;
        while (cycleNum>0)
        {
            int x = Random.Range(-4, 5);
            int z = Random.Range(-4, 5);
            //hit를 감지하는 변수
            rayTrans.position = new Vector3(player.transform.position.x + 5 * x, player.transform.position.y + 10f, player.transform.position.z + 5 * z);
            Debug.DrawRay(rayTrans.position, -rayTrans.up * 30, Color.yellow, 10f);
            if (Physics.Raycast(rayTrans.position, -rayTrans.up, out hit, 30f))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Road"))
                {
                Debug.Log("제로니모"+hit.transform.name);

                    //4방향으로 Raycast쏘기
                    tempQuestBoxTrans.position = new Vector3(player.transform.position.x + 5 * x, 1f, player.transform.position.z + 5 * z);
                    Debug.DrawRay(tempQuestBoxTrans.position, tempQuestBoxTrans.forward*5, Color.yellow, 10f);
                    Debug.DrawRay(tempQuestBoxTrans.position, tempQuestBoxTrans.right*5, Color.yellow, 10f);
                    Debug.DrawRay(tempQuestBoxTrans.position, -tempQuestBoxTrans.forward*5, Color.yellow, 10f);
                    Debug.DrawRay(tempQuestBoxTrans.position, -tempQuestBoxTrans.right * 5, Color.yellow, 10f);

                    player.GetComponent<PlayerManager>().QuestBomb =
                         Instantiate(questPrefab, tempQuestBoxTrans.position+new Vector3(0, 0, -1.6f), Quaternion.identity);
                         return;
                    //if (Physics.Raycast(tempQuestBoxTrans.position, tempQuestBoxTrans.forward, out hit, 5f))
                    //{
                    //    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    //    {
                    //        player.GetComponent<PlayerManager>().QuestBomb = 
                    //        Instantiate(questPrefab, tempQuestBoxTrans.position+tempQuestBoxTrans.forward * 5+new Vector3(0, 0, -1.6f), Quaternion.identity);
                    //        return;
                    //    }
                    //}

                    //else if (Physics.Raycast(tempQuestBoxTrans.position, tempQuestBoxTrans.right, out hit, 5f))
                    //{
                    //    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    //    {
                    //        player.GetComponent<PlayerManager>().QuestBomb =
                    //        Instantiate(questPrefab, tempQuestBoxTrans.position + tempQuestBoxTrans.right * 5 + new Vector3(0, 0, -1.6f), Quaternion.identity);
                    //        return;
                    //    }
                    //}
                    //else if (Physics.Raycast(tempQuestBoxTrans.position, -tempQuestBoxTrans.forward, out hit, 5f))
                    //{
                    //    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    //    {
                    //        player.GetComponent<PlayerManager>().QuestBomb =
                    //        Instantiate(questPrefab, tempQuestBoxTrans.position  -tempQuestBoxTrans.forward * 5 + new Vector3(0, 0, -1.6f), Quaternion.identity);
                    //        return;
                    //    }
                    //}
                    //else if (Physics.Raycast(tempQuestBoxTrans.position, -tempQuestBoxTrans.right, out hit, 5f))
                    //{
                    //    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                    //    {
                    //        player.GetComponent<PlayerManager>().QuestBomb =
                    //        Instantiate(questPrefab, tempQuestBoxTrans.position -tempQuestBoxTrans.right * 5 + new Vector3(0, 0, -1.6f), Quaternion.identity);
                    //        return;
                    //    }
                    //}
                }
            }
            cycleNum--;
        }
        
    }
    public void StartSceneQuest()
    {
        ShowQuestBox();
    }
}
