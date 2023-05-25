using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class AssetManager
{
    public static AssetBundle assets;

    private static GameObject _cards;
    public static GameObject Cards
    {
        get
        {
            if (_cards == null)
                _cards = assets.LoadAsset<GameObject>("_Cards");
            return _cards;
        }
    }

    private static GameObject _Sprite_Cab;
    public static GameObject Sprite_Cab
    {
        get
        {
            if (_Sprite_Cab == null)
                _Sprite_Cab = assets.LoadAsset<GameObject>("Sprite_CAB");
            return _Sprite_Cab;
        }
    }

    private static GameObject _Sprite_Gun;
    public static GameObject Sprite_Gun
    {
        get
        {
            if (_Sprite_Gun == null)
                _Sprite_Gun = assets.LoadAsset<GameObject>("Sprite_GUN");
            return _Sprite_Gun;
        }
    }

    private static GameObject _Particle_Teleport;
    public static GameObject Particle_Teleport
    {
        get
        {
            if (_Particle_Teleport == null)
                _Particle_Teleport = assets.LoadAsset<GameObject>("Particle_Teleport");
            return _Particle_Teleport;
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
