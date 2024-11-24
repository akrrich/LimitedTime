using System.Collections;
using UnityEngine;
using TMPro;

public class BulletsUI : MonoBehaviour
{
    private TMP_Text textAmountBullets;
    private BulletPool bulletPoool;

    [SerializeField] private TMP_Text textReloading;

    private string originalText;

    private bool startCorutineReloading = false;


    void Start()
    {
        textAmountBullets = GetComponentInChildren<TMP_Text>();
        bulletPoool = FindObjectOfType<BulletPool>();

        textReloading.enabled = false;

        originalText = textReloading.text;

        PlayerController.OnReloadingText += ShowReloadingText;
        PlayerController.OnReloadingFinished += HideReloadingText;
    }

    void Update()
    {
        string counterText = bulletPoool.CounterBullets.ToString();
        string totalText = bulletPoool.TotalBullets.ToString();

        textAmountBullets.text = $"{counterText.PadLeft(3)}    {totalText.PadLeft(3)}";
    }

    void OnDestroy()
    {
        PlayerController.OnReloadingText -= ShowReloadingText;
        PlayerController.OnReloadingFinished -= HideReloadingText;
    }


    private void ShowReloadingText()
    {
        textReloading.enabled = true;

        if (!startCorutineReloading)
        {
            StartCoroutine(AddDots());
        }
    }

    private void HideReloadingText()
    {
        textReloading.enabled = false;
        textReloading.text = originalText;
    }

    private IEnumerator AddDots()
    {
        startCorutineReloading = true;

        string baseText = textReloading.text;
        int dotCount = 0;

        while (dotCount < 3)
        {
            dotCount++;

            textReloading.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f);
        }

        while (textReloading.enabled)
        {
            textReloading.text = baseText + new string('.', 3);
            yield return null;
        }

        startCorutineReloading = false;
    }
}
