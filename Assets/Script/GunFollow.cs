using UnityEngine;

public class GunFollow : MonoBehaviour
{//枪跟随鼠标的脚本
    public RectTransform UGUICanvas;
    public Camera mainCamera;
        
    void Update()
    {
        Vector3 mousePosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICanvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera,out mousePosition);
        float z;
        if (mousePosition.x>transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePosition - transform.position);
        }
        else
        {
            z = Vector3.Angle(mousePosition-transform.position,Vector3.up);
        }
        transform.localRotation = Quaternion.Euler(0, 0, z);
    }
}
