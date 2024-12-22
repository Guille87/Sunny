using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float parallax;
    Material mat;
    Transform cam;
    Vector3 initialPos;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        cam = Camera.main.transform;
        initialPos = transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(cam.position.x, initialPos.y, initialPos.z);
        mat.mainTextureOffset = new Vector2(cam.position.x * parallax, 0);
    }
}
