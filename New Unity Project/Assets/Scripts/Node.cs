using UnityEngine;
using System.Collections;

public class Node {

	public bool BadNode;    

	//Grid coordinates
	public int X;
	public int Y;

	//world position
	public Vector2 Position;

	//our 8 connection points
	public NodeConnection Top;
	public NodeConnection Left;
	public NodeConnection Bottom;
	public NodeConnection Right;
	public NodeConnection TopLeft;
	public NodeConnection TopRight;
	public NodeConnection BottomLeft;
	public NodeConnection BottomRight;
	
	GameObject Punt;

	public Node(float x, float y, Vector2 position, Grid grid)
	{
		Initialize(x, y, position, grid);
	}
	
	public void Initialize(float x, float y, Vector2 position, Grid grid)
	{
		X = (int)x;
		Y = (int)y;
		
		Position = position;

		//check if coords inside our grid area
		if (Position.x < grid.transform.position.x || -Position.y < grid.transform.position.y) {
			DisableConnections ();
			BadNode = true;
		}
		if (Position.x > grid.transform.position.x + grid.transform.localScale.x)
		{
			DisableConnections ();
			BadNode = true;
		}
		if (-Position.y > grid.transform.position.y + grid.transform.localScale.y) {
			DisableConnections ();
			BadNode = true;
		}	

		//Draw Node on screen for debugging purposes
		Punt = GameObject.Instantiate (Resources.Load ("Node")) as GameObject;
		Punt.transform.SetParent (Grid.grid.transform);
		Punt.transform.position = Position;
		Punt.GetComponent<Punt> ().X = X;
		Punt.GetComponent<Punt> ().Y = Y;
	}

	public void SetColor(Color color)
	{
		Punt.transform.GetComponent<SpriteRenderer> ().color = color;
	}

	//Cull nodes if they don't have enough valid connection points (3)
	public void CheckConnectionsPass1(Grid grid)
	{
		if (!BadNode) {

			int clearCount = 0;

			if (Top != null && Top.Valid)
				clearCount++;
			if (Bottom != null && Bottom.Valid)
				clearCount++;
			if (Left != null && Left.Valid)
				clearCount++;
			if (Right != null && Right.Valid)
				clearCount++;
			if (TopLeft != null && TopLeft.Valid)
				clearCount++;
			if (TopRight != null && TopRight.Valid)
				clearCount++;
			if (BottomLeft != null && BottomLeft.Valid)
				clearCount++;
			if (BottomRight != null && BottomRight.Valid)
				clearCount++;

			//If not at least 3 valid connection points - disable node
			if (clearCount < 3) {
				BadNode = true;
				DisableConnections ();
			}
		}		
		
		if (!BadNode)
			SetColor (Color.yellow);
		else
			SetColor (Color.red);
	}

	//Remove connections that connect to bad nodes
	public void CheckConnectionsPass2()
	{
		if (Top != null && Top.Node != null && Top.Node.BadNode)
			Top.Valid = false;
		if (Bottom != null && Bottom.Node != null && Bottom.Node.BadNode)
			Bottom.Valid = false;
		if (Left != null && Left.Node != null && Left.Node.BadNode)
			Left.Valid = false;
		if (Right != null && Right.Node != null && Right.Node.BadNode)
			Right.Valid = false;
		if (TopLeft != null && TopLeft.Node != null && TopLeft.Node.BadNode)
			TopLeft.Valid = false;
		if (TopRight != null && TopRight.Node != null && TopRight.Node.BadNode)
			TopRight.Valid = false;
		if (BottomLeft != null && BottomLeft.Node != null && BottomLeft.Node.BadNode)
			BottomLeft.Valid = false;
		if (BottomRight != null && BottomRight.Node != null && BottomRight.Node.BadNode)
			BottomRight.Valid = false;
	}

	//Disable all connections going from this this
	public void DisableConnections()
	{
		if (Top != null) {
			Top.Valid = false;
		}
		if (Bottom != null) {
			Bottom.Valid = false;
		}
		if (Left != null) {
			Left.Valid = false;
		}
		if (Right != null) {
			Right.Valid = false;
		}
		if (BottomLeft != null) {
			BottomLeft.Valid = false;
		}
		if (BottomRight != null) {
			BottomRight.Valid = false;
		}
		if (TopRight != null) {
			TopRight.Valid = false;
		}
		if (TopLeft != null) {
			TopLeft.Valid = false;
		}
	}

