using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MathCourse
{
    public class CGame
    {
        public int data;
        public CUIMan _CUIMan = new CUIMan();
        public CGame left;
        public CGame(int data)
        {
            this.data = data;
        }
    }
    public class CUIMan
    {
        public void Test()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AVLTree avlTree = new AVLTree();
            for (int i = 1; i <= 20; i++)
            {
                avlTree.Insert(i);
            }
            AVLTree.Traverse(avlTree._root);
            
            Console.ReadKey();
        }
    }

    public class Tools
    {
        public static string getMemory(object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.WeakTrackResurrection);
            IntPtr addr = GCHandle.ToIntPtr(handle);
            return $"0x{addr.ToString("X")}";
        }
        public static CGame Change(ref CGame node)
        {
            Console.WriteLine(Tools.getMemory(node));
            node = new CGame(3);
            return node;
        }
        //左移m位
        public static int ShiftLeft(int x, int m)
        {
            return x << m;
        }

        public static int ShiftRight(int x, int m)
        {
            return x >> m;
        }

        public static bool Prove(int k, Result result)
        {
            if (k == 1)
            {
                if ((Math.Pow(2, 1) - 1) == 1)
                {
                    result.wheatNum = 1;
                    result.wheatTotalNum = 1;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                bool proveOfPreViousOne = Prove(k - 1, result);
                result.wheatNum *= 2;
                result.wheatTotalNum += result.wheatNum;
                bool proveOfCurrentOne = false;
                if (result.wheatTotalNum == (Math.Pow(2, k) - 1))
                    proveOfCurrentOne = true;
                if (proveOfPreViousOne && proveOfCurrentOne)
                    return true;
                else
                    return false;
            }
        }

        public static int[] Sort(int[] array)
        {
            if (array.Length > 1)
            {
                int len = array.Length / 2;
                int[] arr_left = new int[len];
                Array.Copy(array, 0, arr_left, 0, len);
                int[] arr_right = new int[array.Length - len];
                Array.Copy(array, len, arr_right, 0, array.Length - len);
                int[] temp1 = Sort(arr_left);
                int[] temp2 = Sort(arr_right);
                int[] temp = new int[temp1.Length + temp2.Length];

                int i = 0, j = 0, index = 0;
                while (true)
                {
                    if (temp1[i] >= temp2[j])
                    {
                        temp[index] = temp2[j];
                        j++;
                    }
                    else
                    {
                        temp[index] = temp1[i];
                        i++;
                    }
                    index++;
                    //判断一下是否结束了
                    if (i >= temp1.Length)
                    {
                        for (int k = j; k < temp2.Length; k++)
                        {
                            temp[index] = temp2[k];
                            index++;
                        }
                        break;
                    }
                    else if (j >= temp2.Length)
                    {
                        for (int k = i; k < temp1.Length; k++)
                        {
                            temp[index] = temp1[k];
                            index++;
                        }
                        break;
                    }
                }

                return temp;
            }
            else
            {
                return array;
            }
        }
    }

    public class Result
    {
        public long wheatNum = 0;
        public long wheatTotalNum = 0;
    }
}
