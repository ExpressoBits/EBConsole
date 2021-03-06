﻿using UnityEngine;

namespace ExpressoBits.Console.Commands
{
    [CreateAssetMenu(fileName = "Log Error Command", menuName = "Expresso Bits/Console/Log Error Command")]
    public class LogErrorCommand : ConsoleCommand
    {

        private void Awake()
        {
            commandWord = "error";
        }
        
        public override bool Process(string[] args)
        {
            string logText = string.Join(" ", args);

            if (logText.Length <= 0) return false;

            if (Consoler.Logs != null) Consoler.Logs.LogError(logText);

            return true;
        }
    }
}