using GameEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public static class Engine
    {

        public static Game StartGame() {
            return new Game();
        }
    }
}
