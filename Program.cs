namespace SnapshotArray
{
	internal class Program
	{
		public class SnapshotArray
		{
			private readonly List<int[]>[] histories;
			private int snapId = 0;
			public SnapshotArray(int length)
			{
				histories = new List<int[]>[length];
				for (int i = 0; i < length; ++i)
				{
					histories[i] = new List<int[]>() { new int[] { -1, 0 } };
				}
			}

			public void Set(int index, int val)
			{
				histories[index].Add(new int[] { snapId, val });
			}

			public int Snap()
			{
				++snapId;
				return snapId - 1;
			}

			public int Get(int index, int snap_id)
			{
				int left = 0;
				int right = histories[index].Count - 1;
				int position = -1;
				while(left <= right)
				{
					int middle = left + (right - left) / 2;
					if (histories[index][middle][0] <= snap_id)
					{
						position = middle;
						left = middle + 1;
					}
					else
					{
						right = middle - 1;
					}
				}
				return histories[index][position][1];
			}
		}

		static void Main(string[] args)
		{
			SnapshotArray snapshotArr = new(3); // set the length to be 3
			snapshotArr.Set(0, 5);  // Set array[0] = 5
            Console.WriteLine(snapshotArr.Snap());  // Take a snapshot, return snap_id = 0
            snapshotArr.Set(0, 6);
            Console.WriteLine(snapshotArr.Get(0, 0)); ;  // Get the value of array[0] with snap_id = 0, return 5
		}
	}
}