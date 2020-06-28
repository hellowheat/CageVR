using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour
{
    public GameObject DialoguePrefab;
    DialogDataReader data;
    GameObject myDialogue;
    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<DialogDataReader>();
        myDialogue = Instantiate(DialoguePrefab, transform);
       // myDialogue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
