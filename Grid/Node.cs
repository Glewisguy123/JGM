using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Grid.Generation.New
{
    class Node
    {

        private Node parent = null;
        private bool blocked = false;
        private Vector2 position = new Vector2(0, 0);
        private Texture2D tex;
        private int element;
        private Color color = Color.White;
        private int g;
        //
        private int h;

        private int x;
        private int y;

        public Vector2 getGrid { get { return new Vector2(x, y); } }

        public Node Parent { get { return parent; } set { parent = value; } }
        public bool Blocked { get { return blocked; } set { blocked = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public int Element { get { return element; } set { element = value; } }
        public Texture2D Tex { get { return tex; } set { tex = value; } }

        public Rectangle Bounds { get { return new Rectangle(x, y, 64, 64); } }

        public int G { get { return g; } set { g = value; } }
        public int H { get { return h; } set { h = value; } }
        public int F { get { return g + h; } }

        public Color Color { set { color = value; } }


        public Node(int el, Texture2D t)
        {
            element = el;
            tex = t;
        }

        public Node(Texture2D t)
        {

            tex = t;
        }

        public Node(int x, int y, int el, Texture2D t)
        {
            this.x = x;
            this.y = y;
            element = el;
            tex = t;
        }


        public Node(bool blocked, Vector2 pos)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, position, color);
        }

    }
}
