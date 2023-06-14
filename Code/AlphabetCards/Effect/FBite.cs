using Assets.AlphabetCards.Code.AlphabetCards.Util;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FBite : MonoBehaviour
{
    public Player enemyPlayer = null;

    public void Attack()
    {
        if (enemyPlayer == null)
            return;

        if (enemyPlayer.data.view.IsMine)
            enemyPlayer.data.healthHandler.CallTakeDamage((enemyPlayer.data.maxHealth / 3 * Vector2.one) - Vector2.one, enemyPlayer.transform.position);
        AudioController.Play(AssetManager.Sound_Bite, enemyPlayer.transform, 1.15f);
    }
}