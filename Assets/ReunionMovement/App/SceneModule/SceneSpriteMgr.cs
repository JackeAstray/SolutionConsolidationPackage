using GameLogic.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class SceneSpriteMgr : SingleToneMgr<SceneSpriteMgr>
    {
        public List<Sprite> sprites= new List<Sprite>();

        public void MyStartCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    }
}