﻿using ExpressoBits.Console.Utils;
using UnityEngine;

namespace ExpressoBits.Console
{
    [AddComponentMenu(menuName: "Console/History")]
    [RequireComponent(typeof(Commander))]
    public class History : MonoBehaviour
    {
        public KeyCode upKeyCode = KeyCode.UpArrow;
        public KeyCode downKeyCode = KeyCode.DownArrow;
        [Range(0, 256)] public int maxHistoryRegistry;
        public CircularBuffer<string> history;

        private int m_ActualIndex;
        private const int noValue = -1;
        private const string saveHistoryKey = "br.com.ExpressoBits.Console.History";
        private Commander m_Commander;

        private void Awake()
        {
            m_Commander = GetComponent<Commander>();
            history = new CircularBuffer<string>(maxHistoryRegistry);
            LoadHistory();
        }

        private void LoadHistory()
        {
            if (!PlayerPrefs.HasKey(saveHistoryKey)) return;
            var raw = PlayerPrefs.GetString(saveHistoryKey);
            var commands = raw.Split('\n');
            foreach (var command in commands)
            {
                history.Add(command);
            }
        }

        private void Start()
        {
            m_Commander.onOpenCommander.AddListener(delegate { enabled = true; });
            m_Commander.onCloseCommander.AddListener(delegate { enabled = false; });


        }

        private void OnEnable()
        {
            m_Commander.onProcessCommand.AddListener(AddLastCommand);
        }

        private void OnDisable()
        {
            m_Commander.onProcessCommand.RemoveListener(AddLastCommand);
        }

        private void AddLastCommand()
        {
            var text = Consoler.Commander.Input;
            if (text.Length > 0)
            {
                history.Add(text);
            }
            m_ActualIndex = noValue;
        }

        private void Update()
        {
            if (Input.GetKeyDown(downKeyCode))
            {
                if (history.Count == 0) return;
                m_ActualIndex++;
                if (m_ActualIndex == history.Count) m_ActualIndex = 0;
                Consoler.Commander.Input = history[m_ActualIndex];
            }

            else if (Input.GetKeyDown(upKeyCode))
            {
                if (history.Count == 0) return;
                m_ActualIndex--;
                switch (m_ActualIndex)
                {
                    case noValue:
                        Consoler.Commander.Input = "";
                        return;
                    case -2:
                        m_ActualIndex = history.Count - 1;
                        break;
                }

                Consoler.Commander.Input = history[m_ActualIndex];
            }
        }

        private void OnDestroy()
        {
            string value = string.Empty;
            for (int i = 0; i < history.Count; i++)
            {
                value += history[i] + "\n";
            }

            PlayerPrefs.SetString(saveHistoryKey, value);
            PlayerPrefs.Save();
        }
    }
}