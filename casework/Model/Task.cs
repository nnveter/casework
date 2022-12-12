﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CaseWork.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Assignment { get; set; }
    public int Urgency { get; set; } = 0; // procents
    public string UrhencyColor { get; set; } = "#FFFFFF";
    public DateTimeKind DeadLine { get; set; }
    public bool? IsComplete { get; set; } = false;
    public DateTimeKind AcceptedTime { get; set; }
    public DateTimeKind CompletedTime { get; set; }
    
    public User Employer { get; set; }
    public User Executor { get; set; }
    public Task? SubTask { get; set; }
    
    public long CreatedAt { get; set; } = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
}