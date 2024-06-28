﻿using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.DAL.Repositories
{
    public class WorkShiftRepository : IWorkShiftRepository
    {
        private readonly Context _context;

        public WorkShiftRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<WorkShift>> GetAllAsync()
        {
            return await _context.WorkShifts.ToListAsync();
        }

        public async Task<WorkShift?> GetByIdAsync(int id)
        {
            return await _context.WorkShifts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(WorkShift workShift)
        {
            _context.WorkShifts.Add(workShift);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkShift workShift)
        {
            _context.WorkShifts.Update(workShift);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var workShift = await _context.WorkShifts.FirstOrDefaultAsync(x => x.Id == id);
            _context.WorkShifts.Remove(workShift);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WorkShift>> GetWeekWorkShifts(int cafeId, DateOnly weekStartDate)
        {
            if (weekStartDate.DayOfWeek != DayOfWeek.Monday)
            {
                weekStartDate = weekStartDate.AddDays(-(int)weekStartDate.DayOfWeek + (int)DayOfWeek.Monday);
            }

            DateOnly weekEndDate = weekStartDate.AddDays(6);

            var workShifts = await _context.WorkShifts
                .Where(ws => ws.CafeId == cafeId && ws.ShiftDate >= weekStartDate && ws.ShiftDate <= weekEndDate)
                .ToListAsync();

            return workShifts;
        }

        public async Task<IEnumerable<WorkShift>> GetDayWorkShifts(int cafeId, DateOnly day)
        {
            var workShifts = await _context.WorkShifts
                .Where(ws => ws.CafeId == cafeId && ws.ShiftDate == day)
                .ToListAsync();

            return workShifts;
        }


    }
}
