using Asteroids.Infrastructure;
using NUnit.Framework;

namespace Tests.GameMechanics.EnemiesSpawn
{
    public class TaskWaiterTest
    {
        private TaskWaiter _taskWaiter;

        [SetUp]
        public void Setup()
        {
            _taskWaiter = new TaskWaiter();
        }

        [Test]
        public void Wait_more_finishes_task()
        {
            var task = _taskWaiter.Wait(2);
            
            _taskWaiter.Tick(3f);
            
            Assert.IsTrue(task.IsCompleted);
        }
        
        [Test]
        public void Wait_less_doesnt_finish_task()
        {
            var task = _taskWaiter.Wait(2);
            
            _taskWaiter.Tick(1f);
            
            Assert.IsFalse(task.IsCompleted);
        }
    }
}
