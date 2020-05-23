﻿using System.Collections.Generic;
using UnityEngine;
using ExpressoBits.Console.Commands;
using UnityEngine.Events;

namespace ExpressoBits.Console
{
    [AddComponentMenu(menuName: "Console/Commander")]
    [RequireComponent(typeof(Consoler))]
    public class Commander : MonoBehaviour
    {

        [Header("General Settings")]
        public string prefix = string.Empty;

        [Header("List of valid Static commands")]
        public ConsoleCommand[] staticCommands;
        public List<ICommand> commands = new List<ICommand>();

        public ConsoleCommand commandWithoutPrefix;


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
                foreach (var item in staticCommands)
                {
                    commands.Add(item);
                }
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

        // Adiciona comando criado em tempo de execução, basta criar um com
        // <code>new Command("test",delegate{ Test(); })</code>
        public void AddCommand(ICommand command)
        {
            commands.Add(command);
        }

        public void AddCommand(string commandWord, UnityAction action)
        {
            commands.Add(new Command(commandWord,action));
        }

    }

}

