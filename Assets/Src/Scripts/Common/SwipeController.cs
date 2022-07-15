using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menus
{
    public class SwipeController : MonoBehaviour
    {
        public GameObject scrollbar;
        private float scrollPos = 0;
        private float distance;
        private int levelCount;
        private int btnCurrent;
        private bool btnSwipe;
        private void Start()
        {
            btnSwipe = false;
            levelCount = transform.childCount;
            
            double currentRatio = ((float) Screen.width) / ((float) Screen.height);
            double currentWidth = currentRatio * 1080.0;
            double additionalWidth = currentWidth - 1920.0;

            double padding = GetComponent<HorizontalLayoutGroup>().padding.left;
            padding += (additionalWidth / 2.0);
            
            GetComponent<HorizontalLayoutGroup>().padding = new RectOffset((int) padding, (int) padding, 0, 0);
        }
        
        void Update()
        {
            int current = CalcCurrent();
            if (btnSwipe)
            {
                if (btnCurrent != current)
                {
                    current = btnCurrent;
                }
                else
                {
                    btnSwipe = false;
                }
            }

            if (Input.GetMouseButton(0))
            {
                // immer, wenn touch
                scrollPos = scrollbar.GetComponent<Scrollbar>().value; // prozent: 0.0 - 1.0  
            }
            else 
            {
                // wenn losgelassen -> snap zu nähestem Level
                float position = 1f / (levelCount - 1) * current;
                scrollbar.GetComponent<Scrollbar>().value =  
                    Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, position, 0.1f);
            }

            transform.GetChild(current).localScale =
                Vector2.Lerp(transform.GetChild(current).localScale, new Vector2(1f, 1f), 0.1f);
            
            for (int i = 0; i < levelCount; i++)
            {
                if (i != current)
                {
                    transform.GetChild(i).localScale =
                        Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
            }
        }
        
        private int CalcCurrent()
        {
            int localCurrent = (int) Math.Round(scrollPos / (1.0 / (levelCount - 1)));
            if (localCurrent < 0)
            {
                localCurrent = 0;
            }
            if (localCurrent > levelCount - 1)
            {
                localCurrent = levelCount - 1;
            }

            return localCurrent;
        }
        
        public void Swipe(int direction)
        {
            btnCurrent = CalcCurrent() + direction;
            if (btnCurrent < 0)
            {
                btnCurrent = 0;
            }
            if (btnCurrent > levelCount - 1)
            {
                btnCurrent = levelCount - 1;
            }
            btnSwipe = true;
        }
    }
}
