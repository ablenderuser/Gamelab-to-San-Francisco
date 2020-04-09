using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartHealthVisual : MonoBehaviour
{
    public static HeartHealthSystem heartHealthSystemStatic;
    [SerializeField] private Sprite heart0Sprite;
    [SerializeField] private Sprite heart1Sprite;
    [SerializeField] private Sprite heart2Sprite;
    [SerializeField] private Sprite heart3Sprite;
    [SerializeField] private Sprite heart4Sprite;

    private List<HeartImage> HeartImageList;
    private HeartHealthSystem heartHealthSystem;

    private void Awake()
    {
        HeartImageList = new List<HeartImage>();
    }

    private void Start()
    {
        HeartHealthSystem heartHealthSystem = new HeartHealthSystem(40);
        SetHeartHealthSystem(heartHealthSystem);

        
    }

    public void SetHeartHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;
        heartHealthSystemStatic = heartHealthSystem;

        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Vector2 heartAnchoredPosition = new Vector2(-350, 100);

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(20, 0);
            if (i%10==9)
            {
                
                heartAnchoredPosition += new Vector2(-200, -40*( (i/10)+1   ));
                Debug.Log("i = " + i + " ; (i/10)+1 =  "+ ((i / 10) + 1));
            }
        }
        heartHealthSystem.OnDamaged += heartHealthSystem_OnDamaged;
        heartHealthSystem.OnHealed += heartHealthSystem_OnHealed;
        heartHealthSystem.OnDead += heartHealthSystem_OnDead;
        //heartHealthSystem.Damage(2);
        //heartHealthSystem.Heal(1);
        heartHealthSystemStatic.Damage(4*35);
        

    }

    private void heartHealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }
    private void heartHealthSystem_OnDead(object sender, System.EventArgs e)
    {
        //CMDebug.TextPopupMouse("Dead !");
    }
    private void heartHealthSystem_OnHealed(object sender, System.EventArgs e)
    {
        RefreshAllHearts();
    }

    private void RefreshAllHearts()
    {
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < HeartImageList.Count; i++)
        {
            HeartImage heartImage = HeartImageList[i];
            HeartHealthSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmount());

        }
    }
    /*
    private void HealMainFunction(List<HeartHealthSystem.Heart> heartList, Vector2 heartAnchoredPosition,int HealAmount)
    {
        int newHearts = heartHealthSystem.Heal(HealAmount);
        Debug.Log("newHearts = " + newHearts);
        for (int i = heartList.Count - newHearts; i < heartList.Count; i++)
        {
            HeartHealthSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchoredPosition).SetHeartFragments(heart.GetFragmentAmount());
            heartAnchoredPosition += new Vector2(30, 0);
            Debug.Log("i = " + i);
            if (i == 9 || i == 19 || i == 29)
            {
                heartAnchoredPosition = new Vector2(0, -70 * ((i / 10) + 1));
                Debug.Log("i = " + i + " ; (i/10)+1 =  " + ((i / 10) + 1));
            }
        }
    }*/

    private HeartImage CreateHeartImage( Vector2 anchoredPosition)
    {
        GameObject heartGameObject = new GameObject("Heart", typeof(Image));
        heartGameObject.transform.parent = transform;
        heartGameObject.transform.localPosition = Vector3.zero;

        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 60);

        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = heart1Sprite;

        HeartImage heartImage = new HeartImage(this,heartImageUI);
        HeartImageList.Add(heartImage);

        return heartImage;
    }

    public class HeartImage
    {
        private Image heartImage;
        private HeartHealthVisual heartHealthVisual;
        public HeartImage(HeartHealthVisual heartHealthVisual, Image heartImage)
        {
            this.heartImage = heartImage;
            this.heartHealthVisual = heartHealthVisual;
        }
        public void SetHeartFragments(int fragments)
        {
            switch (fragments)
            {
                case 0: heartImage.enabled  = false; break;
                case 1: heartImage.enabled = true; heartImage.sprite = heartHealthVisual.heart1Sprite; break;
                case 2: heartImage.enabled = true; heartImage.sprite = heartHealthVisual.heart2Sprite; break;
                case 3: heartImage.enabled = true; heartImage.sprite = heartHealthVisual.heart3Sprite; break;
                case 4: heartImage.enabled = true; heartImage.sprite = heartHealthVisual.heart4Sprite; break;


                //case 0: heartImage.sprite = heartHealthVisual.heart0Sprite; break;
            }
        }
    }
}
