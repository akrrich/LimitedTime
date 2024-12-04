using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    // Constructor de SkillNode("nombre", estaDesbloqueada, precio, indice, boton, bordeDelBoton) 

    private SkillNode root;
    private PlayerController playerController; 
    private AudioSource buttonClick;

    [SerializeField] private Button[] buttons;
    [SerializeField] private RawImage[] borderButtons;

    private string[] skillNames = { "Player", "Speed", "AxeSpeed", "JumpForce", "Life", "ReloadingTime", "ExtraBullets" };
    
    private bool isUnlocked = true;


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buttonClick = GetComponent<AudioSource>();

        root = new SkillNode(skillNames[0], isUnlocked, 0, 0, buttons[0], borderButtons[0]);
        isUnlocked = false;

        AddSkillNodesRecursively(root, 0, 0);
    }


    public void ClickButton(int index)
    {
        SkillNode node = FindNodeByIndex(root, index);

        if (PlayerController.Score >= node.Price)
        {
            buttonClick.Play();
            node.CheckIfCanUnlockSkill();

            if (playerController.PlayerControllerSkills.SkillsMethods.ContainsKey(index))
            {
                playerController.PlayerControllerSkills.SkillsMethods[index].Invoke();
            }

            PlayerController.SubScore(node.Price);
        }
    }


    private SkillNode FindNodeByIndex(SkillNode currentNode, int index)
    {
        if (currentNode == null)
        {
            return null;
        }

        if (currentNode.Index == index)
        {
            return currentNode;
        }

        SkillNode leftSearch = FindNodeByIndex(currentNode.Left, index);
        SkillNode rightSearch = FindNodeByIndex(currentNode.Right, index);

        if (leftSearch != null)
        {
            return leftSearch;
        }

        else
        {
            return rightSearch;
        }
    }

    private void AddSkillNodesRecursively(SkillNode parent, int parentIndex, int depth)
    {
        int leftIndex = parentIndex * 2 + 1; 
        int rightIndex = parentIndex * 2 + 2; 

        int priceLeft = CalculatePrice(depth + 1);
        int priceRight = CalculatePrice(depth + 1);

        if (leftIndex < skillNames.Length)
        {
            SkillNode leftNode = new SkillNode(skillNames[leftIndex], isUnlocked, priceLeft, leftIndex, buttons[leftIndex], borderButtons[leftIndex]);
            parent.AddChild(leftNode);

            AddSkillNodesRecursively(leftNode, leftIndex, depth + 1);
        }

        if (rightIndex < skillNames.Length)  
        {
            SkillNode rightNode = new SkillNode(skillNames[rightIndex], isUnlocked, priceRight, rightIndex, buttons[rightIndex], borderButtons[rightIndex]);
            parent.AddChild(rightNode);

            AddSkillNodesRecursively(rightNode, rightIndex, depth + 1);
        }
    }

    private int CalculatePrice(int depth)
    {
        return depth * 250;
    }
}
