namespace PickupCounterIoT.Settings
{
    public record CounterSettings(string id, Locale locale, int MaxDoorOpenCount, double MinCellTempC, double MaxCellTempC);
}
