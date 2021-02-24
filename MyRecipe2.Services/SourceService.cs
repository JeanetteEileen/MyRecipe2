using MyRecipe2.Data;
using MyRecipe2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipe2.Services
{
    public class SourceService
    {
            private readonly Guid _userId;
            public SourceService(Guid userId)
            {
                _userId = userId;
            }
            public bool CreateSource(SourceCreate model)
            {
                var entity =
                    new Source()
                    {
                        OwnerId = _userId,
                        SName = model.SName,
                        SOrigin = model.SOrigin
                    };
                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Sources.Add(entity);
         
           return ctx.SaveChanges() == 1;
                }
            }
            public CSourceList GetSourceById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Sources
                        .Single((e => e.SourceId == id && e.OwnerId == _userId));
                    return
                        new CSourceList
                        {
                            SourceId = entity.SourceId,
                            SName = entity.SName,
                            SOrigin = entity.SOrigin
                        };
                }
            }
            public IEnumerable<CSourceList> GetAllSources()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .Sources
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e => new CSourceList
                            {
                                SourceId = e.SourceId,
                                SName = e.SName,
                                SOrigin = e.SOrigin
                            }
                            );
                    return query.ToArray();
                }
            }
    }
}
