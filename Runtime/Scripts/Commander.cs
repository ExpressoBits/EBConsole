﻿using System;
using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.Console.Commands;
using UnityEngine.Events;
using ExpressoBits.Console.Utils;

namespace ExpressoBits.Console
{
    public class Commander : Singleton<Commander>
    {

        [Header("General Settings")]
        [SerializeField]
        private string prefix = string.Empty;

        [Header("List of valid commands")]
        public List<ConsoleCommand> commands = new List<ConsoleCommand>();

        [SerializeField] private ConsoleCommand commandWithoutPrefix;


        [Header("Events")]
        public UnityEvent onCloseCommander;
        public UnityEvent onOpenCommander;
        public UnityEvent onProcessCommand;
        public UnityEvent onFinishProcessCommand;
        
        #region private values

        private bool m_ActiveInput;
        private DeveloperConsole m_DeveloperConsole;
        private DeveloperConsole DeveloperConsole
        {
            get
            {
                if (m_DeveloperConsole != null) { return m_DeveloperConsole; }
                return m_DeveloperConsole = new DeveloperConsole(prefix, commands, commandWithoutPrefix);
            }
        }

        #endregion

        private void Start()
        {
            CloseCommander();
        }


        #region Open/Close
        public void CloseCommander()
        {
            m_ActiveInput = false;
            onCloseCommander.Invoke();
        }

        public void OpenCommander()
        {
            m_ActiveInput = true;
            onOpenCommander.Invoke();
        }

        #endregion

        public void ProcessCommand(string inputValue)
        {
            
            DeveloperConsole.ProcessCommand(inputValue);
            onProcessCommand.Invoke();
            onFinishProcessCommand.Invoke();
            
        }


        public void Toggle()
        {
            if (m_ActiveInput)
            {
                CloseCommander();
            }
            else
            {
                OpenCommander();
            }
        }

        // TODO add in runtime commands
        public void AddCommand(ConsoleCommand command)
        {
            commands.Add(command);
        }

    }

}

