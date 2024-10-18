using System.Collections.Generic;

[System.Serializable] public class DialogueNode
{
    public string text; // ��b���e
    public List<DialogueOption> options; // �I�����̃��X�g

    public DialogueNode(string text)
    {
        this.text = text;
        options = new List<DialogueOption>();
    }
}

[System.Serializable] public class DialogueOption
{
    public string text; // �I�����̃e�L�X�g
    public DialogueNode nextNode; // ���̃m�[�h

    public DialogueOption(string text, DialogueNode nextNode)
    {
        this.text = text;
        this.nextNode = nextNode;
    }
}

