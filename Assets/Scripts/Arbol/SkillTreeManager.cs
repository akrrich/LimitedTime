using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
    private SkillNode raiz;
    private PlayerController playerController; 
    private AudioSource buttonClick;

    [SerializeField] private Button[] buttons;
    [SerializeField] private RawImage[] borderButtons;


    private string[] skillNames = { "Player", "Speed", "Damage", "JumpForce", "Life" };

    private bool[] isUnlocked = { true, false, false, false, false };

    private int[] price = { 0, 250, 250, 500, 500, 10, 10 };


    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buttonClick = GetComponent<AudioSource>();

        raiz = new SkillNode(skillNames[0], isUnlocked[0], price[0], 0, buttons[0], borderButtons[0]);

        AddSkillNodesRecursively(raiz, 0);
    }


    public void ClickButton(int index)
    {
        SkillNode node = FindNodeByIndex(raiz, index);

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
            return null;

        // Si el índice coincide con el nodo actual, lo devolvemos
        if (currentNode.Index == index)
            return currentNode;

        // Buscamos en los hijos
        SkillNode leftSearch = FindNodeByIndex(currentNode.Left, index);
        if (leftSearch != null)
            return leftSearch;

        return FindNodeByIndex(currentNode.Right, index);
    }


    private void AddSkillNodesRecursively(SkillNode parent, int parentIndex)
    {
        int leftIndex = parentIndex * 2 + 1;  // Índice para el hijo izquierdo
        int rightIndex = parentIndex * 2 + 2; // Índice para el hijo derecho

        // Verificar si el nodo tiene un hijo izquierdo y si aún hay más nodos en skillNames
        if (leftIndex < skillNames.Length)
        {
            // Crear nodo izquierdo
            SkillNode leftNode = new SkillNode(skillNames[leftIndex], isUnlocked[leftIndex], price[leftIndex], leftIndex, buttons[leftIndex], borderButtons[leftIndex]);
            parent.AddChild(leftNode);

            // Llamada recursiva para seguir agregando nodos debajo de este hijo
            AddSkillNodesRecursively(leftNode, leftIndex);
        }

        // Verificar si el nodo tiene un hijo derecho SOLO si es un nodo intermedio (no hoja)
        if (rightIndex < skillNames.Length)  // Se permite solo para nodos que no sean hoja
        {
            // Crear nodo derecho
            SkillNode rightNode = new SkillNode(skillNames[rightIndex], isUnlocked[rightIndex], price[rightIndex], rightIndex, buttons[rightIndex], borderButtons[rightIndex]);
            parent.AddChild(rightNode);

            // Llamada recursiva para seguir agregando nodos debajo de este hijo
            AddSkillNodesRecursively(rightNode, rightIndex);
        }
    }
}
