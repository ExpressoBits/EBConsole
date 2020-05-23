using UnityEngine;

namespace ExpressoBits.Console.Stats
{
    public class SystemStat : StatBehaviour
    {
        
        public Sprite graphicIcon;

        private Info _graphicsDeviceName;
        private Info _operatingSystem;
        private Info _processorType;

        private void Awake()
        {
            _graphicsDeviceName = new Info(
                string.Format(ColorText("{0}",color),SystemInfo.graphicsDeviceName),
                new LogAttribute(graphicIcon,new Color(0,0,0,0)));

            _operatingSystem = new Info(
                string.Format(ColorText("{0}",color),SystemInfo.operatingSystem),
                new LogAttribute(graphicIcon,new Color(0,0,0,0)));

            _processorType = new Info(
                string.Format(ColorText("{0}",color),SystemInfo.processorType),
                new LogAttribute(graphicIcon,new Color(0,0,0,0)));


            infos.Add(_graphicsDeviceName);
            infos.Add(_operatingSystem);
            infos.Add(_processorType);

            Stater = GetComponentInParent<Stater>();
          
        }
        
        
    }
}

