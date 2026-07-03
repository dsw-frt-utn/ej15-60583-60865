namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public required string Name { get; set; }
        public required string LicenseNumber { get; set; }
        public bool IsActive { get; set; }

        public Guid SpecialityId { get; set; }
        public required Speciality Speciality { get; set; }
    }
}