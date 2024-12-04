using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    [SerializeField] private GameObject infoText; 

    private RectTransform rectTransform;


    void Start()
    {
        infoText.gameObject.SetActive(false); 
    }


    public void OnPointerEnter()
    {
        infoText.gameObject.SetActive(true); 
    }


    public void OnPointerExit()
    {
        infoText.gameObject.SetActive(false); 
    }
}
