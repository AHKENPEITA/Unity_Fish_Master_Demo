using UnityEngine;

public class FishAttr : MonoBehaviour
{
    public int HP;
    public int EXP;
    public int GOLD;
    public int MaxNum;
    public int MaxSpeed;
    public GameObject dieprefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Broader")
        {
            Destroy(gameObject);
        }
    }


    void takeDamage(int damage_value)
    {
        HP -= damage_value;
        if (HP <= 0)
        {
            Controller.Instance.gold += GOLD;
            Controller.Instance.exp += EXP;
            GameObject die = Instantiate(dieprefab);
            die.transform.SetParent(gameObject.transform.parent,false);
            die.transform.position = transform.position;
            die.transform.rotation = transform.rotation;
            Destroy(gameObject);
        }
    }

}
