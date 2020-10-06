using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBird.Models
{
    public class Bird
    {
        public int DistanceFromGround { get; private set; } = 500;
        public int JumpStrength { get; private set; } = 50;
        public bool OnGround { get; set; }

        public void Fall(int gravity)
        {
            DistanceFromGround -= gravity;
            if (DistanceFromGround <= 150)
                OnGround = true;
        }
        public void Jump()
        {

            if (DistanceFromGround <= 650)
            {
                new GameManager().DetectCollisions();
                DistanceFromGround += JumpStrength;
            }

        }
    }
}
