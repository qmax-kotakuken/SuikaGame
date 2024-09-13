/* �쐬���F2024/9/12
 * �T�v�F�A�C�e���𗎂Ƃ����̂̑���
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    //�����鏇�Ԃ��Ǘ��p
    public int dropCount;

    //���E�ړ��̑��x
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
        //�ŏ��̃A�C�e�����Z�b�g
        NextItemSet();
        ItemSet();

        Invoke("NextItemSet", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        var pos = this.transform.position;
        //���E���ō��E�ړ�
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

        //�X�y�[�X�ŃA�C�e���𗎂Ƃ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!canDrop) return;
            //�A�łł��Ȃ��悤��
            canDrop = false;
            //���Ƃ��Ƃ��̉�
            dropSource.PlayOneShot(dropSE);
            //�d�͂����Z�b�g
            item_rb.isKinematic = false;
            //�Ǐ]������
            item_rb.transform.parent = null;
            //�����蔻��𕜊�
            item_rb.GetComponent<Collider>().enabled = true;
            //Next���J��グ
            Invoke("ItemSet", 0.5f);
            //Next�𐶐�
            Invoke("NextItemSet", 1f);
        }
    }

    void NextItemSet()
    {
        //���X�g�̒����烉���_���őI��
        var random = Random.Range(0, itemListManager.ItemList.Count - 6);
        //�A�C�e���𐶐����A�E��ɃZ�b�g
        var item = Instantiate(itemListManager.ItemList[random], nextItemPos.position, Quaternion.identity);
        //���̃A�C�e����RB���擾
        next_item_rb = item.GetComponent<Rigidbody>();
        //���̃A�C�e���̏d�͂��Œ�
        next_item_rb.isKinematic = true;
        //���@�̃A�C�e���Ɗ����Ȃ��悤��
        next_item_rb.GetComponent<Collider>().enabled = false;
        //�������Ԃ�n��
        item.GetComponent<ItemManager>().dropCount = dropCount;
        //���Ԃ��X�V
        dropCount++;
        //���̗����ɏ���OK
        canDrop = true;
    }

    void ItemSet()
    {
        //���̃A�C�e�����J��グ�āA�����痎�Ƃ��A�C�e����
        item_rb = next_item_rb;
        //�v���C���[�ɂ���悤���W�C��
        item_rb.transform.parent = this.transform;
        item_rb.transform.localPosition = Vector3.zero;
    }
}
