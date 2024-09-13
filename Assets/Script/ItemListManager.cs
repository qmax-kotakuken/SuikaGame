/* 作成日：2024/9/12
 * 概要：アイテムの順番のリストを管理用
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{
    public ItemDropController dropController;
    //アイテムのランクを管理用
    public List<GameObject> ItemList = new List<GameObject>();

    public AudioSource rankUpSource;
    public AudioClip rankUpSE;
}
