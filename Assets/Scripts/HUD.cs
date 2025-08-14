using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// Head-Up Display: 시선 이동 없이 필요한 정보를 눈앞에 표시해주는 기술(체력, 점수, 등)
public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    private Text myText;
    private Slider mySlider;

    private void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.Instance.exp;
                float maxExp = GameManager.Instance.nextExp[Mathf.Min(GameManager.Instance.level, GameManager.Instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.Instance.level); // X:FY X는 인덱스, Y는 소수점 자리 0 이면 소수점 x
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.Instance.kill); // X:FY X는 인덱스, Y는 소수점 자리 0 이면 소수점 x
                break;
            case InfoType.Time:
                float remainTime = GameManager.Instance.maxGameTime - GameManager.Instance.gameTimer;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec); // D는 자리수 고정 D2는 두자리
                break;
            case InfoType.Health:
                float curHealth = GameManager.Instance.health;
                float maxHealth = GameManager.Instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
