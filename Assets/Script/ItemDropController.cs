/* 作成日：2024/9/12
 * 概要：アイテムを落とすものの操作
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    //落ちる順番を管理用
    public int dropCount;

    //左右移動の速度
    public float speed;

    public ItemListManager itemListManager;
    public Transform nextItemPos;

    Rigidbody item_rb;
    Rigidbody next_item_rb;
    bool canDrop = false;

    public AudioSource dropSource;
    public AudioClip dropSE;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        //最初のアイテムをセット
        NextItemSet();
        ItemSet();

        Invoke("NextItemSet", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = this.transform.position;
        //左右矢印で左右移動
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
            if(pos.x < -3.3f)
            {
                pos.x = -3.3f;
            }
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;
            if (pos.x > 3.3f)
            {
                pos.x = 3.3f;
            }
        }
        this.transform.position = pos;

        //スペースでアイテムを落とす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!canDrop) return;
            //連打できないように
            canDrop = false;
            //落とすときの音
            dropSource.PlayOneShot(dropSE);
            //重力をリセット
            item_rb.isKinematic = false;
            //追従を解除
            item_rb.transform.parent = null;
            //当たり判定を復活
            item_rb.GetComponent<Collider>().enabled = true;
            //Nextを繰り上げ
            Invoke("ItemSet", 0.5f);
            //Nextを生成
            Invoke("NextItemSet", 1f);
        }
    }

    void NextItemSet()
    {
        //リストの中からランダムで選ぶ
        var random = Random.Range(0, itemListManager.ItemList.Count - 6);
        //アイテムを生成し、右上にセット
        var item = Instantiate(itemListManager.ItemList[random], nextItemPos.position, Quaternion.identity);
        //次のアイテムのRBを取得
        next_item_rb = item.GetComponent<Rigidbody>();
        //次のアイテムの重力を固定
        next_item_rb.isKinematic = true;
        //自機のアイテムと干渉しないように
        next_item_rb.GetComponent<Collider>().enabled = false;
        //落下順番を渡す
        item.GetComponent<ItemManager>().dropCount = dropCount;
        //順番を更新
        dropCount++;
        //次の落下に準備OK
        canDrop = true;
    }

    void ItemSet()
    {
        //次のアイテムを繰り上げて、今から落とすアイテムに
        item_rb = next_item_rb;
        //プレイヤーにつけるよう座標修正
        item_rb.transform.parent = this.transform;
        item_rb.transform.localPosition = Vector3.zero;
    }
}
