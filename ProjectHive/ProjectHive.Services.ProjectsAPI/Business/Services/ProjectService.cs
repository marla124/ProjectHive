using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;

namespace ProjectHive.Services.ProjectsAPI.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProjectDto?> GetProjectById(Guid Id, CancellationToken cancellationToken)
        {
            var project= _mapper.Map<ProjectDto>(await _unitOfWork.ProjectRepository.GetById(Id, cancellationToken));
            return project;
        }

        public async Task DeleteProjectById(Guid Id, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProjectRepository.DeleteById(Id, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task CreateProject(ProjectDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(dto);
            await _unitOfWork.ProjectRepository.CreateOne(project, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task UpdateProject(ProjectDto dto, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(dto);
            await _unitOfWork.ProjectRepository.Update(project, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task CreateManyProject(IEnumerable<ProjectDto> dtos, CancellationToken cancellationToken)
        {
            var projects=new List<Project>();
            foreach(var item in dtos)
            {
                projects.Add(_mapper.Map<Project>(item));
            }
            await _unitOfWork.ProjectRepository.CreateMany(projects, cancellationToken);
            await _unitOfWork.Commit();
        }

        public async Task DeleteManyProject(IEnumerable<ProjectDto> dtos, CancellationToken cancellationToken)
        {
            var projects = new List<Project>();
            foreach (var item in dtos)
            {
                projects.Add(_mapper.Map<Project>(item));
            }
            await _unitOfWork.ProjectRepository.DeleteMany(projects,cancellationToken);
            await _unitOfWork.Commit();
        }
    }
}
