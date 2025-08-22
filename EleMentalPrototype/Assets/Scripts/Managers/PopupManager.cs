using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupName
{
    // 팝업 추가 시 추가해야 함
    public static string CommonPopup = "CommonPopup";

    public static string Game_PausePopup = "Game_PausePopup";
    public static string Game_StageCompletePopup = "Game_StageCompletePopup";
    public static string Game_StageLosePopup = "Game_StageLosePopup";

    public static string Lobby_OptionPopup = "Lobby_OptionPopup";
    public static string Lobby_StageOpenPopup = "Lobby_StageOpenPopup";
    public static string Lobby_InventoryPopup = "Lobby_InventoryPopup";
    public static string Lobby_LockPopup = "Lobby_LockPopup";
    public static string Lobby_CoinPopup = "Lobby_CoinPopup";
    public static string Lobby_SelectElementPopup = "Lobby_SelectElementPopup";

}

public class PopupManager : MonoBehaviour
{
    
    public static PopupManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] RectTransform popupParent_;
    [SerializeField] GameObject popupBackground_;
    

    List<Popup> alivePopupList_ = new List<Popup>();

    bool isLockedCursor_ = false;
    public bool IsLockedCursor
    {
        set { isLockedCursor_ = value; }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if(alivePopupList_.Count > 0)
        //    {
        //        Popup popup = alivePopupList_[alivePopupList_.Count - 1];
        //        popup.OnBackBtnClick();
        //    }
        //}
    }

    public Popup CreatePopupAndShow(string popupName)
    {
        GameObject popupPrefab = Resources.Load(popupName) as GameObject;

        if (popupPrefab == null)
        {
            Debug.LogError("Popup Prefab is not exist");
            return null;
        }

        Popup popup = Instantiate(popupPrefab, popupParent_).GetComponent<Popup>();
        if (popup == null)
        {
            Debug.LogError("Popup Prefab has not popup script component");
            return null;
        }

        alivePopupList_.Add(popup);

        popup.Init();

        PopupBackgroundCheck();

        return popup;
    }

    public void RemoveAtPopupList(Popup popup)
    {
        alivePopupList_.Remove(popup);
        PopupBackgroundCheck();
    }

    void PopupBackgroundCheck()
    {
        bool isPopupExist = alivePopupList_.Count > 0;
        popupBackground_.SetActive(isPopupExist);

        if (Hub_Ingame.Instance != null) // 인게임이라면
        {
            Hub_Ingame.Instance.cameraController_.IsPopupAlive = isPopupExist;
        }

        if (isLockedCursor_)
        {
            if (isPopupExist)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public bool IsAlivePopupExist
    {
        get { return alivePopupList_.Count > 0; }
    }
}
