using UnityEngine;

namespace TheiaVR.Graphics
{
    public class CloudRenderer : KinectRenderer
    {
        private static CloudRenderer instance;
        
        public static CloudRenderer GetInstance()
        {
            return instance;
        }

        private new void Start()
        {
            base.Start();
            instance = this;
            
            //Instanciation of empty sprite
            Vector2 vPivot = new Vector2(1f, 1f);
            Rect vRectangle = new Rect(vPivot, new Vector2(1f, 1f));
            Texture2D vTexture = Texture2D.whiteTexture;
            vTexture.Apply();
            Sprite vSprite = Sprite.Create(vTexture, vRectangle, vPivot);

            //Instanciation of Game Object
            GameObject vGameObject = new GameObject();

            //Adding a spriteRenderer with a sprite and a color
            SpriteRenderer renderer = vGameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = vSprite;

            //Getting the correct scale
            Vector3 vScale = new Vector3(1.5f, 1.5f, 1.5f);
            vGameObject.transform.localScale = vScale;

            base.SetGameObject(vGameObject);
        }
    }
}
