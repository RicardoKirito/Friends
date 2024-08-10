using AutoMapper;
using Friends.Core.Application.Interface.Repository;
using Friends.Core.Application.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Service
{
    public class GenericService<SaveViewModel, ViewModel, Entity>: IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel: class
        where ViewModel : class
        where Entity : class
    {
        public readonly IGenericRepository<Entity> _repository;
        public readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<ViewModel>>(entities);
        }
        public virtual async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<SaveViewModel>(entity);
        }
        public virtual async Task<SaveViewModel> Add(SaveViewModel viewModel)
        {
            Entity entry = _mapper.Map<Entity>(viewModel);
            Entity entity = await _repository.AddAsync(entry);
            return _mapper.Map<SaveViewModel>(entity);
        }
        public async Task Update(SaveViewModel viewModel, int id)
        {
            Entity entity = _mapper.Map<Entity>(viewModel);
            await _repository.UpdateAsync(entity, id);
        }
        public virtual async Task DeleteById(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
