using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Алгоритм_Крускала
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Clear();
        }

        const int radius = 20;
        const int halfRadius = (radius / 2);

        Color vertexColor = Color.Aqua;
        Color edgeColor = Color.Red;

        IList<Vertex> vertices;
        IList<Edge> graph, graphSolved;

        Vertex firstVertex, secondVertex;

        bool drawEdge, solved;

        private void DrawEdges(Graphics g)
        {
            Pen p = new Pen(edgeColor);
            var edges = solved ? graphSolved : graph;

            foreach (Edge e in edges)
            {
                Point v1 = new Point(e.V1.Position.X + halfRadius, e.V1.Position.Y + halfRadius);
                Point v2 = new Point(e.V2.Position.X + halfRadius, e.V2.Position.Y + halfRadius);
                g.DrawLine(p, v1, v2);
                DrawString(e.Cost.ToString(), e.StringPosition, g);
            }
        }

        private void DrawString(string strText, Point pDrawPoint, Graphics g)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(strText, drawFont, drawBrush, pDrawPoint);
        }

        private void DrawVertices(Graphics g)
        {
            Pen p = new Pen(vertexColor);
            Brush b = new SolidBrush(vertexColor);
            foreach (Vertex v in vertices)
            {
                g.DrawEllipse(p, v.Position.X, v.Position.Y, radius, radius);
                g.FillEllipse(b, v.Position.X, v.Position.Y, radius, radius);
                DrawString(v.Name.ToString(), v.Position, g);
            }
        }

        private Vertex GetSelectedVertex(Point pClicked)
        {
            foreach (Vertex v in vertices)
            {
                var distance = GetDistance(v.Position, pClicked);
                if (distance <= radius)
                {
                    return v;
                }
            }
            return null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Очистить форму?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                btnSolve.Enabled = true;
                Graphics g = panel1.CreateGraphics();
                g.Clear(panel1.BackColor);
                Clear();
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (vertices.Count > 2)
            {
                if (graph.Count < vertices.Count - 1)
                {
                    MessageBox.Show("Missing Edges", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    btnSolve.Enabled = false;
                    IKruskal kruskal = new Kruskal();
                    int totalCost;
                    graphSolved = kruskal.Solve(graph, out totalCost);
                    solved = true;
                    panel1.Invalidate();
                    MessageBox.Show("Total Cost: " + totalCost.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawVertices(g);
            DrawEdges(g);
            g.Dispose();
        }

        private void panel1_MouseClick_1(object sender, MouseEventArgs e)
        {
            Point clicked = new Point(e.X - halfRadius, e.Y - halfRadius);
            if (Control.ModifierKeys == Keys.Control)//if Ctrl is pressed
            {
                if (!drawEdge)
                {
                    firstVertex = GetSelectedVertex(clicked);
                    drawEdge = true;
                }
                else
                {
                    secondVertex = GetSelectedVertex(clicked);
                    drawEdge = false;
                    if (firstVertex != null && secondVertex != null && firstVertex.Name != secondVertex.Name)
                    {
                        frmCost formCost = new frmCost();
                        formCost.ShowDialog();

                        Point stringPoint = GetStringPoint(firstVertex.Position, secondVertex.Position);
                        graph.Add(new Edge(firstVertex, secondVertex, formCost.cost, stringPoint));
                        panel1.Invalidate();
                    }
                }
            }
            else
            {
                vertices.Add(new Vertex(vertices.Count, clicked));
                panel1.Invalidate();
            }
        }

        private double GetDistance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }

        private Point GetStringPoint(Point start, Point end)
        {
            int X = (start.X + end.X) / 2;
            int Y = (start.Y + end.Y) / 2;
            return new Point(X, Y);
        }

        private void Clear()
        {
            vertices = new List<Vertex>();
            graph = new List<Edge>();
            solved = false;
            firstVertex = secondVertex = null;
        }
    }
}