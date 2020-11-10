using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransOS.Plugin.Helper;

namespace TransOS.Plugin.Ridge
{
    public partial class Form1 : Form
    {
        readonly IContext Os;

        public Form1(IContext Os, IRidgeObject RidgeRoot)
        {
            InitializeComponent();

            this.Os = Os;

            if (RidgeRoot == null)
                RidgeRoot = this.Os.Ridge.Root;

            this.Text = RidgeRoot.Text;

            this.Add(this.treeView1.Nodes, RidgeRoot);

            this.treeView1.Nodes.Cast<TreeNode>().ВыполнитьДляКаждого(x => x.Expand());
        }

        /// <summary>
        /// Визуально добавить элемент
        /// </summary>
        /// <param name="nodeCollection"></param>
        /// <param name="ridgeObject"></param>
        void Add(TreeNodeCollection nodeCollection, IRidgeObject ridgeObject)
        {
            if (ridgeObject != null)
            {
                // добавить объект
                var новыйНод = nodeCollection.Add(ridgeObject.Text);
                новыйНод.Tag = ridgeObject;

                // если редактирование текста
                ridgeObject.BeginedUserEdit += RidgeObject_BeginedUserEdit;

                // если удаление текста
                ridgeObject.BeginedUserRemove += RidgeObject_BeginedUserRemove;

                // Icon
                if (ridgeObject.Icon != null)
                {
                    this.imageList1.Images.Add(ridgeObject.Id, ridgeObject.Icon);
                    новыйНод.ImageKey = ridgeObject.Id;
                }

                this.AddChilds(новыйНод, ridgeObject, false);
            }
        }

        private void RidgeObject_BeginedUserRemove(IRidgeObject ridgeObject)
        {
            var Node = this.FindNode(ridgeObject);
            if(Node != null)
            {
                if(MessageBox.Show($"Remove object?\r\n{ridgeObject}", "Deleting ridge object", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (ridgeObject.Remove())
                    {
                        //Node.Remove();
                    }
                    else
                        MessageBox.Show($"Object not removed!\r\n{ridgeObject}", "Deleting ridge object", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                }
            }
        }

        private void RidgeObject_BeginedUserEdit(IRidgeObject ridgeObject)
        {
            var Node = this.FindNode(ridgeObject);
            Node?.BeginEdit();
        }

        private void AddChilds(TreeNode новыйНод, IRidgeObject ridgeObject, bool Expanding)
        {
            if (ridgeObject.Child != null)
            {
                // events
                ridgeObject.Child.Added += Child_Added;
                ridgeObject.Child.Removed += Child_Removed;

                // добавить детей                    
                новыйНод.Nodes.Clear();
                if (Expanding && ridgeObject.DynamicChilds)
                    ridgeObject.Child.ReloadElements();
                var ChildArray = ridgeObject.Child.ToArray().ToList();

                // если дети динамические
                if (!Expanding)
                {
                    if (ridgeObject.DynamicChilds)
                    {
                        if (ChildArray.Count == 0)
                        {
                            // добавляем заглушку, для возможного раскрытия элемента
                            ChildArray.Add(new LoadingObject());
                        }
                    }
                }
                else
                {
                    // Expanding
                    if (ridgeObject.DynamicChilds)
                    {
                        if (ChildArray.Count == 0)
                        {
                            // добавляем заглушку, для возможного раскрытия элемента
                            ChildArray.Add(new LoadingObject
                            {
                                Loading = false
                            });
                        }
                    }
                }

                ChildArray.ВыполнитьДляКаждого(x => this.Add(новыйНод.Nodes, x));
            }
        }

        private void Child_Added(IRidgeObject obj)
        {
            var ParentNode = this.FindNode(obj.Parent);
            this.Add(ParentNode.Nodes, obj);

            // если подительский нод выделен - раскрываем 
            if (this.treeView1.SelectedNode == ParentNode)
                ParentNode.Expand();
        }

        private void Child_Removed(IRidgeObject obj)
        {
            var removedNode = this.FindNode(obj);
            removedNode.Remove();
        }

        private TreeNode FindNode(IRidgeObject obj, TreeNodeCollection nodes = null)
        {
            if (nodes == null)
                nodes = this.treeView1.Nodes;

            foreach (TreeNode Node in nodes)
            {
                if (Node.Tag == obj)
                    return Node;

                var result = this.FindNode(obj, Node.Nodes);

                if (result != null)
                    return result;
            }

            return null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            var ridgeObject = e.Node?.Tag as IRidgeObject;
            if (ridgeObject != null)
            {
                var view = ridgeObject.View;
                if (view != null)
                {
                    // устанавливаем стиль контрола
                    //this.Ось.Gi.Gui.VisualStyle.SetToControl(view);

                    this.splitContainer1.Panel2.Controls.Add(view);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            this.contextMenuStrip1.Items.Clear();

            var SelectedRo = this.treeView1.SelectedNode?.Tag as IRidgeObject;
            if (SelectedRo != null)
            {
                if (SelectedRo.RightMenu != null)
                {
                    foreach (var menuItemRo in SelectedRo.RightMenu)
                    {
                        var item = this.contextMenuStrip1.Items.Add(menuItemRo.Text);
                        item.Tag = menuItemRo;

                        if (menuItemRo.Icon != null)
                            item.Image = menuItemRo.Icon;

                        item.Click += Item_Click;
                    }
                }

                if (this.contextMenuStrip1.Items.Count > 0)
                {
                    e.Cancel = false;
                    return;
                }
            }

            e.Cancel = true;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var menuItem = (ToolStripMenuItem)sender;
            var SelectedRo = menuItem?.Tag as IRidgeObject;
            SelectedRo?.Run();
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var ridgeObject = e.Node?.Tag as IRidgeObject;

            if (ridgeObject != null)
            {
                ridgeObject.Expanding();

                if (ridgeObject.DynamicChilds)
                {
                    // обновляем подноды
                    this.AddChilds(e.Node, ridgeObject, true);
                }
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var ridgeObject = e.Node?.Tag as IRidgeObject;

            if (ridgeObject != null && ridgeObject.CanEditText && !string.IsNullOrWhiteSpace(e.Label))
            {
                ridgeObject.Text = e.Label;
            }
            else
                e.CancelEdit = true;
        }
    }
}
