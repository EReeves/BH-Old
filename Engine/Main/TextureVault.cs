using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using System.IO;

namespace Client
{
    class TextureVault
    {
        private static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public TextureVault()
        {
            
        }

        /// <summary>
        /// Add a texture to the vault.
        /// </summary>
        /// <param name="path">Sprites/TextureName.png</param>
        public static void AddTexture(string path)
        {
            ParseTexture(path);
        }

        /// <summary>
        /// Be careful with this, don't unload a texture that is in use.
        /// </summary>
        /// <param name="name">TextureName.png</param>
        public static void UnloadTexture(string name)
        {
            textures.Remove(name.ToUpper());
        }

        /// <summary>
        /// Gets texture from vault, if Texture is not found it will attempt to load it. 
        /// </summary>
        /// <param name="path">Sprites/TextureName.png</param>
        /// <returns>Texture</returns>
        public static Texture GetTexture(string path)
        {
            return ParseTexture(path);
        }

        private static Texture ParseTexture(string path)
        {
            string key = path.ToUpper();
            if (!textures.ContainsKey(key))
                textures.Add(key, new Texture("Content/" + path));
            return textures[key];
        }
        
    }
}
