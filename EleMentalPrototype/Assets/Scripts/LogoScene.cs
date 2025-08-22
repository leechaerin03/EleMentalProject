using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    IEnumerator Start()
    {
        Context.OnLoadContextData();

        yield return new WaitForSeconds(3f);

        FadeManager.Instance.SceneChange(Context.LOBBY_SCENE);
    }
}
