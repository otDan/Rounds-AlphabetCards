using ModdingUtils.MonoBehaviours;
using System;
using UnityEngine;

public class GunEffect : MonoBehaviour
{
    private const float procTime = 0.1f;

    internal CharacterStatModifiers stats;

    internal float prevSizeModifier;
    internal float prevMaxHealth;

    internal float prevTotalScale;
    internal float totalScaleTarget;
    internal float targetScale;

    internal float timer = 0.0f;
    internal bool effectEnabled;
    internal bool effectApplied;
    internal bool wasDeactivated = false;

    private Player player;
    private GameObject gunSprite;
    private SpriteRenderer renderer;

    void Start()
    {
        player = base.transform.root.GetComponent<Player>();
        var art = player.data.weaponHandler.gun.gameObject.transform.GetChild(1);
        var gun = AssetManager.SpriteGun;
        gunSprite = Instantiate(gun, art);
        renderer = gunSprite.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        //timer += TimeHandler.deltaTime;

        //// Respawn case
        //if (wasDeactivated)
        //{
        //    Refresh();

        //    wasDeactivated = false;
        //}

        //if (timer > procTime)
        //{
        //    if (effectEnabled)
        //    {
        //        float currentTotalScale = Mathf.Pow(player.data.maxHealth / 100f * 1.2f, 0.2f) * stats.sizeMultiplier;
        //        if (!ApproxEqual(currentTotalScale, prevTotalScale))
        //        {
        //            Refresh();
        //        }
        //    }

        //    if (effectEnabled && !effectApplied)
        //    {
        //        if (prevTotalScale >= 1.2f)
        //        {
        //            targetScale = 1.65f - (0.5f * Mathf.Pow(0.9f, this.prevTotalScale));
        //        }
        //        else
        //        {
        //            targetScale = 0.1f + Mathf.Pow(1.1f, this.prevTotalScale);
        //        }

        //        totalScaleTarget = prevSizeModifier * targetScale / prevTotalScale;

        //        this.player.gameObject.AddComponent<SizeNormalizerStatus>().SetSize(targetScale / prevTotalScale);
        //        effectApplied = true;

        //        // UnityEngine.Debug.Log($"[SizeNorm] adjusting... [{player.playerID}] [{totalScaleBefore}] >> [{targetScale}] == [{totalScaleAfter}] >> [{stats.sizeMultiplier}]");
        //    }

        //    if (effectEnabled && effectApplied)
        //    {
        //        // this.player.gameObject.GetOrAddComponent<SizeNormalizerStatus>().SetSize(targetScale / totalScaleBefore);
        //    }

        //    if (!effectEnabled && !effectApplied)
        //    {
        //        this.player.gameObject.AddComponent<SizeNormalizerStatus>().SetSize(1.0f);
        //        effectApplied = true;
        //    }
        //    timer -= procTime;

        //}   

        if (player == null) return;
        if (gunSprite == null) return;
        if (renderer == null) return;

        var flipped = player.data.aimDirection.x >= 0;
        renderer.flipY = !flipped;
    }

    //public void Refresh()
    //{
    //    if (this.stats.GetAlphabetData().sizeMod == AlphabetConstants.ModType.sizeNormalize)
    //    {
    //        effectEnabled = true;
    //    }
    //    effectApplied = false;

    //    prevMaxHealth = player.data.maxHealth;
    //    prevSizeModifier = this.stats.sizeMultiplier;
    //    prevTotalScale = Mathf.Pow(prevMaxHealth / 100f * 1.2f, 0.2f) * prevSizeModifier;
    //}

    //public static bool ApproxEqual(float numA, float numB, float precision = 10e-3f)
    //{
    //    float diff = numA - numB;
    //    return Mathf.Abs(diff) <= precision;
    //}

    //class SizeNormalizerStatus : ReversibleEffect
    //{
    //    public override void OnAwake()
    //    {
    //        this.SetLivesToEffect(999);
    //    }

    //    public void SetSize(float size)
    //    {
    //        characterStatModifiersModifier.sizeMultiplier_mult = size;

    //        try
    //        {
    //            ApplyModifiers();
    //        }
    //        catch (Exception exception)
    //        {
    //        }
    //    }
    //}

}
