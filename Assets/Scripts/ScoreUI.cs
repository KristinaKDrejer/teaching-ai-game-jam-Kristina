using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    public int companyValue = 0;
    public TextMeshProUGUI valueText;

    [SerializeField] private AudioClip positiveValueSound;
    [SerializeField] private AudioClip negativeValueSound;

    void Start()
    {

        UpdateUI();


    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.transform.tag == "Enemy")
        {
            companyValue -= 10;
            valueText.text = "Company Value" + companyValue.ToString();

            //SoundFXManager.Instance.PlaySoundFXClip(positiveValueSound, transform, 0.2f);

            Destroy(other.gameObject);

        }

        if(other.transform.tag =="GoodEnemy")
        {
            companyValue += 10;
            valueText.text = "Company Value" + companyValue.ToString();

            //SoundFXManager.Instance.PlaySoundFXClip(negativeValueSound, transform, 0.2f);

            Destroy(other.gameObject);
        }

        
    }

   
    void UpdateUI()
    {
        valueText.text = "Company Value" + companyValue.ToString();

    }
}
