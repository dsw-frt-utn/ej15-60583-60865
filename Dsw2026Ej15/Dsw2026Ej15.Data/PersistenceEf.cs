using Dsw2026Ej15.Data.Migrations;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;
    public PersistenceEf(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Doctor>> GetAllDoctos()
    {
        
        return await _context.Doctors
            .Include(d => d.Speciality)
            .Where(d => d.IsActive).ToListAsync(); 
    }


    public async Task<Doctor?> GetDoctorById(Guid id)
    {
        return await _context.Doctors.
            Include(nameof(Doctor.Speciality)).
            SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
    }

    public async Task SaveDoctor(Doctor doctor)
    {
        _context.Add(doctor);
        await _context.SaveChangesAsync();
    }
    public async Task<Speciality?> GetSpecialityById(Guid id)
    {
        return await _context.Specialities.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task UpdateDoctor(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    }

    

