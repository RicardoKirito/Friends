using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<List<ViewModel>> GetAll();
        Task<SaveViewModel> GetById(int id);
        Task<SaveViewModel> Add(SaveViewModel viewModel);
        Task Update(SaveViewModel viewModel, int id);
        Task DeleteById(int id);
    }
}
