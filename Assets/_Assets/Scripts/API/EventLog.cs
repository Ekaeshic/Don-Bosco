namespace DonBosco.API
{
    /// <summary>
    /// Event log based on database API.
    /// Should be used to GET and POST event log data
    /// </summary>
    [System.Serializable]
    public class EventLog
    {
        public int id_game;
        public int id_log;
        public int no_event;
        public EventStatus status;


        /// <summary>
        /// Constructor for GET request
        /// </summary>
        /// <param name="no_event"></param>
        /// <param name="status"></param>
        public EventLog(int id_game, int no_event, EventStatus status)
        {
            this.id_game = id_game;
            this.no_event = no_event;
            this.status = status;
        }
    }
}