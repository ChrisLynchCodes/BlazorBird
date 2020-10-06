using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBird.Models
{
    public class Pipe
    {

        public int DistanceFromLeft { get; private set; } = 400;
        public int DistanceFromBottom { get; set; } = new Random().Next(0, 120);
        public bool OffScreen { get; set; }
        public int Speed { get; set; } = 2;
        //Distance between top of the bottom pipe and the bottome of the top pipe 
        public int Gap { get; set; } = 150;

        //300 is height of pipe
        public int TopOfLowerPipe => DistanceFromBottom + 300;
        public int BottomOfTopPipe => TopOfLowerPipe + Gap;

        public void Move()
        {
            DistanceFromLeft -= Speed;
            //-60 is the width of the pipe
            if (DistanceFromLeft == -60)
            {
                OffScreen = true;
                
            }
        }
       

        public bool IsCentered()
        {
            //Game container & bird width / 2 
            bool hasEnteredCenter = DistanceFromLeft <= (500 / 2) + (60 / 2);
            bool hasExitedCenter = DistanceFromLeft <= (500 / 2) - (60 / 2) - 60; //pipe width

            return hasEnteredCenter && !hasExitedCenter;
        }




    }
}
