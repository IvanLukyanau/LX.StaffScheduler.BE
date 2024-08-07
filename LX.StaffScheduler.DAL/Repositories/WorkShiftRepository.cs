﻿using LX.StaffScheduler.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

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

        public async Task<IEnumerable<DateOnly>> GetMondaysWorkShiftsAsync(int cafeId)
        {
            var weekStartDates = new List<DateOnly>();

            var workShifts = await _context.WorkShifts
                .Where(ws =>
                    ws.CafeId == cafeId)
                .OrderBy(ws => ws.ShiftDate)
                .ToListAsync();

            var oldestMondayDate = workShifts
                .FirstOrDefault(ws => ws.ShiftDate.DayOfWeek == DayOfWeek.Monday)?.ShiftDate ?? default(DateOnly);

            if (oldestMondayDate == default)
            {
                return weekStartDates;
            }

            weekStartDates.Add(oldestMondayDate);
            var currentMonday = oldestMondayDate;

            while (currentMonday < workShifts.Last().ShiftDate)
            {
                currentMonday = currentMonday.AddDays(7);
                weekStartDates.Add(currentMonday);
            }

            return weekStartDates;
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

        public async Task<IEnumerable<WorkShift>> SaveWeekWorkShifts(IEnumerable<WorkShift> workShifts)
        {
            await _context.WorkShifts.AddRangeAsync(workShifts);
            await _context.SaveChangesAsync();

            var workShiftsList = workShifts.ToList();
            var mondayOfTheWeek = workShiftsList.Min(ws => ws.ShiftDate);

            var responseList = await GetWeekWorkShifts(workShiftsList.First().CafeId, mondayOfTheWeek);

            return responseList;

        }

        public async Task<IEnumerable<WorkShift>> UpdateWeekWorkShifts(IEnumerable<WorkShift> workShifts)
        {
            var workShiftsList = workShifts.ToList();
            var mondayOfTheWeek = workShiftsList.Min(ws => ws.ShiftDate);

            var responseList = await GetWeekWorkShifts(workShiftsList.First().CafeId, mondayOfTheWeek);


            _context.WorkShifts.RemoveRange(responseList);

            await _context.WorkShifts.AddRangeAsync(workShiftsList);
            await _context.SaveChangesAsync();

            var updatedWorkShifts = await GetWeekWorkShifts(workShiftsList.First().CafeId, mondayOfTheWeek);

            return updatedWorkShifts;
        }

    }
}
