using UnityEngine;
using UnityEngine.UI;

public class PLayerLifeUI : MonoBehaviour
{
    private Slider sliderLife;
    private PlayerController playerController;

    [SerializeField] private Image fillImage;


    void Start()
    {
        sliderLife = GetComponent<Slider>();    
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        sliderLife.value = Mathf.Clamp(playerController.Life, 0, 5);

        UpdateHealthBarColor();
    }

    private void UpdateHealthBarColor()
    {
        Color[] healthColors = {
        Color.black,                      
        Color.red,                          
        new Color(1.0f, 0.5f, 0.0f),       
        Color.yellow,                       
        new Color32(150, 255, 0, 255),     
        Color.green                          
        };

        for (int i = 0; i < healthColors.Length; i++)
        {
            if (sliderLife.value == i)
            {
                fillImage.color = healthColors[i];
            }
        }
    }
}