	//debug draw for connection lines
	public void DrawConnections()
	{
		if (Top != null) Top.DrawLine ();
		if (Bottom != null)Bottom.DrawLine ();
		if (Left != null)Left.DrawLine ();
		if (Right != null)Right.DrawLine ();
		if (BottomLeft != null)BottomLeft.DrawLine ();
		if (BottomRight != null)BottomRight.DrawLine ();
		if (TopRight != null)TopRight.DrawLine ();
		if (TopLeft != null)TopLeft.DrawLine ();
	}


	//Raycast in all 8 directions to determine valid routes
	public void InitializeConnections(Grid grid)
	{
		bool valid = true;
		RaycastHit2D hit;
		float diagonalDistance = Mathf.Sqrt (Mathf.Pow (Grid.UnitSize/2f, 2) + Mathf.Pow (Grid.UnitSize/2f, 2));

		if (X > 1)
		{	
			//Left
			valid = true;
			hit = Physics2D.Raycast(Position, new Vector2(-1, 0), Grid.UnitSize);
			if (hit.collider != null && hit.collider.tag == "wall")
			{
				valid = false;
			}
			Left = new NodeConnection(this, grid.Nodes[X - 2, Y], valid);

			//TopLeft
			if (Y > 0)
			{
				valid = true;
				hit = Physics2D.Raycast(Position, new Vector2(-1, 1), diagonalDistance);
				if (hit.collider != null && hit.collider.tag == "wall")
				{
					valid = false;
				}
				TopLeft = new NodeConnection(this, grid.Nodes[X - 1, Y - 1], valid);
			}

			//BottomLeft
			if (Y < grid.Height - 1)
			{
				valid = true;
				hit = Physics2D.Raycast(Position, new Vector2(-1, -1), diagonalDistance);
				if (hit.collider != null && hit.collider.tag == "wall")
				{
					valid = false;
				}			
				BottomLeft = new NodeConnection(this,grid.Nodes[X - 1, Y + 1], valid);
			}
		}


		if (X < grid.Width - 2)
		{
			valid = true;
			hit = Physics2D.Raycast(Position, new Vector2(1, 0), Grid.UnitSize);
			if (hit.collider != null && hit.collider.tag == "wall")
			{
				valid = false;
			}
			Right = new NodeConnection(this,grid.Nodes[X + 2, Y], valid);

			//TopRight
			if (Y > 0)
			{
				valid = true;
				hit = Physics2D.Raycast(Position, new Vector2(1, 1), diagonalDistance);
				if (hit.collider != null && hit.collider.tag == "wall")
				{
					valid = false;
				}
				TopRight = new NodeConnection(this,grid.Nodes[X + 1, Y - 1], valid);
			}
			
			//BottomRight
			if (Y < grid.Height - 1)
			{
				valid = true;
				hit = Physics2D.Raycast(Position, new Vector2(1, -1), diagonalDistance);
				if (hit.collider != null && hit.collider.tag == "wall")
				{
					valid = false;
				}
				BottomRight = new NodeConnection(this,grid.Nodes[X + 1, Y + 1], valid);
			}

		}
	
		if (Y - 1 > 0)
		{
			valid = true;
			hit = Physics2D.Raycast(Position, new Vector2(0, 1), Grid.UnitSize);
			if (hit.collider != null && hit.collider.tag == "wall")
			{
				valid = false;
			}			
			Top = new NodeConnection(this,grid.Nodes[X, Y - 2], valid);
		}


		if (Y < grid.Height - 2)
		{
			valid = true;
			hit = Physics2D.Raycast(Position, new Vector2(0, -1), Grid.UnitSize);
			if (hit.collider != null && hit.collider.tag == "wall")
			{
				valid = false;
			}						
			Bottom = new NodeConnection(this,grid.Nodes[X, Y + 2], valid);
		}
	}


}

