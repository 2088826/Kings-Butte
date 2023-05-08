using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    [SerializeField] private InputActionAsset inputAction;

    private TypeWriter typeWriter;
    private InputActionMap uiActionMap;

    private void Start()
    {
        typeWriter = GetComponent<TypeWriter>();
        CloseDialogueBox();
        ShowDialogue(testDialogue);

        if (inputAction != null)
        {
            uiActionMap = inputAction.FindActionMap("UI");
        }
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return typeWriter.Run(dialogue, textLabel);
            yield return new WaitUntil(() => uiActionMap["Submit"].triggered);
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }
}
