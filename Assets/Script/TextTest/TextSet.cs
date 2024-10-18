using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSet : MonoBehaviour
{
    public ScenarioManager scenarioManager;


    void Start()
    {
        // 会話ノードの作成
        DialogueNode node1 = new DialogueNode("Heloo!");
        DialogueNode node2 = new DialogueNode("Select One!!!");
        DialogueNode node3 = new DialogueNode("Select Two!!!!");
        DialogueNode node4 = new DialogueNode("Select Three!!!!!");

        node1.options.Add(new DialogueOption("Select One", node2));
        node1.options.Add(new DialogueOption("Select Two", node3));
        node1.options.Add(new DialogueOption("Select Three", node4));

        // 会話を開始
        scenarioManager.StartDialogue(node1);
    }

    void Update()
    {
        
    }
}
