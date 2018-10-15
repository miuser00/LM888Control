using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM888Control
{
    internal class Snake
    {
        private int[] _dots;

        private int _tag;

        public int Tag
        {
            get
            {
                return this._tag;
            }
            set
            {
                this._tag = value;
            }
        }

        public int[] GetDots()
        {
            return this._dots;
        }

        public Snake(int Length)
        {
            this._dots = new int[Length];
            System.Array.Clear(this._dots, 0, Length);
        }

        public void SetHead(int Index)
        {
            this._dots[0] = Index;
        }

        public void Follow()
        {
            for (int i = this._dots.Length - 1; i > 0; i--)
            {
                this._dots[i] = this._dots[i - 1];
            }
        }

        public int Head_X()
        {
            int result = 0;
            int num = 0;
            int num2 = 0;
            Cube.Index2xyz(this._dots[0], ref result, ref num, ref num2);
            return result;
        }

        public int Head_Y()
        {
            int num = 0;
            int result = 0;
            int num2 = 0;
            Cube.Index2xyz(this._dots[0], ref num, ref result, ref num2);
            return result;
        }

        public int Head_Z()
        {
            int num = 0;
            int num2 = 0;
            int result = 0;
            Cube.Index2xyz(this._dots[0], ref num, ref num2, ref result);
            return result;
        }

        public void Move(int Direction)
        {
            if (Direction > 5)
            {
                return;
            }
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            Cube.Index2xyz(this._dots[0], ref num, ref num2, ref num3);
            this.Follow();
            switch (Direction)
            {
                case 0:
                    num3++;
                    break;
                case 1:
                    num3--;
                    break;
                case 2:
                    num2++;
                    break;
                case 3:
                    num2--;
                    break;
                case 4:
                    num++;
                    break;
                case 5:
                    num--;
                    break;
            }
            this._dots[0] = Cube.Index(num, num2, num3);
        }
    }
}