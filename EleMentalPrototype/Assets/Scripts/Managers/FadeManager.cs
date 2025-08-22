using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    [SerializeField] Animator fadeAnimator_;

    public void SceneChange(string sceneName)
    {
        fadeAnimator_.gameObject.SetActive(true);
        fadeAnimator_.SetTrigger("FadeIn");

        StartCoroutine(SceneChangeCoroutine(sceneName));
    }

    IEnumerator SceneChangeCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(0.75f);

        SceneManager.LoadScene(sceneName);
    }

    public void FadeOut()
    {
        fadeAnimator_.SetTrigger("FadeOut");

        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(0.75f);

        fadeAnimator_.gameObject.SetActive(false);

    }
}
