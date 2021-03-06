﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphs
{
    class Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    class Weight
    {
        public int v1, v2;
        public String value;

        public Weight(int v1, int v2, String value)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.value = value;
        }
    }

    class Edge
    {
        public int v1, v2;

        public Edge(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    class DrawGraph
    {
        Bitmap bitmap;
        Pen blackPen;
        Pen redPen;
        Pen darkGoldPen;
        Graphics gr;
        Font fo;
        Brush br;
        PointF point;
        public int R = 20;

        public struct Subtree
        {
            public int Parent;
            public int Rank;
        }

        public DrawGraph(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            gr = Graphics.FromImage(bitmap);
            clearSheet();
            blackPen = new Pen(Color.Black);
            blackPen.Width = 2;
            redPen = new Pen(Color.Red);
            redPen.Width = 2;
            darkGoldPen = new Pen(Color.DarkGoldenrod);
            darkGoldPen.Width = 2;
            fo = new Font("Arial", 15);
            br = Brushes.Black;
        }

        public Bitmap GetBitmap()
        {
            return bitmap;
        }

        public void clearSheet()
        {
            gr.Clear(Color.White);
        }

        public void drawVertex(int x, int y, string number)
        {
            gr.FillEllipse(Brushes.White, (x - R), (y - R), 2 * R, 2 * R);
            gr.DrawEllipse(blackPen, (x - R), (y - R), 2 * R, 2 * R);
            point = new PointF(x - 9, y - 9);
            gr.DrawString(number, fo, br, point);
        }

        public void drawSelectedVertex(int x, int y)
        {
            gr.DrawEllipse(redPen, (x - R), (y - R), 2 * R, 2 * R);
        }

        public void drawEdge(Vertex V1, Vertex V2, Edge E, int numberE, Weight weight)
        {
            if (E.v1 == E.v2)
            {
                gr.DrawArc(darkGoldPen, (V1.x - 2 * R), (V1.y - 2 * R), 2 * R, 2 * R, 90, 270);
                point = new PointF(V1.x - (int)(2.75 * R), V1.y - (int)(2.75 * R));
                gr.DrawString((weight.value).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
            }
            else
            {
                gr.DrawLine(darkGoldPen, V1.x, V1.y, V2.x, V2.y);
                point = new PointF((V1.x + V2.x) / 2, (V1.y + V2.y) / 2);
                gr.DrawString((weight.value).ToString(), fo, br, point);
                drawVertex(V1.x, V1.y, (E.v1 + 1).ToString());
                drawVertex(V2.x, V2.y, (E.v2 + 1).ToString());
            }
        }

        
        public void drawALLGraph(List<Vertex> V, List<Weight> W)
        {
            for (int i = 0; i < W.Count; i++)
            {
                if (W[i] != null)
                {
                    if (W[i].v1 == W[i].v2)
                    {
                        gr.DrawArc(darkGoldPen, (V[W[i].v1].x - 2 * R), (V[W[i].v1].y - 2 * R), 2 * R, 2 * R, 90, 270);
                        point = new PointF(V[W[i].v1].x - (int)(2.75 * R), V[W[i].v1].y - (int)(2.75 * R));
                    }

                    else
                    {
                        gr.DrawLine(darkGoldPen, V[W[i].v1].x, V[W[i].v1].y, V[W[i].v2].x, V[W[i].v2].y);
                        point = new PointF((V[W[i].v1].x + V[W[i].v2].x) / 2, (V[W[i].v1].y + V[W[i].v2].y) / 2);
                        gr.DrawString(((W[i].value)).ToString(), fo, br, point);
                    }
                }
            }
            for (int i = 0; i < V.Count; i++)
            {
                drawVertex(V[i].x, V[i].y, (i + 1).ToString());
            }
        }
        
        public void fillAdjacencyMatrix(int numberV, List<Edge> E, int[,] matrix, List<Weight> W)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < numberV; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                for (int j = 0; j < W.Count; j++)
                {
                    if (W[j].v1 == E[i].v1 && W[j].v2 == E[i].v2)
                    {
                        matrix[E[i].v1, E[i].v2] = Convert.ToInt32(W[j].value);
                        matrix[E[i].v2, E[i].v1] = Convert.ToInt32(W[j].value);
                    }
                }

            }
        }
        
        public void fillIncidenceMatrix(int numberV, List<Edge> E, int[,] matrix)
        {
            for (int i = 0; i < numberV; i++)
                for (int j = 0; j < E.Count; j++)
                    matrix[i, j] = 0;
            for (int i = 0; i < E.Count; i++)
            {
                matrix[E[i].v1, i] = 1;
                matrix[E[i].v2, i] = 1;
                    
            }
        }

        
    }
}