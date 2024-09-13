/* 作成日：2024/9/12
 * 概要：
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //何番目に落としたのを判定用
    public int dropCount;
    //アイテムのランクを分別用
    public int itemRank = 0;
    ItemListManager itemListManager;
    public bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        //順番のリストを取得
        itemListManager = GameObject.Find("ItemListManager").GetComponent<ItemListManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //他のアイテムと当たった時
        if(collision.gameObject.CompareTag("Item"))
        {
            isHit = true;
            var item = collision.gameObject.GetComponent<ItemManager>();
            
            //当たったアイテムのランクと違う場合
            if (item.itemRank != itemRank) return;

            //両方スイカの時
            if (item.itemRank == itemListManager.ItemList.Count - 1)
            {
                Destroy(this.gameObject);
                Destroy(item.gameObject);
                return;
            }

            //当たったアイテムのランクと同じ場合
            //自分の方が先に落とされた場合
            if (dropCount < item.dropCount) 
            {
                Destroy(this.gameObject);
                Destroy(item.gameObject);
                //一個上のランクのアイテムを生成する
                var pos = collision.contacts[0].point;
                var next_item = Instantiate(itemListManager.ItemList[itemRank + 1],pos,Quaternion.identity);
                var dropNum = itemListManager.dropController.dropCount++;
                next_item.GetComponent<ItemManager>().dropCount = dropNum;

                //進化の時の音
                itemListManager.rankUpSource.PlayOneShot(itemListManager.rankUpSE);
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isHit = true;
        }
    }
}
