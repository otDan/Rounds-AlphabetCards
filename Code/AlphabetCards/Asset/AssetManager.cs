using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class AssetManager
{
    public static AssetBundle assets;

    private static GameObject _Cards;
    public static GameObject Cards
    {
        get
        {
            if (_Cards == null)
                _Cards = assets.LoadAsset<GameObject>("_Cards");
            return _Cards;
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
                _Sound_Teleport = assets.LoadAsset<AudioClip>("teleport");
            return _Sound_Teleport;
        }
    }
}
