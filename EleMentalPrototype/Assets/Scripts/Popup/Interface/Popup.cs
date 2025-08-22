using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] Animator popupAnimator_;

    public virtual void Init()
    {
        if(popupAnimator_ != null)
        {
            popupAnimator_.SetTrigger("Show");
        }
    }

    public virtual void OnBackBtnClick()
    {
        PopupManager.Instance.RemoveAtPopupList(this);

        if (popupAnimator_ != null)
        {
            popupAnimator_.SetTrigger("Hide");
        }

        StartCoroutine(OnDestroyThisPopup());
    }

    IEnumerator OnDestroyThisPopup()
    {
        yield return new WaitForSeconds(0.5f); // 애니메이션 시간과 동일하게 세팅 필요

        Destroy(gameObject);
    }
}
