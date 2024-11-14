using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �q�ɂ���I�u�W�F�N�g���~��ɔz�u����N���X
/// </summary>
public class CircleDeployer : MonoBehaviour
{

    //���a
    [SerializeField, Header("���̒l�ŉ~�`�͈͂̔��a�𒲐�")]
    private float _radius;

    private void Awake()
    {
        Deploy();
    }

    //Inspector�̓��e(���a)���ύX���ꂽ���Ɏ��s
    private void OnValidate()
    {
        Deploy();
    }

    //�q���~��ɔz�u����(ContextMenu�Ō��}�[�N�̏��Ƀ��j���[�ǉ�)
    [ContextMenu("Deploy")]
    private void Deploy()
    {

        //�q���擾
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
        }

        //���l�A�A���t�@�x�b�g���Ƀ\�[�g
        childList.Sort(
          (a, b) => {
              return string.Compare(a.name, b.name);
          }
        );

        //�I�u�W�F�N�g�Ԃ̊p�x��
        float angleDiff = 360f / (float)childList.Count;

        //�e�I�u�W�F�N�g���~��ɔz�u
        for (int i = 0; i < childList.Count; i++)
        {
            Vector3 childPostion = transform.position;

            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            childPostion.x += _radius * Mathf.Cos(angle);
            childPostion.z += _radius * Mathf.Sin(angle);

            childList[i].transform.position = childPostion;
        }

    }

}
