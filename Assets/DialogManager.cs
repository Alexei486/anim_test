using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogCanvas;
    public TMP_Text dialogText;
    private string[] dialogLines;
    private int currentLineIndex;

    void Start()
    {
        dialogCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (dialogCanvas.activeSelf)
            {
                ShowNextLine();
            }
            else
            {
                StartDialog(new string[] { "Meow?", "Meowwww?!", "MEOW MEOW MEOW" });
            }
        }
    }

    public void StartDialog(string[] lines)
    {
        dialogLines = lines;
        currentLineIndex = 0;
        dialogCanvas.SetActive(true);
        ShowNextLine();
    }

    public void ShowNextLine()
    {
        if (currentLineIndex < dialogLines.Length)
        {
            dialogText.text = dialogLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        dialogCanvas.SetActive(false);
    }
}
