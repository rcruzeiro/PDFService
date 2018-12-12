using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDFService.Entities;
using PDFService.Repository;

namespace PDFService.Services.Entity
{
    public sealed class TemplateEntityService
    {
        readonly ITemplateRepository _repository;

        public TemplateEntityService(ITemplateRepository repository)
        {
            _repository = repository;
        }

        ~TemplateEntityService()
        {
            _repository.Dispose();
        }

        public List<Template> GetTemplates(string clientID)
        {
            try
            {
                return _repository.Get(t => t.ClientID == clientID)
                                  .ToList();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public Template GetTemplate(string clientID, int id)
        {
            try
            {
                return _repository.Get(t => t.ClientID == clientID && t.ID == id)
                                  .SingleOrDefault();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task Save(Template template)
        {
            try
            {
                if (template.ID == default(int))
                    await _repository.AddAsync(template);
                else
                    _repository.Update(template);

                await _repository.SaveAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task Delete(Template template)
        {
            try
            {
                _repository.Remove(template);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public async Task Delete(string clientID, int id)
        {
            try
            {
                _repository.Remove(t => t.ClientID == clientID && t.ID == id);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
