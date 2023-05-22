using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Camadas.Dados;
using WebAPI.Camadas.Entidades;

namespace WebAPI.Camadas.Negocios
{
    public class NTask
    {
        private readonly DTask _dataAccessLayer;

        public NTask(DTask dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public List<TaskModel> GetAllTasks()
        {
            return _dataAccessLayer.GetAllTasks();
        }

        public TaskModel GetTaskById(int id)
        {
            return _dataAccessLayer.GetTaskById(id);
        }

        public TaskModel CreateTask(TaskModel task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            return _dataAccessLayer.CreateTask(task);
        }

        

        public void UpdateTask(int id, TaskModel task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }

            _dataAccessLayer.UpdateTask(id, task);
        }

        public void DeleteTask(int id)
        {
            _dataAccessLayer.DeleteTask(id);
        }

    }
}