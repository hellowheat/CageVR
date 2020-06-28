using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public string NPCid;
    DialogDataReader data;
    GameObject dialogueGb;
    TextMesh contentMesh;
    // Start is called before the first frame update
    void Start()
    {
        data = new DialogDataReader(NPCid);
        try{
            dialogueGb = transform.Find("dialogue").gameObject;
            contentMesh = dialogueGb.transform.Find("content").GetComponent<TextMesh>();
        }
        catch { dialogueGb = null; }
        StartDialog();
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (!data.isDialogueEnd)
            {
                data.NextDialogue(-1);
                updateUIContent();
            }
            else CloseDialog();
        }
    }

    public void StartDialog()
    {
        data.StartDialogue();
        updateUIContent();
        dialogueGb.SetActive(true);
    }

    public void CloseDialog()
    {
        dialogueGb.SetActive(false);
    }

    void updateUIContent()
    {
        if (dialogueGb)
        {
            //一行最多十个字，包括句号
            contentMesh.text = data.nowContent;
        }
    }
}
