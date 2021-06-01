using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class HeartHealthVisual : MonoBehaviour
{
    public static HeartHealthSystem heartHealthSystemStatic;
    public int m_initHealth;

    public GameObject heartsContainer;
    public Sprite heart0Sprite;
    public Sprite heart1Sprite;
    public Sprite heart2Sprite;
    public Sprite heart3Sprite;
    public Sprite heart4Sprite;
    public AnimationClip heartFullAnimationClip;


    public List<HeartImage> heartsImagesList;
    private HeartHealthSystem heartHealthSystem;
    private bool isHealing;
    
    private void Awake() {
        heartsImagesList = new List<HeartImage>();
    }

    private void Start()
    {
        // Creation des HeartImage à partir des GameObjects contenant les Images
        foreach(KeyValuePair<string, GameObject> children in ChildrenComponents.GetChildren(heartsContainer))
        {
            if (!children.Key.Equals("HeartHealthVisual"))
            {
                HeartImage heartImage = new HeartImage(this, children.Value.GetComponent<Image>(), children.Value.GetComponent<Animation>()); 
                heartsImagesList.Add(heartImage);
            }
        }

        InvokeRepeating("HealingAnimatedPeriodic", 0f, 0.1f);
        //FunctionPeriodic.Create(HealingAnimatedPeriodic,.05f);
        HeartHealthSystem heartHealthSystem = new HeartHealthSystem(5);
        SetHeartHealthSystem(heartHealthSystem);
    }

    public void SetHeartHealthSystem(HeartHealthSystem heartHealthSystem)
    {
        this.heartHealthSystem = heartHealthSystem;
        heartHealthSystemStatic = heartHealthSystem;

        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        Debug.Log(heartList);

        // Définition les HeartImage en fonction des points de vie
        for (int i = 0; i < heartList.Count; i++)
        {
            heartsImagesList[i].SetHeartFragments(heartList[i].GetFragmentAmount());
        }

        heartHealthSystem.OnDamaged += heartHealthSystem_OnDamaged;
        heartHealthSystem.OnHealed += heartHealthSystem_OnHealed;
        heartHealthSystem.OnDead += heartHealthSystem_OnDead;

        string path = Application.dataPath +"/Scripts/UI/numberOfHearts.txt";
        Debug.Log("HeartHealthVisual");
        if (File.Exists(path))
        {
            Debug.Log(File.ReadAllText(path));
            heartHealthSystemStatic.Damage(5 - int.Parse(File.ReadAllText(path)));
        } else {
            heartHealthSystemStatic.Damage(4);
        }
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
        //RefreshAllHearts();
        isHealing = true;
    }

    private void RefreshAllHearts()
    {
        List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();
        for (int i = 0; i < heartsImagesList.Count; i++)
        {
            heartsImagesList[i].SetHeartFragments(heartList[i].GetFragmentAmount());
        }
    }

    private void HealingAnimatedPeriodic()
    {
        if (isHealing)
        {
            bool fullyHealed = true;
            List<HeartHealthSystem.Heart> heartList = heartHealthSystem.GetHeartList();

            for (int i = 0 ; i < heartList.Count;i++) 
            {
                HeartImage heartImage = heartsImagesList[i];
                HeartHealthSystem.Heart heart = heartList[i];

                if (heartImage.GetFragmentAmount() != heart.GetFragmentAmount())
                {
                    heartImage.AddHeartVisualFragment();

                    if (heartImage.GetFragmentAmount() == HeartHealthSystem.MAX_FRAGMENT_AMOUNT)
                    {
                        heartImage.PlayHeartFullAnimation();
                    }

                    fullyHealed = false;
                    break;
                }
            }

            if (fullyHealed)
            {
                isHealing = false;
            }
        }
    }


    public class HeartImage
    {
        private int fragments;
        private Image heartImage;
        private HeartHealthVisual heartHealthVisual;
        private Animation animation;

        public HeartImage(HeartHealthVisual heartHealthVisual, Image heartImage, Animation animation)
        {
            this.heartImage = heartImage;
            this.heartHealthVisual = heartHealthVisual;
            this.animation = animation;
            this.animation.AddClip(heartHealthVisual.heartFullAnimationClip, "HeartFull");
        }

        public void SetHeartFragments(int fragments)
        {
            this.fragments = fragments;
            switch (fragments)
            {
                case 0: 
                    heartImage.sprite = heartHealthVisual.heart0Sprite;
                    break;
                case 1:
                    heartImage.sprite = heartHealthVisual.heart1Sprite;
                    break;
                case 2:
                    heartImage.sprite = heartHealthVisual.heart2Sprite;
                    break;
                case 3:
                    heartImage.sprite = heartHealthVisual.heart3Sprite;
                    break;
                case 4:
                    heartImage.sprite = heartHealthVisual.heart4Sprite;
                    break;
            }
        }

        public int GetFragmentAmount(){
            return this.fragments;
        }

        public void AddHeartVisualFragment(){
            SetHeartFragments(fragments+1);
        }

        public void PlayHeartFullAnimation(){
            animation.Play("HeartFull", PlayMode.StopAll);
        }
    }

    void OnDestroy() {
        print("Script was destroyed");
        string path = Application.dataPath +"/Scripts/UI/numberOfHearts.txt";
        print(path);
        
        File.WriteAllText(path,heartHealthSystemStatic.GetNumberOfHearts().ToString());
        
    }
}
