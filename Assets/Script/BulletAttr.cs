using UnityEngine;

public class BulletAttr : MonoBehaviour
{
    public int speed;
    public double disapperTimeFactor;
    public int damage;
    public GameObject web;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Fish")
        {
            GameObject newweb =  Instantiate(web);
            newweb.transform.SetParent(transform.parent,false);
            newweb.transform.position = transform.position;
            newweb.GetComponent<WebAttr>().damage = damage;
            newweb.GetComponent<WebAttr>().disapper_time_factor = disapperTimeFactor;
            Destroy(gameObject);
        }
    }
}
