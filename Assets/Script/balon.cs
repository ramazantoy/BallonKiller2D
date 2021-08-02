using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balon : MonoBehaviour
{
    float hiz;
    Color[] renkler;
    yonetici y;
    MeshRenderer gorunurluk;
   public bool patlatildi = false;
    public GameObject patlama_efekti;
    private void OnEnable()//setactive true oldugunda çalışır
    {
        y = GameObject.Find("yonetici").GetComponent<yonetici>();
        hiz = y.balon_hiz;
        gorunurluk = gameObject.GetComponent<MeshRenderer>();
        renkAyarla();
        CancelInvoke("sil");// her doğdugunda kaldığı yerden devam edip silmesin diye cancel invoke
        Invoke("sil", 3.0f);
    }
    private void OnMouseDown()
    {
        patlatildi = true;
        y.ses.Play();
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (patlatildi)
        {
            GameObject y_efekt = Instantiate(patlama_efekti, transform.position, Quaternion.identity);
            y_efekt.GetComponent<ParticleSystem>().startColor = gorunurluk.material.color;
            Destroy(y_efekt, 0.5f);
            if (gorunurluk.material.color == renkler[0])//patlayan balon kırmızı ise
            {
                y.saniyeDegistir(-5);
                y.skoruDegistir(-10);
            }
            else//diğer renkler için
            {
                y.saniyeDegistir(1);
                y.skoruDegistir(10);
            }
            patlatildi = false;

        }
    
    }
    void sil()
    {
        gameObject.SetActive(false);
    }
    void renkAyarla()
    {
        renkler = new Color[4];
        renkler[0] = Color.red;
        renkler[1] = Color.blue;
        renkler[2] = Color.green;
        renkler[3] = Color.yellow;
        int ran = Random.Range(0, 4);
  gorunurluk.material.color = renkler[ran];
    }

    void Update()
    {
        transform.Translate(0, -hiz * Time.deltaTime, 0);
    }
}
