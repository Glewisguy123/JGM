using Engine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADS.Collision.SAT
{
    public class Hitbox
    {
        // SAT
        protected List<Vector2> points;
        protected List<Vector2> edges;

        public List<Vector2> Points
        {
            get { return points; }
            set { points = value; }
        }

        public List<Vector2> Edges
        {
            get { return edges; }
            set { edges = value; }
        }

        protected Vector2 velocity;
        public Vector2 Velocity { get { return velocity; } set { velocity = value; } }

        private float width;
        private float height;
        private float rotation;

        //Reference to the mind.
        protected Mind _mind;
        public Mind Mind { get { return _mind; } set { _mind = value; } }

        public Vector2 Centre;

        public Hitbox(Vector2 _pos, float pWidth, float pHeight, float pRot, Mind parent)
        {
            width = pWidth;
            height = pHeight;
            rotation = pRot;
            points = new List<Vector2>(4);
            edges = new List<Vector2>(4);
            points.Add(_pos);
            points.Add(new Vector2(_pos.X + width, _pos.Y));
            points.Add(new Vector2(_pos.X + width, _pos.Y + height));
            points.Add(new Vector2(_pos.X, _pos.Y + height));
            Centre = centrePoint();
            Centre = createRotation(centrePoint());
            createMatrix();
            CreateEdges();
            _mind = parent;
        }

        public void createMatrix()
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = createRotation(points[i]);
            }
        }

        public Vector2 createRotation(Vector2 _point)
        {
            Vector2 origin = _point;
            _point = Vector2.Transform(_point - Centre, Matrix.CreateRotationZ(MathHelper.ToRadians(rotation)));
            _point += Centre;
            return _point;
        }
        public void CreateEdges()
        {
            Vector2 point1;
            Vector2 point2;
            for (int i = 0; i < points.Count; i++)
            {
                point1 = points[i];
                if (i + 1 == points.Count)
                {
                    point2 = points[0];
                }
                else
                {
                    point2 = points[i + 1];
                }

                Edges.Add(point2 - point1);
            }
        }

        public void UpdatePoint(Vector2 velocity)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] += velocity;
            }
        }

        /// <summary>
        /// the centre point of an object, so that the radius of a circle may be applied from it
        /// </summary>
        public Vector2 centrePoint()
        {
            float midX = points[0].X + (width / 2); // x coordinate for the first entity
            float midY = points[0].Y + (height / 2); // y coordinate for the first entity

            Vector2 centre = new Vector2(midX, midY); // making coordinates into a new vector for the first entity

            return centre;
        }

        public virtual void Update()
        {

        }

    }
}
