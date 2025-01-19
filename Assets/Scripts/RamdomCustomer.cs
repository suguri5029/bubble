using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomCustomer : MonoBehaviour
{
    [SerializeField] TutorialHints hints;

    public List<string> animalCustomers; // ����˿͵��б�
    public TextMeshProUGUI tmpText; // TextMeshPro ������ʾ�ı�
    public TextMeshProUGUI feedbackText; // ������ʾ������Ϣ���ı�

    private float randomPercentage; // ������ɵı���

    public void Update()
    {
       
    }
    void Start()
    {
        // ��ʼ�������б�������Inspector�б༭���ڴ�����ֱ�Ӹ�ֵ��
        animalCustomers = new List<string> { "����", "����¹", "����" };

        // ���ѡ��˿Ͳ���ʾ���
        DisplayRandomCustomer();
    }

    void DisplayRandomCustomer()
    {
        // ���б������ѡ��һ���˿�
        string randomCustomer = animalCustomers[Random.Range(0, animalCustomers.Count)];

        randomPercentage = Random.Range(0f, 60f);

        // �������ʾ�� tmpText ��
        tmpText.text = $"{randomCustomer}: {randomPercentage:F0}%";

        // ���֮ǰ�ķ����ı�
        feedbackText.text = "";
    }

    public void CheckRate(float rate)
    {
        // ����Ŀ�귶Χ
        float lowerBound = randomPercentage - 5f;
        float upperBound = randomPercentage + 5f;
        print(rate);
        // ��������Ƿ��ڷ�Χ��
        if (rate >= lowerBound && rate <= upperBound)
        {
            
            feedbackText.text = "������!";
        }
        else
        {
            feedbackText.text = "����һ��!";
        }


        Pouring.allowedToPour = false;
        hints.displayNextHint();
    }
}
