using UnityEngine;

public class WebAttr : MonoBehaviour
{
    public int damage;
    public float disappear_time;
    public double disapper_time_factor;


    private void Start()
    {
        Debug.Log(disappear_time + (float)disapper_time_factor);
        Destroy(gameObject,disappear_time+(float)disapper_time_factor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            collision.SendMessage("takeDamage", damage);
        }  
    }
}
