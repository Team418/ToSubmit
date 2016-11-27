using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ClientMasterSc : MonoBehaviour {
    [Header("Spawn Stuff")]
    //Spawn stuff   
        public GameObject CurrentRefreshTarget;
        public float SpawnRequest = 0;
        public string SpawnRequest_EnemyName = "";
        public string[] ServerEnemyInfo = new string[2000];
    //Spawn stuff

    [Header("Networking Stuff")]
    //networking stuff
		public string ServerIpAdress = "";
		public string Authkey = "";
		public string[] ServerResponseArray = new string[100];
        int ServerRefreshSwitcher = 1;
        float ServerRefreshSwitcherTimer = 1F;
        public string ServerMessage = "";
	//networking stuff end
    public int[] Counter = new int[5];

    public string FnRun = "";

	// Use this for initialization
	void Start () {
        GameObject.Find("GameMaster").GetComponent<WaveSpawner>().SpawnServerEnemy();

		ServerIpAdress = PlayerPrefs.GetString("ServerIp");
		Authkey = PlayerPrefs.GetString("Authkey");

        //debug
        ServerIpAdress = "85.188.10.80";
        //Debug.Log(System.Net.NetworkInformation.IPAddressInformation());
	}
	
	// Update is called once per frame
	void Update () {
        switch (FnRun) {
            case "UseServerEnemyInfo":
                Counter[3] = 0;
                for (;;) {
                    //try {
                        CurrentRefreshTarget = GameObject.Find(ServerEnemyInfo[Counter[3]]);
                        Debug.Log("if " + ServerEnemyInfo[Counter[3]] + " =  null");
                        if (!(ServerEnemyInfo[Counter[3]] == "")) {
                            Debug.Log("changed!");
                            if (GameObject.Find("enemy_" + Counter[3]) != null) {
                                CurrentRefreshTarget.GetComponent<Enemy>().routeNumber = Convert.ToInt32(ServerEnemyInfo[Counter[3] + 1]);
                                CurrentRefreshTarget.GetComponent<Enemy>().WayPointIndex = Convert.ToInt32(ServerEnemyInfo[Counter[3] + 2]);
                                CurrentRefreshTarget.GetComponent<Transform>().position = new Vector3( Convert.ToSingle(ServerEnemyInfo[Counter[3] + 3]) , 0F , Convert.ToSingle(ServerResponseArray[Counter[3] + 4]));
                            } else {
                                Debug.Log("create new object");
                                GameObject.Find("GameMaster").GetComponent<WaveSpawner>().SpawnServerEnemy();
                            }
                        } else {
                            break;
                        }
                        if (Counter[3] > 250) {
                            break;
                        }
                    //} catch {
                    //}
                    if (Counter[3] >= 1000) {
                        break;
                    }
                    Counter[3]+=5;
                }
                FnRun = "";
            break;
        }
        /*

        */
        if (ServerRefreshSwitcherTimer <= 0) {
            switch (ServerRefreshSwitcher) {
                case 1:
                    try {
                        // GameObject.FindWithTag("Enemy").GetComponent<Enemy>().SyncFailed(); //set SyncFailed to all Enemies
                    } catch {

                    }
					ServerSendData("EI/");
                    Debug.Log("blah!");
                    //RefreshCurrentEnemies();   
                break;
				default:
					ServerRefreshSwitcher = 1;
				break;
            }
            ServerRefreshSwitcherTimer = 0.5F;
        } else {
            ServerRefreshSwitcherTimer -= Time.deltaTime;
        }
	}

	public void ServerSendData(string ServerDataSend) {
        ServerRefreshSwitcherTimer = 0.75F;
		ServerMessage = ServerDataSend;
		Thread ServerThread = new Thread(server_connect);
		ServerThread.Start();
	}

    void server_connect() {
        //try {
            TcpClient client = new TcpClient(ServerIpAdress, 8027);
            byte[] ServerGetData = System.Text.Encoding.ASCII.GetBytes(ServerMessage);
            NetworkStream stream = client.GetStream();
            stream.Write(ServerGetData, 0, ServerGetData.Length);
            Debug.Log("Sent: " + ServerMessage);
            ServerGetData = new byte[256];
            string ServerResponseData = string.Empty;
            int bytes = stream.Read(ServerGetData, 0, ServerGetData.Length);
            ServerResponseData = System.Text.Encoding.ASCII.GetString(ServerGetData, 0, bytes);
            Debug.Log("Received: " + ServerResponseData);
            stream.Close();
            client.Close();
            ServerResponseArray = ServerResponseData.Split(new char[] { '/' });
            switch (ServerResponseArray[0]) {
                case "EI": //enemy info
                    Counter[4] = 1;
                    for (;;) {
                        ServerEnemyInfo[Counter[4]] = ServerResponseArray[Counter[4]];
                        if (ServerEnemyInfo[Counter[4]] == "") {
                            break;
                        }
                        if (Counter[4] > 500) {
                            break;
                        }
                    }
                    FnStart("UseServerEnemyInfo");
                break;
            }
        //} catch {
        //}
    }

    void RefreshCurrentEnemies() {
        
    }

    public void SpawnClientEnemy(string SpawnEnemyName) {
        //GameObject.Find("GameMaster").GetComponent<WaveSpawner>().SpawnServerEnemyByName(SpawnEnemyName);
    }

    public void FnStart(string FnSelect) {
        FnRun = FnSelect;
    }
}