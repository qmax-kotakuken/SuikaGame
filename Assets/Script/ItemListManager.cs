/* �쐬���F2024/9/12
 * �T�v�F�A�C�e���̏��Ԃ̃��X�g���Ǘ��p
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{
    public ItemDropController dropController;
    //�A�C�e���̃����N���Ǘ��p
    public List<GameObject> ItemList = new List<GameObject>();

    public AudioSource rankUpSource;
    public AudioClip rankUpSE;
}
