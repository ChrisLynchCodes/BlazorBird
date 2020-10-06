using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBird.Models
{
    public class GameManager
    {


        public event EventHandler MainLoopCompletion;
        public Bird Bird { get; private set; }
        public List<Pipe> Pipes { get; private set; }
        public bool IsRunning { get; private set; } = false;
        private readonly int _gravity = 5;
        public GameManager()
        {
            Bird = new Bird();
            Pipes = new List<Pipe>();
        }


        ///////////MAIN LOOP/////////////
        public async void MainLoop()
        {
            IsRunning = true;

            while (IsRunning)

            {
                MoveObjects();
                DetectCollisions();
                ManagePipes();


                await Task.Delay(20);

                MainLoopCompletion?.Invoke(this, EventArgs.Empty);

            }


        }


        public void StartGame()
        {
            if (!IsRunning)
            {
                Bird = new Bird();
                Pipes = new List<Pipe>();
                MainLoop();
            }
        }
        public void Jump()
        {
            if (IsRunning)

                Bird.Jump();

        }
        public void DetectCollisions()
        {

            if (Bird.OnGround)
            {
                GameOver();
            }

            //Check for pipe in collision zone
            var pipeInCollisonZone = Pipes.FirstOrDefault(p => p.IsCentered());

            //check for collision with bird 
            if (pipeInCollisonZone != null)
            {
                //150 is the ground height. 60 is the bird height in css :(
                bool hasCollidedWithBottomPipe = Bird.DistanceFromGround < pipeInCollisonZone.TopOfLowerPipe;
                bool hasCollidedWithTopPipe = Bird.DistanceFromGround + 60 > pipeInCollisonZone.BottomOfTopPipe;

                if (hasCollidedWithBottomPipe || hasCollidedWithTopPipe)

                    GameOver();

            }
        }
        public void GameOver()
        {
            IsRunning = false;
        }
        public void MoveObjects()
        {
            Bird.Fall(_gravity);
            foreach (var pipe in Pipes)
            {
                pipe.Move();
            }
        }
        public void ManagePipes()
        {
            if (!Pipes.Any() || Pipes.Last().DistanceFromLeft <= 250)
            {
                Pipes.Add(new Pipe());
            }
            if (Pipes.First().OffScreen)
                Pipes.Remove(Pipes.First());
        }

    }
}
