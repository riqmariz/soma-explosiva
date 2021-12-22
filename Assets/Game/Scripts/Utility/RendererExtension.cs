
using UnityEngine;

namespace Utility
{
    public static class RendererExtension
    {
        #region Private Fields
        private static readonly Material FlashMaterial = Resources.Load("WhiteFlash") as Material;
        private static readonly Material SpriteDefaultMaterial = Resources.Load("SpriteLitDefault_Material") as Material;
        private static readonly Material FlashMaterial3d = Resources.Load("Flash3D") as Material;
        #endregion

        #region Utility Methods
        public static void WhiteFlash(this SpriteRenderer spriteRenderer, float duration, int repeat = 0)
        {
            Timers.CreateClock(
                spriteRenderer.gameObject, 
                duration,
                () => {spriteRenderer.material = FlashMaterial;},
                (() =>
                {
                    if(repeat<= 0) 
                        spriteRenderer.material = SpriteDefaultMaterial;
                    else
                        WhiteFlash(spriteRenderer,duration,repeat-1);
                }));
        }

        public static void Flash(this Renderer renderer, float duration, int repeat = 1,Material endMaterial = null)
        {
            var halfDuration = duration / 2;
            if (endMaterial == null)
            {
                endMaterial = renderer.material;
            }
            
            Debug.Log("calling flash with: duration: "+duration+", repeat: "+repeat);
            Timers.CreateClock(
                renderer.gameObject, 
                halfDuration,
                () => {renderer.material = FlashMaterial3d;},
                (() =>
                {
                    renderer.material = endMaterial;
                    if(repeat > 1)
                        Timers.CreateClock(
                            renderer.gameObject,
                            halfDuration,
                            () => { },
                            () => { Flash(renderer, duration, repeat - 1, endMaterial); }
                        );
                }));
        }

        public static void AlphaFlash(this SpriteRenderer spriteRenderer,float duration,int repeat = 0, float alpha = 0.5f)
        {
            var originalColor = spriteRenderer.color;
            Timers.CreateClock(
                spriteRenderer.gameObject,
                duration,
                () =>
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b,alpha);
                },
                (() =>
                {
                    if(repeat<= 0) 
                        spriteRenderer.color = originalColor;
                    else
                        AlphaFlash(spriteRenderer,duration,repeat-1,alpha);
                }));
        }
        
        
        #endregion
    }
}
