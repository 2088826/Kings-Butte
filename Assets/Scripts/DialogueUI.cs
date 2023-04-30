using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;


    private void Start()
    {
        GetComponent<TypeWriter>().Run("Hail, my liege! I doth perceive thou art preparing to lay claim to that which is thy rightful due: The BUTTE...", textLabel);
    }
}
