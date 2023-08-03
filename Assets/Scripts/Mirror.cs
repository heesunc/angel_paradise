using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    Camera mirrorcam;
    Texture2D t2d;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        mirrorcam = GetComponent<Camera>();
        // ���� �ؽ����� ũ�⸸ŭ ����
        t2d = new Texture2D(mirrorcam.targetTexture.width, mirrorcam.targetTexture.height, textureFormat: TextureFormat.ARGB32, false);
        StartCoroutine(camrender());
    }

    WaitForEndOfFrame WaitForEnd = new WaitForEndOfFrame();
    // WaitForEndOfFrame : ��� ī�޶� �������� �Ϸ��� ������ ��ٸ�
    IEnumerator camrender()
    {
        while (true)
        {
            yield return WaitForEnd;
            RenderTexture.active = mirrorcam.targetTexture; // ī�޶� �߰��� ���� �ؽ��� Ȱ��ȭ
            mirrorcam.Render(); // ī�޶� �о���
            // ReadPixels �Լ��� ���� ������ �����ؽ����� ������ ������ �� ����
            t2d.ReadPixels(new Rect(0, 0, mirrorcam.targetTexture.width, mirrorcam.targetTexture.height), 0, 0);
            t2d.Apply(); // ����

            // ��������Ʈ�� t2d�� ������ ����
            sr.sprite = Sprite.Create(t2d, new Rect(0, 0, t2d.width, t2d.height), new Vector2(0.5f, 0.5f), t2d.width);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
