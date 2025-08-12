﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace stepic.Models;

[PrimaryKey(nameof(UnitId), nameof(LessonId))]
[Table("unit_lessons")]
public class UnitLesson
{
    [Column("unit_id")]
    public int UnitId { get; set; }
    [Column("lesson_id")]
    public int LessonId { get; set; }

    public Unit Unit { get; set; }
    public Lesson Lesson { get; set; }
}