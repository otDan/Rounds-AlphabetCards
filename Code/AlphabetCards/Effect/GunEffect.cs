using ModdingUtils.MonoBehaviours;
using System;
using System.Collections;
using UnboundLib;
using UnboundLib.GameModes;
using UnityEngine;

public class GunEffect : MonoBehaviour
{
    private Player player;
    private GameObject gunSprite;
    private SpriteRenderer renderer;

    private void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        var art = player.data.weaponHandler.gun.gameObject.transform.GetChild(1);
        var gun = AssetManager.SpriteGun;
        gunSprite = Instantiate(gun, art);
        renderer = gunSprite.GetComponent<SpriteRenderer>();
        player.data.stats.GetAlphabetData().sizeMod = AlphabetConstants.ModType.sizeNormalize;
    }

    public void Update()
    {
        if (player == null) return;
        if (gunSprite == null) return;
        if (renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;
        renderer.flipY = !flipped;
    }
}
