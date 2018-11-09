using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Graphs
{
    public partial class Form1 : Form
    {
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        List<Weight> MST;
        List<String> Value;
        List<Weight> W;
        List<Weight> KruskalW;
        List<int> Euler;
        int[,] AMatrix; 
        int[,] IMatrix; 
        int[,] e;
        int selected1; 
        int selected2;

        public Form1()
        {
            InitializeComponent();
            MST = new List<Weight>();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            W = new List<Weight>();
            KruskalW = new List<Weight>();
            Value = new List<String>();
            int inintialValue = 1;
            Value.Add(inintialValue.ToString());
            Euler = new List<int>();
            
            sheet.Image = G.GetBitmap();
        }
        
        private void selectButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = false;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
        }
        
        private void drawVertexButton_Click(object sender, EventArgs e)
        {
            drawVertexButton.Enabled = false;
            selectButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
        }
        
        private void drawEdgeButton_Click(object sender, EventArgs e)
        {
            drawEdgeButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            deleteButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
            selected1 = -1;
            selected2 = -1;
        }
        
        private void deleteButton_Click(object sender, EventArgs e)
        {
            deleteButton.Enabled = false;
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
        }
        
        private void deleteALLButton_Click(object sender, EventArgs e)
        {
            selectButton.Enabled = true;
            drawVertexButton.Enabled = true;
            drawEdgeButton.Enabled = true;
            deleteButton.Enabled = true;
            const string message = "Вы действительно хотите полностью удалить граф?";
            const string caption = "Удаление";
            var MBSave = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (MBSave == DialogResult.Yes)
            {
                V.Clear();
                E.Clear();
                G.clearSheet();
                sheet.Image = G.GetBitmap();
            }
        }
        
        private void buttonAdj_Click(object sender, EventArgs e)
        {
            createAdjAndOut();
        }
        
        private void buttonInc_Click(object sender, EventArgs e)
        {
            createIncAndOut();
        }

        private void sheet_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectButton.Enabled == false)
            {
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        if (selected1 != -1)
                        {
                            selected1 = -1;
                            G.clearSheet();
                            G.drawALLGraph(V, W);
                            sheet.Image = G.GetBitmap();
                        }
                        if (selected1 == -1)
                        {
                            G.drawSelectedVertex(V[i].x, V[i].y);
                            selected1 = i;
                            sheet.Image = G.GetBitmap();
                            createAdjAndOut();
                            listBoxMatrix.Items.Clear();
                            int degree = 0;
                            for (int j = 0; j < V.Count; j++)
                                degree += AMatrix[selected1, j];
                            listBoxMatrix.Items.Add("Степень вершины №" + (selected1 + 1) + " равна " + degree);
                            break;
                        }
                    }
                }
            }
            if (drawVertexButton.Enabled == false)
            {
                V.Add(new Vertex(e.X, e.Y));
                G.drawVertex(e.X, e.Y, V.Count.ToString());
                sheet.Image = G.GetBitmap();
            }
            if (drawEdgeButton.Enabled == false)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int i = 0; i < V.Count; i++)
                    {
                        if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                        {
                            if (selected1 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected1 = i;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                            if (selected2 == -1)
                            {
                                G.drawSelectedVertex(V[i].x, V[i].y);
                                selected2 = i;
                                E.Add(new Edge(selected1, selected2));
                                W.Add(new Weight(selected1, selected2, Value[Value.Count-1]));
                                G.drawEdge(V[selected1], V[selected2], E[E.Count - 1], E.Count - 1, W[W.Count - 1] );
                                selected1 = -1;
                                selected2 = -1;
                                sheet.Image = G.GetBitmap();
                                break;
                            }
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    if ((selected1 != -1) &&
                        (Math.Pow((V[selected1].x - e.X), 2) + Math.Pow((V[selected1].y - e.Y), 2) <= G.R * G.R))
                    {
                        G.drawVertex(V[selected1].x, V[selected1].y, (selected1 + 1).ToString());
                        selected1 = -1;
                        sheet.Image = G.GetBitmap();
                    }
                }
            }
            if (deleteButton.Enabled == false)
            {
                bool flag = false; 
                for (int i = 0; i < V.Count; i++)
                {
                    if (Math.Pow((V[i].x - e.X), 2) + Math.Pow((V[i].y - e.Y), 2) <= G.R * G.R)
                    {
                        for (int j = 0; j < E.Count; j++)
                        {
                            if ((E[j].v1 == i) || (E[j].v2 == i))
                            {
                                E.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                if (E[j].v1 > i) E[j].v1--;
                                if (E[j].v2 > i) E[j].v2--;
                            }
                        }
                        V.RemoveAt(i);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    for (int i = 0; i < E.Count; i++)
                    {
                        if (E[i].v1 == E[i].v2) 
                        {
                            if ((Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) <= ((G.R + 2) * (G.R + 2))) &&
                                (Math.Pow((V[E[i].v1].x - G.R - e.X), 2) + Math.Pow((V[E[i].v1].y - G.R - e.Y), 2) >= ((G.R - 2) * (G.R - 2))))
                            {
                                E.RemoveAt(i);
                                flag = true;
                                break;
                            }
                        }
                        else 
                        {
                            if (((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) <= (e.Y + 4) &&
                                ((e.X - V[E[i].v1].x) * (V[E[i].v2].y - V[E[i].v1].y) / (V[E[i].v2].x - V[E[i].v1].x) + V[E[i].v1].y) >= (e.Y - 4))
                            {
                                if ((V[E[i].v1].x <= V[E[i].v2].x && V[E[i].v1].x <= e.X && e.X <= V[E[i].v2].x) ||
                                    (V[E[i].v1].x >= V[E[i].v2].x && V[E[i].v1].x >= e.X && e.X >= V[E[i].v2].x))
                                {
                                    E.RemoveAt(i);
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (flag)
                {
                    G.clearSheet();
                    G.drawALLGraph(V, W);
                    sheet.Image = G.GetBitmap();
                }
            }
        }

        //следующие 2 метода выводы матриц смежности и инцидентности

        private void createAdjAndOut()
        {
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix, W);
            listBoxMatrix.Items.Clear();
            string sOut = "    ";
            for (int i = 0; i < V.Count; i++)
                sOut += (i + 1) + " ";
            listBoxMatrix.Items.Add(sOut);
            for (int i = 0; i < V.Count; i++)
            {
                sOut = (i + 1) + " | ";
                for (int j = 0; j < V.Count; j++)
                    sOut += AMatrix[i, j] + " ";
                listBoxMatrix.Items.Add(sOut);
            }
        }

        private void createIncAndOut()
        {
            if (E.Count > 0)
            {
                IMatrix = new int[V.Count, E.Count];
                G.fillIncidenceMatrix(V.Count, E, IMatrix);
                listBoxMatrix.Items.Clear();
                string sOut = "    ";
                for (int i = 0; i < E.Count; i++)
                    sOut += (char)('a' + i) + " ";
                listBoxMatrix.Items.Add(sOut);
                for (int i = 0; i < V.Count; i++)
                {
                    sOut = (i + 1) + " | ";
                    for (int j = 0; j < E.Count; j++)
                        sOut += IMatrix[i, j] + " ";
                    listBoxMatrix.Items.Add(sOut);
                }
            }
            else
                listBoxMatrix.Items.Clear();
        }

        //методы для получения простых циклов
        private void GetAndPrintCycles(List<Edge> edges)
        {
            listBoxMatrix.Items.Clear();
            int[] color = new int[V.Count];
            for (int i = 0; i < V.Count; i++)
            {
                for (int k = 0; k < V.Count; k++)
                    color[k] = 1;
                List<int> cycle = new List<int>();
                cycle.Add(i + 1);
                DFScycle(i, i, edges, color, -1, cycle);
            }
        }
        private void DFScycle(int u, int endV, List<Edge> edges, int[] color, int unavailableEdge, List<int> cycle)
        {
            if (u != endV)
                color[u] = 2;
            else
            {
                if (cycle.Count >= 2)
                {
                    cycle.Reverse();
                    string s = cycle[0].ToString();
                    for (int i = 1; i < cycle.Count; i++)
                        s += "-" + cycle[i].ToString();
                    bool flag = false; 
                    for (int i = 0; i < listBoxMatrix.Items.Count; i++)
                        if (listBoxMatrix.Items[i].ToString() == s)
                        {
                            flag = true;
                            break;
                        }
                    if (!flag)
                    {
                        cycle.Reverse();
                        s = cycle[0].ToString();
                        for (int i = 1; i < cycle.Count; i++)
                            s += "-" + cycle[i].ToString();
                        listBoxMatrix.Items.Add(s);
                    }
                    return;
                }
            }
            for (int w = 0; w < edges.Count; w++)
            {
                if (w == unavailableEdge)
                    continue;
                if (color[edges[w].v2] == 1 && edges[w].v1 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(edges[w].v2 + 1);
                    DFScycle(edges[w].v2, endV, edges, color, w, cycleNEW);
                    color[edges[w].v2] = 1;
                }
                else if (color[edges[w].v1] == 1 && edges[w].v2 == u)
                {
                    List<int> cycleNEW = new List<int>(cycle);
                    cycleNEW.Add(edges[w].v1 + 1);
                    DFScycle(edges[w].v1, endV, edges, color, w, cycleNEW);
                    color[edges[w].v1] = 1;
                }
            }
        }
        
        //ввод веса
        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Value.Add(textBox1.Text);
            }
        }

        //реализация алгоритма прима
        private void algorithmByPrim(List<Vertex> V, List<Weight> W, List<Weight> MST)
        {
            List<Weight> notUsedE = new List<Weight>(W);
            List<int> usedV = new List<int>();
            List<int> notUsedV = new List<int>();
            for (int i = 0; i < V.Count; i++)
                notUsedV.Add(i);
            Random rand = new Random();
            usedV.Add(rand.Next(0, V.Count));
            notUsedV.RemoveAt(usedV[0]);
            while (notUsedV.Count > 0)
            {
                int minE = -1; 
                for (int i = 0; i < notUsedE.Count; i++)
                {
                    if ((usedV.IndexOf(notUsedE[i].v1) != -1) && (notUsedV.IndexOf(notUsedE[i].v2) != -1) ||
                        (usedV.IndexOf(notUsedE[i].v2) != -1) && (notUsedV.IndexOf(notUsedE[i].v1) != -1))
                    {
                        if (minE != -1)
                        {
                            if (Convert.ToInt32(notUsedE[i].value) < Convert.ToInt32(notUsedE[minE].value))
                                minE = i;
                        }
                        else
                            minE = i;
                    }
                }
                if (usedV.IndexOf(notUsedE[minE].v1) != -1)
                {
                    usedV.Add(notUsedE[minE].v2);
                    notUsedV.Remove(notUsedE[minE].v2);
                }
                else
                {
                    usedV.Add(notUsedE[minE].v1);
                    notUsedV.Remove(notUsedE[minE].v1);
                }
                MST.Add(notUsedE[minE]);
                notUsedE.RemoveAt(minE);
            }
        }
        //кнопка вызывающая алгоритм прима
        private void button2_Click(object sender, EventArgs e)
        {
            algorithmByPrim(V, W, MST);
            G.clearSheet();
            G.drawALLGraph(V, MST);
            sheet.Image = G.GetBitmap();
        }

        //следующие методы служат для алгоритма Крускала
        private static int Find(DrawGraph.Subtree[] subtrees, int i)
        {
            if (subtrees[i].Parent != i)
                subtrees[i].Parent = Find(subtrees, subtrees[i].Parent);

            return subtrees[i].Parent;
        }

        private static void Union(DrawGraph.Subtree[] subtrees, int x, int y)
        {
            int xroot = Find(subtrees, x);
            int yroot = Find(subtrees, y);

            if (subtrees[xroot].Rank < subtrees[yroot].Rank)
                subtrees[xroot].Parent = yroot;
            else if (subtrees[xroot].Rank > subtrees[yroot].Rank)
                subtrees[yroot].Parent = xroot;
            else
            {
                subtrees[yroot].Parent = xroot;
                ++subtrees[xroot].Rank;
            }
        }

        private List<Weight> Kruskal()
        {
            int verticesCount = V.Count;
            Weight[] result = new Weight[verticesCount];
            int i = 0;
            int e = 0;

            W = W.OrderBy(edge => Convert.ToInt32(edge.value)).ToList();

            DrawGraph.Subtree[] subtrees = new DrawGraph.Subtree[verticesCount];

            for (int v = 0; v < verticesCount; ++v)
            {
                subtrees[v].Parent = v;
                subtrees[v].Rank = 0;
            }

            while (e < verticesCount - 1)
            {
                var nextEdge = W[i++];
                int x = Find(subtrees, nextEdge.v1);
                int y = Find(subtrees, nextEdge.v2);

                if (x != y)
                {
                    result[e++] = nextEdge;
                    Union(subtrees, x, y);
                }
            }
            return result.ToList();
        }
        //вызов алгоритма крускала
        private void button1_Click(object sender, EventArgs e)
        {
            KruskalW = Kruskal();
            G.clearSheet();
            G.drawALLGraph(V,KruskalW);
            sheet.Image = G.GetBitmap();
        }

        //объединение алгоритмов прима и крускала и поиск элементарных циклов
        private void button4_Click(object sender, EventArgs e)
        {
            algorithmByPrim(V, W, MST);
            KruskalW = Kruskal();
            var www = MST.Concat(KruskalW).ToList();
            G.clearSheet();
            G.drawALLGraph(V, www);
            sheet.Image = G.GetBitmap();
            var edges = new List<Edge>();
            foreach (var weight in www)
            {
                if (weight != null)
                {
                    edges.Add(new Edge(weight.v1, weight.v2));
                }
            }
            GetAndPrintCycles(edges);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
        }
    }
}
