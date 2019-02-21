﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourContext : BehaviourState {

	//Context specific to this instance of the enemy and its current behaviour and personal information.

	public CharacterController enemy;
	public EnemyController enemyPhysics;
	public Animator enemyAnimation;
	public GameObject[] pathNodes;
	public GameObject startNode = null;
	public NodeController startInfo = null;
	public GameObject endNode = null;

	public BehaviourContext(CharacterController enemy, EnemyController enemyPhy, Animator enemyAn){
		this.enemy = enemy;
		this.enemyPhysics = enemyPhy;
		this.enemyAnimation = enemyAn;
		pathNodes = GameObject.FindGameObjectsWithTag("PathNode");
	}
		
}
