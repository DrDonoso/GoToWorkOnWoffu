using System;

public class DayInfo
{
    public string id { get; set; }
    public string RequestId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string UserId { get; set; }
    public string RequestStatusId { get; set; }
    public string QuickDescription { get; set; }
    public string Description { get; set; }
    public object Comments { get; set; }
    public string AgreementEventName { get; set; }
    public object Docs { get; set; }
    public bool Enjoyed { get; set; }
    public bool NotEnjoyed { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string AgreementEventId { get; set; }
    public bool CancellationRequested { get; set; }
    public string NumberDaysRequested { get; set; }
    public string NumberHoursRequested { get; set; }
    public string RequestStatus { get; set; }
    public string ClassName { get; set; }
    public string FileName { get; set; }
    public bool SlotRequired { get; set; }
    public bool DocumentRequired { get; set; }
    public bool Modified { get; set; }
}
