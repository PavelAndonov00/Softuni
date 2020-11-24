using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace p01_Key_Revolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var bulletsPrice = int.Parse(Console.ReadLine());
            var sizeOfBarrel = int.Parse(Console.ReadLine());

            var bullets = Console.ReadLine()
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
            var bulletsStack = new Stack<int>(bullets);

            var locks = Console.ReadLine()
               .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
               .Select(int.Parse)
               .ToList();
            var locksQueue = new Queue<int>(locks);

            var intelligence = int.Parse(Console.ReadLine());

            var currentBullet = bulletsStack.Pop();
            var currentLock = locksQueue.Dequeue();
            var bulletsCounter = 0;
            while (bulletsStack.Count > 0 && locksQueue.Count > 0)
            {
                if(bulletsCounter == sizeOfBarrel)
                {
                    Console.WriteLine("Reloading!");
                    bulletsCounter = 0;
                }

                if(currentBullet <= currentLock)
                {
                    currentLock = locksQueue.Dequeue();

                    currentBullet = bulletsStack.Pop();

                    Console.WriteLine("Bang!");
                }
                else
                {
                    currentBullet = bulletsStack.Pop();
                    Console.WriteLine("Ping!");
                }

                bulletsCounter++;
            }

            if(locksQueue.Count > 0)
            {            
                Console.WriteLine($"Couldn't get through. Locks left: {locksQueue.Count+1}");
            }
            else
            {
                var bulletsLeft = (bullets.Count - bulletsStack.Count);
                var totalEarned = intelligence - (bulletsLeft * bulletsPrice);
                Console.WriteLine($"{bulletsStack.Count} bullets left. Earned ${totalEarned}");
            }
        }
    }
}
