using GameEngine.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Models
{
    public class Game
    {
        public GameStatus Status
        { get; set; }
        public bool IsOn
        {
            get
            {
                return Status == GameStatus.On;
            }
        }
        public bool IsOff
        {
            get
            {
                return Status == GameStatus.Off;
            }
        }
        public Game()
        {
            Status = GameStatus.On;
        }

        public void TurnOff(){
            Status = GameStatus.Off;
        }
        
    }
}
