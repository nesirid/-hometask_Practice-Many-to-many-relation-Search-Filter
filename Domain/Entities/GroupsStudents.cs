﻿

namespace Domain.Entities
{
    public class GroupsStudents
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
