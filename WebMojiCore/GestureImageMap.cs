using System;
using System.Collections.Generic;
using System.Text;

namespace WebMojiCore
{
    public class GestureImageMap
    {
        private Dictionary<GestureType, string> _map = new();

        public void Set(GestureType gesture, string imagePath)
        {
            _map[gesture] = imagePath;
        }

        public string? Get(GestureType gesture)
        {
            _map.TryGetValue(gesture, out var path);
            return path;
        }
    }
}
