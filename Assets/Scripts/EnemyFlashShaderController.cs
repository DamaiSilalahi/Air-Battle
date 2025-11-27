using UnityEngine;

public class EnemyFlashShaderController : MonoBehaviour
{
    public Material flashMaterial;
    public Renderer rend;

    void Start()
    {
        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        if (flashMaterial != null)
            rend.material = flashMaterial;   // pasang material biru
    }
}
