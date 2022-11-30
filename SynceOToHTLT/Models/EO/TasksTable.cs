using System;
using System.Collections.Generic;

namespace WinFormsApp1.eOffice
{
    public partial class TasksTable
    {
        public int IdTask { get; set; }
        public string? SEmailEmployee { get; set; }
        public string? SEmailEmployer { get; set; }
        public string? SSender { get; set; }
        public string? SReceiver { get; set; }
        public string? SSubject { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public int? ReminderTime { get; set; }
        public int? ReminderType { get; set; }
        public int? NotifyTime { get; set; }
        public string? SDetail { get; set; }
        public int? ReminderBefore { get; set; }
        public short? TaskType { get; set; }
        public short? BHasNotified { get; set; }
        public short? IPercentage { get; set; }
        public string? SMobileMsg { get; set; }
        public string? SMobileNum { get; set; }
        public short? BViaMobile { get; set; }
        public string? SNotifySound { get; set; }
        public short? BEnableSound { get; set; }
        public short? IUpdate { get; set; }
        public short? IStatus { get; set; }
        public short? BNotifyReceived { get; set; }
        public string? SEmployer { get; set; }
        public string? SEmployee { get; set; }
        public short? BEmployerDelete { get; set; }
        public short? BEmployeeDelete { get; set; }
        public short? BNotify { get; set; }
        public short? BNotifyUpdating { get; set; }
        public short? BNotifyDeadline { get; set; }
        public int? TimeToServer { get; set; }
        public short? IReminderMobile { get; set; }
        public short? IRepeatChoiceId { get; set; }
    }
}
