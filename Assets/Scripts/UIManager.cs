using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    MainRect mainL;
    MainRect mainR;

    [SerializeField] Image l_MainL;
    [SerializeField] Image l_MainR;
    [SerializeField] HorizontalLayoutGroup horizontalLayoutGroup;
    [SerializeField] Animator Anim_MainDeco;
    [SerializeField] Color ColorB;

    Color colorL;
    Color colorR;

    class MainRect
    {
        public Image self_l;
        public bool isScaling = false;
    }

    [SerializeField] float scaleDuration = 1f; // ������ ���濡 �ɸ��� �ð�

    private bool isScaling = false; // ���� �����ϸ� ������ ���θ� ��Ÿ���� ����
    private float timer = 0f; // ������ ���濡 ���Ǵ� Ÿ�̸�

    private void Start()
    {
        mainL = new MainRect();
        mainR = new MainRect();
        mainL.self_l = l_MainL;
        mainR.self_l = l_MainR;
        colorL = mainL.self_l.color;
        colorR = mainR.self_l.color;
    }
    void Update()
    {
        // �����ϸ� ���̸� ó��
        updateScale_();
        //updateScale(mainR);
        //updateScale(mainL);
    }

    void updateScale(MainRect m)
    {
        if (m.isScaling)
        {
            timer += Time.deltaTime;

            // Lerp �Լ��� ����Ͽ� �������� ����
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // �̹����� �������� ����
            m.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();
            // ������ ������ �Ϸ�Ǹ� �����ϸ� ����
            if (t >= 1f)
            {
                m.isScaling = false;
                timer = 0;
            }
        }
    }
    void updateScale_()
    {
        if (mainL.isScaling)
        {
            timer += Time.deltaTime;

            // Lerp �Լ��� ����Ͽ� �������� ����
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // �̹����� �������� ����
            mainL.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            mainR.self_l.transform.localScale = new Vector3(2-scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();
            // ��� �÷� ����
            mainR.self_l.color = Color.Lerp(colorR, ColorB, t);


            // ������ ������ �Ϸ�Ǹ� �����ϸ� ����
            if (t >= 1f)
            {
                mainL.isScaling = false;
                timer = 0;
            }
        }
        else if (mainR.isScaling)
        {
            timer += Time.deltaTime;

            // Lerp �Լ��� ����Ͽ� �������� ����
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // �̹����� �������� ����
            mainR.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            mainL.self_l.transform.localScale = new Vector3(2 - scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();

            //�÷�����
            mainL.self_l.color = Color.Lerp(colorL, ColorB, t);


            // ������ ������ �Ϸ�Ǹ� �����ϸ� ����
            if (t >= 1f)
            {
                mainR.isScaling = false;
                timer = 0;
            }
        }
    }
    public void scaleOutL()
    {
        mainL.isScaling = true;
        Anim_MainDeco.SetTrigger("btOn");
    }
    public void scaleOutR()
    {
        mainR.isScaling = true;
        Anim_MainDeco.SetTrigger("btOn");
        // t_MainR.localScale = new Vector3(0, 1, 1);
    }

}
