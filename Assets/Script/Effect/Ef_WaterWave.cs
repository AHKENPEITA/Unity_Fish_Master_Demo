using UnityEngine;

public class Ef_WaterWave : MonoBehaviour
{
    public Texture[] texture;
    private Material material;
    private int index = 0;


    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTexture", 0, 0.1f);
    }


    void ChangeTexture()
    {
        material.mainTexture = texture[index];
        index = (index + 1) % texture.Length;
    }




}
