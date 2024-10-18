using System.Collections.Generic;

[System.Serializable] public class DialogueNode
{
    public string text; // 会話内容
    public List<DialogueOption> options; // 選択肢のリスト

    public DialogueNode(string text)
    {
        this.text = text;
        options = new List<DialogueOption>();
    }
}

[System.Serializable] public class DialogueOption
{
    public string text; // 選択肢のテキスト
    public DialogueNode nextNode; // 次のノード

    public DialogueOption(string text, DialogueNode nextNode)
    {
        this.text = text;
        this.nextNode = nextNode;
    }
}

