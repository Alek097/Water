namespace Pirates.Water
{
	#region Using
	using UnityEngine;
	using System.Collections.Generic;
	#endregion
	public static class Water
	{
		public static Plane Generate(int width, int height, uint gap)
		{
			if (gap == 0u)
			{
				gap = 1;
			}

			Vector3[] vertices = new Vector3[(width * gap) * (height * gap)];



			float x = 0.0f;
			for (int i = 0; i < vertices.Length; x += 1.0f / gap)
			{
				for (float z = 0.0f; i < vertices.Length && z < height; z += 1.0f / gap, i++)
				{
					vertices[i] = new Vector3(x, 0, z);
				}
			}

			int[][] verticesIndex = new int[width * gap][];

			for (int i = 0, index = 0; i < verticesIndex.Length; i++)
			{
				verticesIndex[i] = new int[height * gap];
				for (int j = 0; j < verticesIndex[i].Length; j++, index++)
				{
					verticesIndex[i][j] = index;
				}
			}

			List<int> tringles = new List<int>();

			for (int i = 0; i + 1 < verticesIndex.Length; i++)
			{
				for (int j = 0; j + 1 < verticesIndex[i].Length; j++)
				{
					tringles.AddRange(new int[]
					{
						verticesIndex[i][j],
						verticesIndex[i][j + 1],
						verticesIndex[i + 1][j + 1],

						verticesIndex[i][j],
						verticesIndex[i + 1][j + 1],
						verticesIndex[i + 1][j]
					});
				}
			}
			return new Plane
			{
				Vertices = vertices,
				Triangles = tringles.ToArray()
			};
		}
	}
}
