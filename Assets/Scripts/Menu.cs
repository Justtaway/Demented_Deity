using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public bool showMenu; //Отображать ли меню 
    public int window; //Отображаемое окно 
    public float lifeTime = 5f; //Максимальное время отображения меню 
    private float curTime; //Текущие время отображения меню 
    private string ip = "127.0.0.1";
    private string port = "25000";
    private string MaxConnections = "2";
    public GUISkin skin;

    void Start()
    {
        showMenu = true;
        window = 1;
    }
        void OnServerInitialized()
            {
                window = 5;
        }
        void ConnectedToServer()
            {
                window = 6;
        }


    void Update()
    {
        if (showMenu == true) //Проверяем включно ли меню 
        {
            curTime += Time.deltaTime; //Если включено, Увеличиваем переменную curTime согласно пройденому времени 
        }
        if (curTime > lifeTime) //Если время дошло до максимальной точки 
        {
            showMenu = false; //Отключаем меню 
            window = 1;
            curTime = 0; //Сбрасываем таймер 
        }
        if (showMenu == false & Input.anyKeyDown) //Если меню выключено и нажата любая клавиша 
        {
            showMenu = true; //Включаем меню 
            window = 1;
        }
    }

    void OnGUI()
    {
        if (window == 1) //Если окно 1 
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 200), "Demented Deity"); //Создаем окно с меню 
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Одиночная игра"))
            {
                Application.LoadLevel(1);
            }
            //Загружаем уровень 1 
             if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 0, 180, 30), "Сетевая игра"))
             {
                   window = 2; //открываем окно настроек 
              }
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 40, 180, 30), "Об игре"))
            {
                window = 3; //Выводим информацию об Авторах игры 
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 80, 180, 30), "Выход"))
            {
                window = 4; //Вызываем окно выхода 
            }
        }


        //Далее все аналогично 
        //  if (window == 2)
        //  {
        //     GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 250), "Настройки");
        //    if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 40, 180, 30), "Игра"))
        //    {
        //    }
        //    if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 0, 180, 30), "Аудио"))
        //    {
        //  }
        //if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 40, 180, 30), "Видео"))
        //{
        //}
        //  if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 80, 180, 30), "Управление"))
        //  {
        //  }
        //  if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 120, 180, 30), "Назад"))
        //  {
        //      window = 1;
        // }
        // }
        if (window == 2) //Cоздание сервера
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 200), "Demented Deity");
            ip = GUI.TextField(new Rect(Screen.width / 2 - 100, 170, 110, 25), ip);
            port = GUI.TextField(new Rect(Screen.width / 2 + 15, 170, 55, 25), port);
            MaxConnections = GUI.TextField(new Rect(Screen.width / 2 + 75, 170, 25, 25), MaxConnections);
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 20, 180, 30), "Создать сервер"))
            {
                Network.InitializeServer(int.Parse(MaxConnections), int.Parse(port), true);
            }
            if (GUI.Button(new Rect(Screen.width / 2 -90, Screen.height / 2 + 20, 180, 30), "Подключиться"))
            {
                Network.Connect(ip, int.Parse(port));
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 60, 180, 30), "назад"))
            {
                window = 1;
            }

        }
        if (window == 5)
        {
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 200), "Demented Deity");
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 20, 180, 30), "Отключить сервер"))
            {
                Network.Disconnect();
                window = 1;
            }
             GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 20, 180, 30), "Подключено игроков:" + Network.connections.Length);
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 60, 180, 30), "Зайти в комнату"))
            {
                Application.LoadLevel(2);
                 GetComponent<NetworkView>().RPC﻿("LoadLevel", RPCMode.All);
            }
        }
        if (window == 6)
        {
            GUI.Button(new Rect(Screen.width / 2 - 100, 185, 180, 30), "Connected");
        }

            if (window == 3)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 80, 200, 200), "Разработчики");
                GUI.Label(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 50, 90, 150),
                    "Blood Sparrow TimmyDuck4 Justaway HisashiHiro xobgoblin F4t4l5w0rd EvgenCC");
                if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 80, 180, 30), "назад"))
                {
                    window = 1;
                }
            }

            if (window == 4)
            {
                GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 120), "Выход?");
                if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 - 20, 180, 30), "Да"))
                {
                    Application.Quit(); //Выход из игры 
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height / 2 + 20, 180, 30), "Нет"))
                {
                    window = 1;
                }
            }

            if (window == 0) //Если это окно то выключаем меню 
            {
                useGUILayout = false;

            }
        }
    
[RPC]
    void LoadLevel()
    {
        Application.LoadLevel(1);
		
		Application.LoadLevel (2);
	}	}
