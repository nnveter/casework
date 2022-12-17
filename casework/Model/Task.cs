using casework;
using casework.SplashScreen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CaseWork.Models;

public class Task
{
    public int id { get; set; }
    public string title { get; set; }
    public string assignment { get; set; }
    public int urgency { get; set; } = 0; // procents
    public string UrgencyColor { get; set; } = Constants.White;
    public long deadLine { get; set; }
    public DateTime DeadLineGet { get { return SplashScreenPage.UnixTimeStampToDateTime(deadLine); } }
    public String DeadLineGetString { get { return SplashScreenPage.UnixTimeStampToDateTime(deadLine).ToString("dd.MM.yyyy HH:mm"); } }
    public bool? isComplete { get; set; } = false;
    public long acceptedTime { get; set; }
    public DateTime AcceptedTimeGet { get { return SplashScreenPage.UnixTimeStampToDateTime(acceptedTime); } }
    public String AcceptedTimeGetString { get { return SplashScreenPage.UnixTimeStampToDateTime(acceptedTime).ToString("dd.MM.yyyy HH:mm"); } }
    public long completedTime { get; set; }
    public DateTime CompletedTimeGet { get { return SplashScreenPage.UnixTimeStampToDateTime(completedTime); } }
    public String CompletedTimeGetString { get { return SplashScreenPage.UnixTimeStampToDateTime(completedTime).ToString("dd.MM.yyyy HH:mm"); } }

    public User employer { get; set; }
    public User executor { get; set; }
    public Task? subTask { get; set; }
    
    public long createdAt { get; set; } = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

    public string PrevoisTextDeadLine { get; set; } = "до";
    public string ExecutorString { get; set; }
}