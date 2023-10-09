using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 0.2f;

    private Material defaultMat;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultMat = sprite.material;
    }

    public IEnumerator FlashRoutine()
    {
        sprite.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);

        sprite.material = defaultMat;
    }
}
