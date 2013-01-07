using System;
using System.Collections.Generic;
using System.Linq;
using Task1650.World;

namespace Task1650
{
	class TreeSegment<T>
		where T: class, IChange 
	{
		private readonly Int64[] _tree;
		private readonly T[] _elements;
		private readonly int _size;
		public TreeSegment(T[] elements)
		{
			_tree = new Int64[elements.Length << 2];
			_size = elements.Length;
			_elements = elements;
			Build(elements, 1, 0, elements.Length - 1);
			foreach (T t in _elements)
			{
				t.Update += Update;
			}
		}
		public static int GetLeftChild(int v)
		{
			return v<<1;
		}
		public static int GetRightChild(int v)
		{
			return (v<<1) + 1;
		}


		private void Build (T[] elements, int v, int tl, int tr) 
		{
			if (tl == tr)
				_tree[v] = elements[tl].GetValue;
			else 
			{
				int tm = (tl + tr) >>1;
				int leftChild = GetLeftChild(v);
				int rightChild = GetRightChild(v);
				Build(elements, leftChild, tl, tm);
				Build(elements, rightChild, tm + 1, tr);
				_tree[v] = (_tree[leftChild] > _tree[rightChild]) ? _tree[leftChild] : _tree[rightChild];
			}
		}
		public void Update(int pos, Int64 newVal)
		{
			var possitionForUpdate = GetIndexForUpdate(pos).ToArray();
			_tree[possitionForUpdate[possitionForUpdate.Length - 1]] = newVal;
			for (int i = possitionForUpdate.Length - 2; i >= 0; i--)
			{
				var vl = _tree[GetLeftChild(possitionForUpdate[i])];
				var vr = _tree[GetRightChild(possitionForUpdate[i])];
				_tree[possitionForUpdate[i]] = (vl>vr)?vl:vr;
			}
		}
		public T GetMaxElelements()
		{
			var vl = 0;
			var vr = _size - 1;
			var currentPos = 1;
			while (vl < vr)
			{
				var middle = (vl + vr) >> 1;
				int leftChild = GetLeftChild(currentPos);
				int rightChild = GetRightChild(currentPos);
				if (_tree[leftChild] == _tree[rightChild])
				{
					return null;
				}
				{
					if (_tree[leftChild] >= _tree[rightChild])
					{
						vr = middle;
						currentPos = leftChild;
					}
					else
					{
						vl = middle + 1;
						currentPos = rightChild;
					}
				}
			}
			return _elements[vl];
		}

		private IEnumerable<int> GetIndexForUpdate(int pos)
		{
			var vl = 0;
			var vr = _size - 1;
			var currentPos = 1;
			yield return 1;
			while (vl < vr)
			{
				var middle = (vl + vr) >> 1;
				if (pos <= middle)
				{
					vr = middle;
					currentPos = GetLeftChild(currentPos);
				}
				else
				{
					vl = middle + 1;
					currentPos = GetRightChild(currentPos);
				}
				yield return currentPos;
			}
		}
	}
}