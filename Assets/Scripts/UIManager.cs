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

    [SerializeField] float scaleDuration = 1f; // 스케일 변경에 걸리는 시간

    private bool isScaling = false; // 현재 스케일링 중인지 여부를 나타내는 변수
    private float timer = 0f; // 스케일 변경에 사용되는 타이머

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
        // 스케일링 중이면 처리
        updateScale_();
        //updateScale(mainR);
        //updateScale(mainL);
    }

    void updateScale(MainRect m)
    {
        if (m.isScaling)
        {
            timer += Time.deltaTime;

            // Lerp 함수를 사용하여 스케일을 변경
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // 이미지의 스케일을 변경
            m.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();
            // 스케일 변경이 완료되면 스케일링 종료
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

            // Lerp 함수를 사용하여 스케일을 변경
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // 이미지의 스케일을 변경
            mainL.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            mainR.self_l.transform.localScale = new Vector3(2-scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();
            // 배경 컬러 변경
            mainR.self_l.color = Color.Lerp(colorR, ColorB, t);


            // 스케일 변경이 완료되면 스케일링 종료
            if (t >= 1f)
            {
                mainL.isScaling = false;
                timer = 0;
            }
        }
        else if (mainR.isScaling)
        {
            timer += Time.deltaTime;

            // Lerp 함수를 사용하여 스케일을 변경
            float t = Mathf.Clamp01(timer / scaleDuration);
            float scale = Mathf.Lerp(1f, 0f, t);

            // 이미지의 스케일을 변경
            mainR.self_l.transform.localScale = new Vector3(scale, 1f, 1f);
            mainL.self_l.transform.localScale = new Vector3(2 - scale, 1f, 1f);
            horizontalLayoutGroup.CalculateLayoutInputHorizontal();
            horizontalLayoutGroup.SetLayoutHorizontal();

            //컬러변경
            mainL.self_l.color = Color.Lerp(colorL, ColorB, t);


            // 스케일 변경이 완료되면 스케일링 종료
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
