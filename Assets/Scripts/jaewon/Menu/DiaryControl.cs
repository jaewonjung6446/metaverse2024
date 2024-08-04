using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DiaryControl : MonoBehaviour
{
    string filePath; // ���� ���
    private string[] lines;
    private int currentIndex = 0;

    public Text displayText; // UI �ؽ�Ʈ ������Ʈ

    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "savedDiaries.txt");
        if (File.Exists(filePath))
        {
            Debug.Log("���� �߰�");
            lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                displayText.text = lines[currentIndex];
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }
    }

    public void NextLine()
    {
        if (lines != null && lines.Length > 0)
        {
            currentIndex++;
            if (currentIndex >= lines.Length)
            {
                currentIndex = 0; // ������ ���� ������ ó������ ���ư��ϴ�.
            }
            displayText.text = lines[currentIndex];
        }
    }

    public void PreviousLine()
    {
        if (lines != null && lines.Length > 0)
        {
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = lines.Length - 1; // ù ���� ������ ���������� ���ư��ϴ�.
            }
            displayText.text = lines[currentIndex];
        }
    }
}
