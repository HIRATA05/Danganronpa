using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static IEnumerator PlayAnimAndSetStateWhenFinished(GameObject parent, Animator animator, string clipName, bool activeStateAtTheEnd = true)
    {
        // アニメーションを再生
        animator.Play(clipName, 0, 0f);

        // アニメーションが開始されるまで待機
        yield return new WaitForEndOfFrame();

        // アニメーションが完了するまで待機
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }

        // アニメーション完了後、親オブジェクトのアクティブ状態を設定
        parent.SetActive(activeStateAtTheEnd);
    }
}
