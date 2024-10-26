using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static IEnumerator PlayAnimAndSetStateWhenFinished(GameObject parent, Animator animator, string clipName, bool activeStateAtTheEnd = true)
    {
        // �A�j���[�V�������Đ�
        animator.Play(clipName, 0, 0f);

        // �A�j���[�V�������J�n�����܂őҋ@
        yield return new WaitForEndOfFrame();

        // �A�j���[�V��������������܂őҋ@
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // �A�j���[�V����������A�e�I�u�W�F�N�g�̃A�N�e�B�u��Ԃ�ݒ�
        parent.SetActive(activeStateAtTheEnd);
    }
}
