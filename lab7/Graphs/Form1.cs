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
        int time;
        int NIL;
        DrawGraph G;
        List<Vertex> V;
        List<Edge> E;
        List<String> Value;
        List<Weight> W;
        int[,] AMatrix; //матрица смежности
        int[,] IMatrix; //матрица инцидентности
        int selected1; //выбранные вершины, для соединения линиями
        int selected2;

        public Form1()
        {
            InitializeComponent();
            V = new List<Vertex>();
            G = new DrawGraph(sheet.Width, sheet.Height);
            E = new List<Edge>();
            W = new List<Weight>();
            Value = new List<String>();
            int inintialValue = 1;
            Value.Add(inintialValue.ToString());
            
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

        
        
        //ввод веса
        private void textBox1_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Value.Add(textBox1.Text);
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            G.clearSheet();
            G.drawALLGraph(V, W);
            sheet.Image = G.GetBitmap();
        }
        private bool isBCUtil(int u, bool[] visited, int[] disc, int[] low, int[] parent, int[,] adjMatrix, int verticesAmo)
        {
            
            int children = 0;
            
            visited[u] = true;
            
            disc[u] = low[u] = ++time;
 
            for (int i = 0; i < verticesAmo; i++)
            {
                if (adjMatrix[u, i] == 0)
                {
                    continue;
                }

                int v = i; 
                
                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    
                    if (isBCUtil(v, visited, disc, low, parent, adjMatrix, verticesAmo))
                    {

                        return true;
                    }
                    
                    low[u] = Math.Min(low[u], low[v]);
                    
                    if (parent[u] == NIL && children > 1)
                    {
                        return true;
                    }
 
                    if (parent[u] != NIL && low[v] >= disc[u])
                    {
                        return true;
                    }
                }
 
                else if (v != parent[u])
                {
                    low[u] = Math.Min(low[u], disc[v]);
                }
            }
            return false;
        }

        public bool BC(int verticesAmount, int[,] adjMatrix)
        {
            bool[] visited = new bool[verticesAmount];
            int[] disc = new int[verticesAmount];
            int[] low = new int[verticesAmount];
            int[] parent = new int[verticesAmount];
            
            for (int i = 0; i < verticesAmount; i++)
            {
                parent[i] = NIL;
                visited[i] = false;
            }
            
            if (isBCUtil(0, visited, disc, low, parent, adjMatrix, verticesAmount) == true)
            {
                return false;
            }
            
            for (int i = 0; i < verticesAmount; i++)
            {
                if (visited[i] == false)
                {
                    return false;
                }
            }

            return true;
        }

        
        private bool isBCUtil2(int u, bool[] visited, int[] disc, int[] low, int[] parent, int[,] adjMatrix, int verticesAmo)
        {
            
            int children = 0;
            
            visited[u] = true;
            
            disc[u] = low[u] = ++time;
            
            for (int i = 0; i < verticesAmo; i++)
            {
                if (adjMatrix[u, i] == 0)
                {
                    continue;
                }

                int v = i;  
 
                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    
                    if (isBCUtil2(v, visited, disc, low, parent, adjMatrix, verticesAmo))
                    {
                        return true;
                    }
                    
                    low[u] = Math.Min(low[u], low[v]);
                    
                    if (parent[u] == NIL && children > 1)
                    {
                        List<int> vert = new List<int>();

                        for (int j = 0; j < V.Count; j++)
                        {
                            if (adjMatrix[u, j] != 0)
                            {
                                vert.Add(j);
                                adjMatrix[u, j] = 0;
                                adjMatrix[j, u] = 0;
                            }
                        }

                        string[] used = new string[V.Count];
                        string symbol = "area";
                        int m = 0;

                        for (int j = 0; j < V.Count; j++)
                        {

                            if (String.IsNullOrEmpty(used[j]))
                            {
                                bfs(j, adjMatrix, used, symbol);
                                m++;
                                symbol = (string)("area" + m);
                            }
                        }

                        bool flag = false;

                        for (int j = 0; j < V.Count; j++)
                        {
                            for (int k = j + 1; k < V.Count; k++)
                            {
                                if (k == u || u == j) continue;

                                if (!used[j].Equals(used[k]) && adjMatrix[j, k] == 0 && adjMatrix[k, j] == 0)
                                {
                                    Weight w = new Weight(j, k, "1");
                                    W.Add(w);
                                    adjMatrix[j, k] = 1;
                                    adjMatrix[k, j] = 1;
                                    listBoxMatrix.Items.Add(j + " " + k);
                                    flag = true;
                                    break;
                                }
                            }

                            if (flag) break;
                        }

                        for (int j = 0; j < vert.Count; j++)
                        {
                            if (adjMatrix[u, vert[j]] == 0)
                            {
                                adjMatrix[u, vert[j]] = 1;
                                adjMatrix[vert[j], u] = 1;
                            }
                        }
                    }

                    
                    if (parent[u] != NIL && low[v] >= disc[u])
                    {
                       

                        List<int> vert = new List<int>();

                        for (int j = 0; j < V.Count; j++)
                        {
                            if (adjMatrix[u, j] != 0)
                            {
                                vert.Add(j);
                                adjMatrix[u, j] = 0;
                                adjMatrix[j, u] = 0;
                            }
                        }
                        

                        string[] used = new string[V.Count];
                        string symbol = "area";
                        int m = 0;

                        for (int j = 0; j < V.Count; j++)
                        {

                            if (String.IsNullOrEmpty(used[j]))
                            {
                                bfs(j, adjMatrix, used, symbol);
                                m++;
                                symbol = (string)("area" + m);
                            }
                        }

                        bool flag = false;

                        for (int j = 0; j < V.Count; j++)
                        {
                            for (int k = j + 1; k < V.Count; k++)
                            {
                                if (k == u || u == j) continue;

                                if (!used[j].Equals(used[k]) && adjMatrix[j, k] == 0 && adjMatrix[k, j] == 0)
                                {
                                    Weight w = new Weight(j, k, "1");
                                    W.Add(w);
                                    adjMatrix[j, k] = 1;
                                    adjMatrix[k, j] = 1;
                                    listBoxMatrix.Items.Add(j + " " + k);
                                    flag = true;
                                    break;
                                }


                            }

                            if (flag) break;
                        }

                        for (int j = 0; j < vert.Count; j++)
                        {
                            if (adjMatrix[u, vert[j]] == 0)
                            {
                                adjMatrix[u, vert[j]] = 1;
                                adjMatrix[vert[j], u] = 1;
                            }
                        }
                    }
                }


                else if (v != parent[u])
                {
                    low[u] = Math.Min(low[u], disc[v]);
                }
            }
            return false;
        }
        public bool BC2(int verticesAmount, int[,] adjMatrix)
        {

            bool[] visited = new bool[verticesAmount];
            int[] disc = new int[verticesAmount];
            int[] low = new int[verticesAmount];
            int[] parent = new int[verticesAmount];


            for (int i = 0; i < verticesAmount; i++)
            {
                parent[i] = NIL;
                visited[i] = false;
            }


            if (isBCUtil2(0, visited, disc, low, parent, adjMatrix, verticesAmount) == true)
            {
                return false;
            }

            for (int i = 0; i < verticesAmount; i++)
            {
                if (visited[i] == false)
                {
                    return false;
                }
            }

            return true;
        }
        private void bfs(int v, int[,] AMatrix, string[] used, string symbol)
        {
            used[v] = symbol;
            Queue<int> q = new Queue<int>();

            q.Enqueue(v);
            while (q.Count != 0)
            {

                v = q.Dequeue();
                for (int i = 0; i < V.Count; i++)
                {
                    if (AMatrix[i, v] != 0 && String.IsNullOrEmpty(used[i]))
                    {
                        used[i] = symbol;
                        q.Enqueue(i);
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {

            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix, W);
            BC2(V.Count, AMatrix);
            G.clearSheet();
            G.drawALLGraph(V, W);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix, W);
            if (BC(V.Count, AMatrix))
            {
                listBoxMatrix.Items.Add("True");
            }
            else
            {
                listBoxMatrix.Items.Add("False");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AMatrix = new int[V.Count, V.Count];
            G.fillAdjacencyMatrix(V.Count, E, AMatrix, W);
            BC2(V.Count, AMatrix);
            G.clearSheet();
            G.drawALLGraph(V, W);
        }
    }
}
