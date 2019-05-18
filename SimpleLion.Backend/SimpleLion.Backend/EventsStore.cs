using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace SimpleLion.EventsLib
{

    public class Event
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public string Rubric { get; set; }
        public string LocationName { get; set; }
        public double Distance { get; set; }
    }


    public class EventsStore
    {
        const string InsertEventQuery = @"INSERT INTO events(start_time, end_time, location, comment, title, rubric, location_name) 
                                          VALUES (@StartTime,
                                                  @EndTime,
                                                  ST_GeomFromEWKT('SRID=4326;POINT(' || @Latitude || ' '|| @Longitude || ')'),
                                                  @Comment,
                                                  @Title,
                                                  @Rubric,
                                                  @LocationName)
                                           RETURNING id";

        const string GetEventsQuery = @"SELECT 
                                            id AS Id,
                                            comment AS Comment,
                                            title AS Title,
                                            location_name AS LocationName,
                                            rubric AS Rubric,
                                            start_time AS StartTime,
                                            end_time AS EndTime,
                                            ST_X(location) AS Latitude, ST_Y(location) AS Longitude,
                                            ST_DistanceSphere(
                                             location,
                                             ST_GeomFromEWKT('SRID=4326;POINT(' || @lat || ' ' || @lng || ')')
                                           ) AS Distance
                                        FROM events
                                        WHERE ST_DistanceSphere(
                                             location,
                                             ST_GeomFromEWKT('SRID=4326;POINT(' || @lat || ' ' || @lng || ')')
                                           ) < @dist 
                                        AND start_time - (current_timestamp + interval '5 hours') < interval '30 minutes'
                                        AND current_timestamp + interval '5 hours' < end_time
                                        AND (rubric = @rubric OR @rubric='any')";

        private readonly string connectionString;

        public EventsStore(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task AddEvent(Event @event)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var newId = await connection.ExecuteScalarAsync<int>(InsertEventQuery, @event);
                @event.Id = newId;
            }
        }

        public async Task<Event[]> GetEventsNearby(double lat, double lng, double dist, string rubric)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<Event>(GetEventsQuery, new { lat, lng, dist, rubric });
                return result.ToArray();
            }
        }
    }
}
