using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class yonetici : MonoBehaviour
{
    public GameObject balon;
    int skor = 0;
    int saniye = 50;
    List<GameObject> balonlar;
    public Text skor_text;
    public Text saniye_text;
 public  AudioSource ses;
    public GameObject yeniden_oyna_panel;
    public float balon_hiz = 5.0f;
     float balon_ekleme_hizi = 1.0f;
    void Start()
    {
        balonlar = new List<GameObject>();
        skor_text.text = "" + skor;
        saniye_text.text = "" + saniye;
        for(int i = 0; i < 20.0f; i++)//balon havuzu
        {
            float rast = Random.Range(-3.5f, 3.5f);
            GameObject y_balon = Instantiate(balon, new Vector3(rast, -3.0f, 1.0f), Quaternion.Euler(0, 0, 180));//quaternion açı ayarlamak
            balonlar.Add(y_balon);
            y_balon.SetActive(false);
        }
        InvokeRepeating("balonGoster", 0.0f, balon_ekleme_hizi);
        InvokeRepeating("saniyeAzalt", 0.0f, 1.0f);
        InvokeRepeating("zorluguArttir", 30f, 30f);


    }
    void zorluguArttir()
    {
        balon_ekleme_hizi -= 0.1f;
        balon_hiz += 1.0f;
        if (balon_ekleme_hizi <= 0.2f)
        {
            balon_ekleme_hizi = 0.2f;
        }
        if (balon_hiz >= 10.0f)
        {
            balon_hiz = 10.0f;
        }
        CancelInvoke("balonGoster");
        InvokeRepeating("balonGoster", 0.0f, balon_ekleme_hizi);
    }
    void balonGoster()
    {
        foreach(GameObject b1 in balonlar)
        {
            if (b1.activeSelf == false)
            {
                b1.SetActive(true);
                float rast = Random.Range(-3.5f, 3.5f);
                b1.transform.position = new Vector3(rast, -3.0f, 1);
                break;
            }
        }
    }
    void saniyeAzalt()
    {
        saniye--;
        saniye_text.text = "" + saniye;
        if (saniye <= 0)
        {
            yeniden_oyna_panel.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void skoruDegistir(int deger)
    {
        skor += deger;
        skor_text.text = "" + skor;
    }
    public void saniyeDegistir(int deger)
    {
        saniye += deger;
        saniye_text.text = "" + saniye;
    }
    public void cikisYap()
    {
        Application.Quit();
    }

    void Update()
    {/*
        if (Input.touchCount > 0)
        {
            Touch parmak = Input.GetTouch(0);
            RaycastHit nesne;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(parmak.position),out nesne))
            {
                if (nesne.collider.tag == "balon")
                {
                    ses.Play();
                    nesne.collider.GetComponent<balon>().patlatildi = true;
                    nesne.collider.gameObject.SetActive(false);
                }
            }
        }
       */
    }
    public void yenidenOyna()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
