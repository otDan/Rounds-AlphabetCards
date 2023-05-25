using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabEffect : MonoBehaviour
{
    private Player player;
    private GameObject cabSprite;
    private SpriteRenderer renderer;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        var art = player.transform.GetChild(0);
        var cab = AssetManager.Sprite_Cab;
        cabSprite = Instantiate(cab, art);
        renderer = cabSprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player == null) return;
        if (cabSprite == null) return;
        if (renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;

        if (flipped)
        {
            cabSprite.transform.localRotation = new Quaternion(0, 0, 0, 0);
            cabSprite.transform.localPosition = new Vector3(0.65f, 0, 0);
        }
        else
        {
            cabSprite.transform.localRotation = new Quaternion(0, 180f, 0, 0);
            cabSprite.transform.localPosition = new Vector3(-0.65f, 0, 0);
        }
    }
}
