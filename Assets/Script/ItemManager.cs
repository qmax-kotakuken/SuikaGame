/* �쐬���F2024/9/12
 * �T�v�F
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //���Ԗڂɗ��Ƃ����̂𔻒�p
    public int dropCount;
    public int Score;
    //�A�C�e���̃����N�𕪕ʗp
    public int itemRank = 0;
    ItemListManager itemListManager;
    ScoreManager scoreManager;

    public bool isHit = false;

    // Start is called before the first frame update
    void Start()
    {
        //���Ԃ̃��X�g���擾
        itemListManager = GameObject.Find("ItemListManager").GetComponent<ItemListManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //���̃A�C�e���Ɠ���������
        if(collision.gameObject.CompareTag("Item"))
        {
            isHit = true;
            var item = collision.gameObject.GetComponent<ItemManager>();
            
            //���������A�C�e���̃����N�ƈႤ�ꍇ
            if (item.itemRank != itemRank) return;

            //�����X�C�J�̎�
            if (item.itemRank == itemListManager.ItemList.Count - 1)
            {
                Destroy(this.gameObject);
                Destroy(item.gameObject);
                return;
            }

            //���������A�C�e���̃����N�Ɠ����ꍇ
            //�����̕�����ɗ��Ƃ��ꂽ�ꍇ
            if (dropCount < item.dropCount) 
            {
                Destroy(this.gameObject);
                Destroy(item.gameObject);
                //���̃����N�̃A�C�e���𐶐�����
                var pos = collision.contacts[0].point;
                var next_item = Instantiate(itemListManager.ItemList[itemRank + 1],pos,Quaternion.identity);
                var dropNum = itemListManager.dropController.dropCount++;
                var nextItemManager = next_item.GetComponent<ItemManager>();
                nextItemManager.dropCount = dropNum;
                ScoreAdd(nextItemManager.Score);

                //�i���̎��̉�
                itemListManager.rankUpSource.PlayOneShot(itemListManager.rankUpSE);
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isHit = true;
        }
    }

    //�X�R�A�����Z
    public void ScoreAdd(int _score)
    {
        scoreManager.Score += _score;
    }
}
