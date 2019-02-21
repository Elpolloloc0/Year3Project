﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder {

	//Class responsible for finding the path between the start and the end nodes.

	private static PriorityQueue NodeQueue; 
	private static List<NodeController> resetList;

	//Returns the most optimal path based on the nodes calculated and resets all the node's info at the end.
	private static ArrayList calculatePath(NodeController node){
		ArrayList pathList = new ArrayList ();
		while (node != null) {
			pathList.Add (node);
			node = node.getNodeParent ();
		}
		pathList.Reverse ();

		foreach(NodeController modNode in resetList){
			modNode.resetNodeInfo();
		}

		NodeQueue.RemoveAll ();
		resetList.Clear ();


		return pathList;
	}

	//This function is responsible for finding the most optimal path using the A* search method.
	//Compares node distances between nodes as well as their children and neighbors.
	public static ArrayList getPath(GameObject currentNode, GameObject targetNode){
		
		NodeQueue = new PriorityQueue ();
		resetList = new List<NodeController> ();

		NodeController currentNodeInfo = currentNode.GetComponent<NodeController> ();
		currentNodeInfo.setTotalNodeDistance (0.0f);
		resetList.Add (currentNodeInfo);
		NodeController targetNodeInfo = targetNode.GetComponent<NodeController> ();

		for (int i = 0; i < currentNodeInfo.neighborNodes.Length; i++) {
			NodeController neighborInfo = currentNodeInfo.neighborNodes [i].GetComponent<NodeController> ();
			neighborInfo.setTotalNodeDistance (currentNodeInfo.neighborDistances [i]);
			neighborInfo.setNodeParent (currentNodeInfo);
			NodeQueue.Push (neighborInfo);
			resetList.Add (neighborInfo);
		}

		NodeController nodeInfo = null;

		while (NodeQueue.Length != 0) {

			nodeInfo = NodeQueue.GetFirstNode ();
	
			if (nodeInfo == targetNodeInfo) {
				return calculatePath (nodeInfo);
			}

			float currentTotalDistance = nodeInfo.getTotalNodeDistance ();

			for (int i = 0; i < nodeInfo.neighborNodes.Length; i++) {
				
				NodeController neighborInfo = nodeInfo.neighborNodes [i].GetComponent<NodeController> ();
				float modifiedTotalDistance = nodeInfo.neighborDistances [i] + currentTotalDistance;
				float originalTotalDistance = neighborInfo.getTotalNodeDistance ();

				if (modifiedTotalDistance < originalTotalDistance) {
					neighborInfo.setTotalNodeDistance (modifiedTotalDistance);
					neighborInfo.setNodeParent (nodeInfo);
					if (originalTotalDistance == float.PositiveInfinity) {
						NodeQueue.Push (neighborInfo);
						resetList.Add (neighborInfo);
					} else {
						NodeQueue.sortQueue ();
					}
				}

			}

			NodeQueue.Remove (nodeInfo);
		}

		//if (nodeInfo != targetNodeInfo) {
		Debug.LogError ("Goal Not Found");
		return null;
		//}

		//return calculatePath (nodeInfo);

	}

}
