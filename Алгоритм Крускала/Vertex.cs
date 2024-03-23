using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Алгоритм_Крускала
{
    public class Vertex
    {
        #region Members

        private int name;

        #endregion

        #region Public Properties

        public int Name
        {
            get
            {
                return name;
            }
        }

        public int Rank { get; set; }

        public Vertex Root { get; set; }

        public Point Position { get; set; }

        #endregion

        #region Constructor

        public Vertex(int name, Point position)
        {
            this.name = name;
            this.Rank = 0;
            this.Root = this;
            this.Position = position;
        }

        #endregion

        #region Methods

        internal Vertex GetRoot()
        {
            if (this.Root != this)
            {
                this.Root = this.Root.GetRoot();
            }

            return this.Root;
        }

        internal static void Join(Vertex root1, Vertex root2)
        {
            if (root2.Rank < root1.Rank)
            {
                root2.Root = root1;
            }
            else 
            {
                root1.Root = root2;
                if (root1.Rank == root2.Rank)
                {
                    root2.Rank++;
                }
            }
        }

        #endregion
    }
}
