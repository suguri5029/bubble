using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomCustomer : MonoBehaviour
{
    [SerializeField] TutorialHints hints;

    public List<string> animalCustomers; // 动物顾客的列表
    public TextMeshProUGUI tmpText; // TextMeshPro 用于显示文本
    public TextMeshProUGUI feedbackText; // 用于显示反馈信息的文本

    private float randomPercentage; // 随机生成的比例

    public void Update()
    {
       
    }
    void Start()
    {
        // 初始化动物列表（可以在Inspector中编辑或在代码中直接赋值）
        animalCustomers = new List<string> { "鹈鹕", "长颈鹿", "仓鼠" };

        // 随机选择顾客并显示结果
        DisplayRandomCustomer();
    }

    void DisplayRandomCustomer()
    {
        // 从列表中随机选择一个顾客
        string randomCustomer = animalCustomers[Random.Range(0, animalCustomers.Count)];

        randomPercentage = Random.Range(0f, 60f);

        // 将结果显示在 tmpText 上
        tmpText.text = $"{randomCustomer}: {randomPercentage:F0}%";

        // 清除之前的反馈文本
        feedbackText.text = "";
    }

    public void CheckRate(float rate)
    {
        // 计算目标范围
        float lowerBound = randomPercentage - 5f;
        float upperBound = randomPercentage + 5f;
        print(rate);
        // 检查输入是否在范围内
        if (rate >= lowerBound && rate <= upperBound)
        {
            
            feedbackText.text = "好样的!";
        }
        else
        {
            feedbackText.text = "再试一次!";
        }


        Pouring.allowedToPour = false;
        hints.displayNextHint();
    }
}
