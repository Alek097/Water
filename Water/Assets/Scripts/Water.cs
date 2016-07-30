using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class Water : MonoBehaviour
{

	private Mesh mesh;
	private Dictionary<string, Vector3> moveTo = new Dictionary<string, Vector3>();

	public float Speed;

	void Start()
	{
		this.mesh = GetComponent<MeshFilter>().mesh;

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

			if(target.y == vertices[i].y)
			{
				Debug.Log(string.Format("old y:{0}; new y:{1}", target.y, target.y*-1));
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
