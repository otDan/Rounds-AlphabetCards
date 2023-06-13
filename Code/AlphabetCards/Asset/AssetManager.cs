using ModdingUtils.Utils;
using UnityEngine;

public static class AssetManager
{
    public static AssetBundle assets;

    private static GameObject _Cards;
    public static GameObject Cards
    {
        get
        {
            AlphabetCards.Instance.Log("Loading _Cards");
            if (_Cards == null)
                _Cards = assets.LoadAsset<GameObject>("_Cards");
            AlphabetCards.Instance.Log("_Cards: " + _Cards);
            return _Cards;
        }
    }

    private static GameObject _SpriteF;
    public static GameObject SpriteF
    {
        get
        {
            if (_SpriteF == null)
                _SpriteF = assets.LoadAsset<GameObject>("Sprite_F");
            return _SpriteF;
        }
    }

    private static GameObject _SpriteCab;
    public static GameObject SpriteCab
    {
        get
        {
            if (_SpriteCab == null)
                _SpriteCab = assets.LoadAsset<GameObject>("Sprite_CAB");
            return _SpriteCab;
        }
    }

    private static GameObject _SpriteGun;
    public static GameObject SpriteGun
    {
        get
        {
            if (_SpriteGun == null)
                _SpriteGun = assets.LoadAsset<GameObject>("Sprite_GUN");
            return _SpriteGun;
        }
    }

    private static GameObject _ParticleTeleport;
    public static GameObject ParticleTeleport
    {
        get
        {
            if (_ParticleTeleport == null)
                _ParticleTeleport = assets.LoadAsset<GameObject>("Particle_Teleport");
            return _ParticleTeleport;
        }
    }

    private static AudioClip _Sound_Teleport;
    public static AudioClip Sound_Teleport {
        get {
            if (_Sound_Teleport == null)
                _Sound_Teleport = assets.LoadAsset<AudioClip>("Sound_Teleport");
            return _Sound_Teleport;
        }
    }

    private static AudioClip _Sound_Bite;
    public static AudioClip Sound_Bite
    {
        get
        {
            if (_Sound_Bite == null)
                _Sound_Bite = assets.LoadAsset<AudioClip>("Sound_Bite");
            return _Sound_Bite;
        }
    }

    private static AudioClip _Sound_F;
    public static AudioClip Sound_F
    {
        get
        {
            if (_Sound_F == null)
                _Sound_F = assets.LoadAsset<AudioClip>("Sound_F");
            return _Sound_F;
        }
    }

    private static AudioClip _Sound_SSUNDEE;
    public static AudioClip Sound_SSUNDEE
    {
        get
        {
            if (_Sound_SSUNDEE == null)
                _Sound_SSUNDEE = assets.LoadAsset<AudioClip>("Sound_SSUNDEE");
            return _Sound_SSUNDEE;
        }
    }
}
