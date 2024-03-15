using UnityEngine;
using System.Collections;

public class FishGenerator : MonoBehaviour
{
    public Transform FishHolder;
    public Transform[] GenPositions;
    public GameObject[] FishPrefebs;

    public float fishGenerateTime = 0.1f;
    public float waveGenerateTime = 0.3f;

    void Start()
    {
        InvokeRepeating("GenerateFishes", 0, waveGenerateTime);
    }

    void GenerateFishes()
    {
        int GenPositionsIndex = Random.Range(0, GenPositions.Length);
        int FishPrefebsIndex = Random.Range(0, FishPrefebs.Length);
        int MaxNum = FishPrefebs[FishPrefebsIndex].GetComponent<FishAttr>().MaxNum;
        int MaxSpeed = FishPrefebs[FishPrefebsIndex].GetComponent<FishAttr>().MaxSpeed;
        int Num = Random.Range(MaxNum / 2 + 1, MaxNum);
        int Speed = Random.Range(MaxSpeed / 2 , MaxSpeed);
        int MoveType = Random.Range(0, 2);//0直走，1转弯；
        int AngOffset;//直行时角度偏转；
        int AngSpeed;//弯绕时角速度；
        float localfishGenerateTime = fishGenerateTime * (10 - Speed);
        
        if (MoveType==0)
        {
            AngOffset = Random.Range(-22, 22);
            StartCoroutine( GenerateStraightFish(GenPositionsIndex,FishPrefebsIndex,Num,Speed,AngOffset));
            

        }
        else
        {
            if (Random.Range(0, 2) == 0)
            {
                AngSpeed = Random.Range(-9, 15);
            }
            else
            {
                AngSpeed = Random.Range(9, 15);
            }
            StartCoroutine(GenerateTurnFish(GenPositionsIndex, FishPrefebsIndex, Num, Speed, AngSpeed));

        }

        IEnumerator GenerateStraightFish(int genPositionIndex,int fishPrefebIndex,int num,int speed,int angOffset)
        {
            for (int i = 0; i <num; i++)
            {
                GameObject Fish = Instantiate(FishPrefebs[fishPrefebIndex]);
                Fish.transform.SetParent(FishHolder,true);
                Fish.transform.localPosition = GenPositions[genPositionIndex].localPosition;
                Fish.transform.localRotation = GenPositions[genPositionIndex].localRotation;
                Fish.transform.Rotate(0, 0, angOffset);
                Fish.GetComponent<SpriteRenderer>().sortingOrder += i;
                Fish.AddComponent<Ef_AutoMove>().moveSpeed=speed;
                
                yield return new WaitForSeconds(localfishGenerateTime);
            }
        }

        IEnumerator GenerateTurnFish(int genPositionIndex, int fishPrefebIndex, int num, int speed, int angSpeed)
        {
            for (int i = 0; i < num; i++)
            {
                GameObject Fish = Instantiate(FishPrefebs[fishPrefebIndex]);
                Fish.transform.SetParent(FishHolder, true);
                Fish.transform.localPosition = GenPositions[genPositionIndex].localPosition;
                Fish.transform.localRotation = GenPositions[genPositionIndex].localRotation;
                
                Fish.GetComponent<SpriteRenderer>().sortingOrder += i;
                Fish.AddComponent<Ef_AutoTurn>().speed = angSpeed;
                Fish.AddComponent<Ef_AutoMove>().moveSpeed = speed;

                yield return new WaitForSeconds(localfishGenerateTime);
            }
        }


    }
    
}
