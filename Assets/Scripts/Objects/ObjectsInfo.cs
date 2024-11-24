using UnityEngine;

public class ObjectsInfo : MonoBehaviour
{
    private GameObject textInfo;

    void Start()
    {
        textInfo = transform.Find("InfoText").gameObject;

        Objects.OnInfoTextShow += ShowInfoText;
        Objects.OnInfoTextHide += HideInfoText;

        GameManager.Instance.GameStatePlaying += UpdateObjectsInfo;
    }

    void UpdateObjectsInfo()
    {
        if (!PauseManager.Instance.IsGamePaused && !TimeManager.Instance.TimeExpired && Input.GetKey(KeyCode.E))
        {
            HideInfoText();
        }
    }

    void OnDestroy()
    {
        Objects.OnInfoTextShow -= ShowInfoText;
        Objects.OnInfoTextHide -= HideInfoText;

        GameManager.Instance.GameStatePlaying -= UpdateObjectsInfo;
    }

    private void ShowInfoText()
    {
        textInfo.SetActive(true);
    }

    private void HideInfoText()
    {
        textInfo.SetActive(false);
    }
}
