using System;
using System.Collections.Generic;

namespace FSMSystem.Core
{
    public class TaskRunner
    {
        public event Action<bool> OnTasksFinished = delegate {  };
        
        private List<ActionTask> tasks;
        private int currentIndex;

        public void StartTasks(List<ActionTask> tasksToRun)
        {
            tasks = tasksToRun;
            currentIndex = 0;

            if (tasks.Count == 0)
            {
                OnTasksFinished(true);
            }
            else
            {
                StartTask(0);
            }
        }
        
        public void UpdateTask()
        {
            if(tasks == null || currentIndex < 0 || currentIndex >= tasks.Count)
                return;
            
            tasks[currentIndex].Update();
        }

        public void ClearCallback()
        {
            OnTasksFinished = delegate {  };
        }

        private void StartTask(int index)
        {
            currentIndex = index;
            tasks[currentIndex].OnTaskFinished += FinishCurrentTask;
            tasks[currentIndex].Start();
        }

        private void FinishCurrentTask(bool wasSuccessful)
        {
            tasks[currentIndex].OnTaskFinished -= FinishCurrentTask;
            
            if (wasSuccessful)
            {
                GoToNextTask();
            }
            else
            {
                OnTasksFinished(false);
            }
        }
        
        private void GoToNextTask()
        {
            tasks[currentIndex].End();

            if (currentIndex + 1 >= tasks.Count)
            {
                OnTasksFinished(true);
                return;
            }

            StartTask(currentIndex + 1);
        }
    }
}