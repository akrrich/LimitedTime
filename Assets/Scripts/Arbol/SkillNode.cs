using UnityEngine.UI;
using UnityEngine;

public class SkillNode
{
    private SkillNode left;
    private SkillNode right;

    private string name;
    private bool isUnlocked;
    private int price;
    private int index;
    private Button button;
    private RawImage borderButton;

    public SkillNode Left { get => left; }
    public SkillNode Right { get => right; }

    public int Price { get => price; }
    public int Index { get => index; }


    public SkillNode(string name, bool isUnlocked, int price, int index, Button button, RawImage borderButton)
    {
        this.name = name;
        this.isUnlocked = isUnlocked;
        this.price = price;
        this.index = index;
        this.button = button;
        this.borderButton = borderButton;

        button.interactable = isUnlocked;
        button.image.color = Color.black;
    }


    public void AddChild(SkillNode child)
    {
        if (child != null)
        {
            if (left == null)
            {
                left = child;
            }

            else if (right == null)
            {
                right = child;
            }
        }
    }

    public void CheckIfCanUnlockSkill()
    {
        if (!isUnlocked)
        {
            return;
        }

        button.interactable = false;
        isUnlocked = false;
        button.image.color = Color.white;
        borderButton.color = Color.green;

        if (left != null)
        {
            left.isUnlocked = true;
            left.button.interactable = true;
        }

        if (right != null)
        {
            right.isUnlocked = true;
            right.button.interactable = true; 
        }
    }
}
