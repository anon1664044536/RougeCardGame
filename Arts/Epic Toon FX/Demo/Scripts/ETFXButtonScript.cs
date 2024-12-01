using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace EpicToonFX
{
    public class ETFXButtonScript : MonoBehaviour
    {
        public GameObject Button;
        Text MyButtonText;
        string projectileParticleName;      // 用于更新按钮文本组件的变量 

        ETFXFireProjectile effectScript;        //用于访问射弹列表的变量 
        ETFXProjectileScript projectileScript;

        public float buttonsX;
        public float buttonsY;
        public float buttonsSizeX;
        public float buttonsSizeY;
        public float buttonsDistance;

        void Start()
        {
            effectScript = GameObject.Find("ETFXFireProjectile").GetComponent<ETFXFireProjectile>();
            getProjectileNames();
            MyButtonText = Button.transform.Find("Text").GetComponent<Text>();
            MyButtonText.text = projectileParticleName;
        }

        void Update()
        {
            MyButtonText.text = projectileParticleName;
            //		print(projectileParticleName);
        }

        public void getProjectileNames()            //查找并显示当前选中的弹丸名称 
        {
            // 访问当前选定的弹丸的“ProjectileScript” 
            projectileScript = effectScript.projectiles[effectScript.currentProjectile].GetComponent<ETFXProjectileScript>();
            projectileParticleName = projectileScript.projectileParticle.name;  // 将当前选定的射弹的名称分配给 projectileParticleName 
        }

        public bool overButton()        // 此函数将返回 true 或 false 
        {
            Rect button1 = new Rect(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY);
            Rect button2 = new Rect(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY);

            if (button1.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)) ||
               button2.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
            {
                return true;
            }
            else
                return false;
        }
    }
}