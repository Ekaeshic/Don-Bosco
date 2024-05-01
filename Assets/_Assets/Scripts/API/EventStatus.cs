namespace DonBosco.API
{
    /// <summary>
    /// Status of an event based on database API.
    /// Udah sesuai tinggal dikasih eventStatus.ToString() kalo di POST
    /// </summary>
    [System.Serializable]
    public enum EventStatus
    {
        belum,
        sedang,
        selesai
    }
}