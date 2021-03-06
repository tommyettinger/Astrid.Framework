using System.Collections.Generic;
using Astrid.Core;
using Astrid.Framework.Graphics;

namespace Astrid.Framework.Assets.Fonts
{
    public class BitmapFont : IAsset
    {
        public BitmapFont(string name, Texture texture, FontFile fontFile)
        {
            Name = name;
            _characterMap = BuildCharacterMap(fontFile, texture);
        }

        private struct FontRegion
        {
            public FontChar FontChar { get; set; }
            public TextureRegion TextureRegion { get; set; }
        }

        private readonly Dictionary<char, FontRegion> _characterMap;

        private static Dictionary<char, FontRegion> BuildCharacterMap(FontFile fontFile, Texture texture)
        {
            var characterMap = new Dictionary<char, FontRegion>();

            foreach (var fontChar in fontFile.Chars)
            {
                var character = (char)fontChar.ID;
                var name = character.ToString();
                var region = new TextureRegion(name, texture, fontChar.X, fontChar.Y, fontChar.Width, fontChar.Height);
                var fontRegion = new FontRegion {FontChar = fontChar, TextureRegion = region};
                characterMap.Add(character, fontRegion);
            }

            return characterMap;
        }

        public string Name { get; private set; }
        
        public void Draw(SpriteBatch spriteBatch, string text, int x, int y)
        {
            Draw(spriteBatch, text, x, y, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, string text, int x, int y, Color color)
        {
            var dx = x;
            var dy = y;

            foreach (char character in text)
            {
                FontRegion fontRegion;

                if (_characterMap.TryGetValue(character, out fontRegion))
                {
                    var fontChar = fontRegion.FontChar;
                    var region = fontRegion.TextureRegion;
                    var position = new Vector2(dx + fontChar.XOffset, dy + fontChar.YOffset);

                    spriteBatch.Draw(region, position, color, Vector2.Zero, 0, Vector2.One);
                    dx += fontChar.XAdvance;
                }
            }
        }

        public Rectangle MeasureText(string text, int x, int y)
        {
            var width = 0;
            var height = 0;

            foreach (char c in text)
            {
                FontRegion fontRegion;

                if (_characterMap.TryGetValue(c, out fontRegion))
                {
                    var fc = fontRegion.FontChar;
                    width += fc.XAdvance;

                    if (fc.Height + fc.YOffset > height)
                        height = fc.Height + fc.YOffset;
                }
            }

            return new Rectangle(x, y, width, height);
        }
    }
}