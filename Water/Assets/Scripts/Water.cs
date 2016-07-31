using UnityEngine;
using System.Collections.Generic;
using System;

public class Water : MonoBehaviour
{

	private Mesh mesh;
	private Dictionary<string, Vector3> moveTo = new Dictionary<string, Vector3>();

	public float Speed;
	public int Width = 5;
	public int Height = 5;
	public uint Gap = 1;


	void Start()
	{
		this.mesh = GetComponent<MeshFilter>().mesh;

		this.mesh.Clear();


		Pirates.Water.Plane plane = Pirates.Water.Water.Generate(this.Width, this.Height, this.Gap);

		foreach (Vector3 item in plane.Vertices)
		{
			Debug.Log(item);
		}

		this.mesh.SetVertices(new List<Vector3>(plane.Vertices));

		this.mesh.triangles = plane.Triangles;

		//this.mesh.vertices = new Vector3[] { new Vector3(0, 0, 1), new Vector3(1, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, 0) };
		//this.mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };


		Debug.Log("Plane contructed");

		for (int i = 0; i < this.mesh.vertices.Length; i++)
		{
			Vector3 target = new Vector3(
			this.mesh.vertices[i].x,
			(float)Math.Sin(this.mesh.vertices[i].x),
			this.mesh.vertices[i].z);

			this.moveTo.Add(i.ToString(), target);
		}
	}


	void Update()
	{

		Vector3[] vertices = this.mesh.vertices;
		int[] tringles = this.mesh.triangles;

		for (int i = 0; i < vertices.Length; i++)
		{
			string key = i.ToString();
			Vector3 target = this.moveTo[key];

			if (target.y == vertices[i].y)
			{
				Debug.Log(string.Format("old y:{0}; new y:{1}", target.y, target.y * -1));
				target.y *= -1;
				this.moveTo[key] = target;
			}

			vertices[i] = Vector3.MoveTowards(
				vertices[i],
				target,
				Time.deltaTime * this.Speed
				);
		}

		this.mesh.vertices = vertices;
		this.mesh.triangles = tringles;
	}
}
