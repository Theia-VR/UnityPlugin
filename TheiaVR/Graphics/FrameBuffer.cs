using System;
using System.Collections.Generic;
using System.Linq;
using TheiaVR.Helpers;
using TheiaVR.Model;

namespace TheiaVR.Graphics
{
    public class FrameBuffer
    {
        private int vertexNumber;

        private List<Vertex> vertexs;

        public FrameBuffer(int aNbOfVertexs)
        {
            vertexNumber = aNbOfVertexs;
            vertexs = new List<Vertex>(aNbOfVertexs);
        }

        public int GetLength()
        {
            lock (vertexs)
            {
                return vertexNumber;
            }
        }

        public int GetCount()
        {
            lock (vertexs)
            {
                return vertexs.Count;
            }
        }

        public bool IsEmpty()
        {
            lock (vertexs)
            {
                return GetCount() <= 0;
            }
        }

        public bool IsFull()
        {
            lock (vertexs)
            {
                return GetCount() >= GetLength();
            }
        }

        public void Queue(Vertex aVertex)
        {
            lock (vertexs)
            {
                if (!IsFull())
                {
                    vertexs.Add(aVertex);
                }
            }
        }

        public void Queue(ICollection<Vertex> aVertexs)
        {
            lock (vertexs)
            {
                if(GetCount() + aVertexs.Count < GetLength())
                {
                    vertexs.AddRange(aVertexs);
                }
            }
        }

        public Vertex Dequeue()
        {
            lock (vertexs)
            {
                Vertex vVertex = null;
                if (IsEmpty())
                {
                    throw new Exception("Buffer is empty");
                }
                vVertex = vertexs[0];
                vertexs.RemoveRange(0, 1);
                return vVertex;
            }
        }

        public List<Vertex> Dequeue(int aNbOfVertexs)
        {
            lock (vertexs)
            {
                List<Vertex> vVertexs = null;

                if (IsEmpty())
                {
                    throw new Exception("Buffer is empty");
                }

                int vNbVertexs = Math.Min(vertexs.Count, aNbOfVertexs);


                vVertexs = vertexs.GetRange(0, vNbVertexs);
                vVertexs.RemoveRange(0, vNbVertexs);

                return vVertexs;
            }
        }

        public List<Vertex> DequeueAll()
        {
            lock (vertexs)
            {
                if (IsEmpty())
                {
                    throw new Exception("Buffer is empty");
                }

                List<Vertex> vVertexs = new List<Vertex>(vertexs);
                vertexs.Clear();
                Messages.Log("Retrait: " + vVertexs.Count);
                return vVertexs;
            }
        }
        
    }
}
