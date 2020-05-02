using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NybSys.AuditLog.DAL;
using NybSys.Common.ExceptionHandle;
using NybSys.Common.Extension;
using NybSys.Models.DTO;
using NybSys.Models.ViewModels;

namespace NybSys.AuditLog.BLL
{
    public class AuditLogBLL : IAuditLogBLL
    {
        private readonly AuditLogContext _context;

        public AuditLogBLL(AuditLogContext context)
        {
            _context = context;
        }

        public virtual async Task<(IEnumerable<VMAuditLog> Items, int TotalCount)> GetLogByFilter(VmAuditLogFilter filter)
        {

            var query = _context.LogMain.Where(a => (a.LogTime.Value.Date >= filter.FromDate.GetLocalZoneDate()
                                               && a.LogTime.Value.Date <= filter.ToDate.GetLocalZoneDate())
                                               && ( string.IsNullOrEmpty(filter.Username) ? true : a.UserId.EqualsWithLower(filter.Username))
                                               && ((filter.Action == 0) ? true : a.Action.ActionId == filter.Action));

            int TotalCount = await query.CountAsync();

            if(TotalCount > 0)
            {
                IEnumerable<VMAuditLog> Items = await query.OrderByDescending(a => a.LogTime).Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).Select(a => new VMAuditLog()
                {
                    Action = (Common.Enums.Action)a.Action.ActionId,
                    CalledFunction = a.CalledFunction,
                    LogDescription = a.LogDescription,
                    LogMessage = a.LogMessage,
                    LogTime = a.LogTime.Value,
                    UserId = a.UserId
                })
                .ToListAsync();

                return (Items, TotalCount);
            }
            else
            {
                throw new NotFoundException(Common.Constants.ErrorMessages.NO_LOG_FOUND);
            }

            
        }

        public virtual async Task SaveLog(
            string CalledFunction, 
            string LogDescription, 
            Common.Enums.Action action, 
            ApiCommonMessage data, 
            Common.Enums.LogType logType = Common.Enums.LogType.SystemLog, 
            Common.Enums.Module module = Common.Enums.Module.Web)
        {
            try
            {
                LogMain logMain = new LogMain()
                {
                    ActionId = (int)action,
                    CalledFunction = CalledFunction,
                    FormName = "",
                    LogTime  = DateTime.Now,
                    LogDescription = LogDescription,
                    LogTypeId = (int)logType,
                    ModuleId = (int)module,
                    LogMessage = Common.Utility.JSONConvert.ConvertString(new
                                                                        {
                                                                            data.UserName,
                                                                            data.SessionId,
                                                                            Content = Common.Utility.JSONConvert.Convert<dynamic>(data.Content)
                                                                        }),
                    Status = (int)Common.Enums.Enums.Status.Active,
                    UserId = data.UserName
                };

                await _context.AddAsync(logMain);
                await _context.SaveChangesAsync();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
