using System;
using System.Collections.Generic;
using System.Text;

namespace MathCourse
{
    class BST
    {

    }

    public class AvlNode
    {
        public int data;
        public AvlNode lchild;
        public AvlNode rchild;
        public AvlNode parent;
        public int height = 0;
        public bool IsLChild(AvlNode node)
        {
            if (node != null & node == lchild) return true;
            return false;
        }
        public bool IsRChild(AvlNode node)
        {
            if (node != null & node == rchild) return true;
            return false;
        }
        public void SetParent(AvlNode node)
        {
            parent = node;
        }

        public AvlNode(int data, AvlNode parent)
        {
            this.data = data;
            this.parent = parent;
        }
    }

    // 在这里定义各种操作
    public class AVLTree
    {
        public AvlNode _root = new AvlNode(0, null);
        //当前操作热点 初始化一个节点 这个节点现在并没有存入树中
        public AvlNode _hot = null;
        //树的规模
        public int _size = 1;
        //计算节点的高度
        public static int height(AvlNode T)
        {
            if (T == null) { return -1; }
            else { return T.height; }
        }
        //左左型 右旋转
        public static AvlNode R_Rotate(AvlNode node)
        {
            AvlNode node2 = node.lchild;
            node.lchild = node2.rchild;
            node2.rchild = node;
            //更新父母节点的左右孩子
            AvlNode parent = node.parent;
            if (parent != null)
            {
                if (parent.IsLChild(node))
                {
                    parent.lchild = node2;
                }
                else
                {
                    parent.rchild = node2;
                }
            }
            //更新自身的父母节点
            node2.SetParent(parent);
            node.SetParent(node2);
            //更新高度
            UpdateHeight(node);
            UpdateHeight(node2);

            return node2;
        }

        //右右型 左旋转
        public static AvlNode L_Rotate(AvlNode node)
        {
            AvlNode node2 = node.rchild;
            node.rchild = node2.lchild;
            node2.lchild = node;
            //更新父母节点的左右孩子
            AvlNode parent = node.parent;
            if (parent != null)
            {
                if (parent.IsLChild(node))
                {
                    parent.lchild = node2;
                }
                else
                {
                    parent.rchild = node2;
                }
            }
            //更新自身的父母节点
            node2.SetParent(parent);
            node.SetParent(node2);
            //更新高度
            UpdateHeight(node);
            UpdateHeight(node2);

            return node2;
        }

        //左右型 先左转 再右转
        public static AvlNode L_R_Rotate(AvlNode node)
        {
            node.lchild = L_Rotate(node.lchild);
            return R_Rotate(node);
        }

        //右左型 先右转 再左转
        public static AvlNode R_L_Rotate(AvlNode node)
        {
            node.rchild = R_Rotate(node.rchild); 
            return L_Rotate(node);
        }

        public AvlNode Insert(int data)
        {
            AvlNode node = Search(data);
            AvlNode res = null;
            //查找失败的时候才插入
            if (node == null)
            {
                node = new AvlNode(data, _hot);
                if (data < _hot.data)
                    _hot.lchild = node;
                else
                    _hot.rchild = node;
                _size++;

                //从下往上找 一旦发现有一个祖先失衡 必将引起多个祖先失衡
                bool isBalanced = true;
                while (node.parent != null)
                {
                    node = node.parent;
                    if (height(node.lchild) - height(node.rchild) == 2)
                    {
                        isBalanced = false;
                        //左左型
                        if (height(node.lchild) - height(node.lchild.lchild) == 1)
                        {
                            res = R_Rotate(node);
                        }
                        //左右型
                        else 
                        {
                            res = L_R_Rotate(node);
                        }
                    }
                    else if(height(node.lchild) - height(node.rchild) == -2) 
                    {
                        isBalanced = false;
                        //右右型
                        if (height(node.rchild) - height(node.rchild.rchild) == 1)
                        {
                            res = L_Rotate(node);
                        }
                        //右左型
                        else
                        {
                            res = R_L_Rotate(node);
                        }
                    }
                    if (!isBalanced)
                        break;
                    else
                        //平衡性虽然不变 但是高度可能改变
                        UpdateHeight(node);
                }

            }

            if(node != null)
            {
                _root = node;
                node = node.parent;
            }

            return node;
        }

        public AvlNode Search(int data)
        {
            //记录下当前操作的热点 方便到时候插入时候用
            return SearchIn(_root, data, ref _hot);
        }
        public static AvlNode SearchIn(AvlNode node, int data, ref AvlNode hot)
        {
            if (node == null || (data == node.data)) return node;
            hot = node;
            return SearchIn((data < node.data) ? node.lchild : node.rchild, data, ref hot);
        }

        public static void UpdateHeight(AvlNode node)
        {
            node.height = Math.Max(height(node.lchild), height(node.rchild)) + 1;
        }

        public static void UpdateHeightAbove(AvlNode node)
        {
            while (node != null)
            {
                UpdateHeight(node);
                node = node.parent;
            }
        }

        public static void GoAlongLeftBranch(AvlNode root, Stack<AvlNode> treeStack)
        {
            // 反复地入栈，沿着左分支深入
            while (root != null)
            {
                treeStack.Push(root);
                root = root.lchild;
            }
        }

        public static void Traverse(AvlNode root)
        {
            Stack<AvlNode> treeStack = new Stack<AvlNode>();
            while (true)
            {
                GoAlongLeftBranch(root, treeStack);
                if (treeStack.Count <= 0) break;
                root = treeStack.Pop();
                Console.Write(root.data + " ");
                root = root.rchild;
            }
        }
    }

}
