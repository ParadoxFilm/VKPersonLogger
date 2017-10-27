using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VKPersonLogger
{
    /// <summary>
    /// VKontakte person.
    /// </summary>
    public class VKPerson
    {
        /// <summary>
        /// Data loaded fisrt time.
        /// </summary>
        public bool inited = false;

        /// <summary>
        /// VKontakte ID.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// VKontakte Token. Get it with Itilities.GetToken().
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Shows status of user.
        /// </summary>
        public bool IsOnline { get; private set; } = false;

        /// <summary>
        /// Contains IDs of publics of user.
        /// </summary>
        public List<int> Publics { get; private set; } = new List<int>();

        /// <summary>
        /// Contains IDs of friends of user.
        /// </summary>
        public List<int> Friends { get; private set; } = new List<int>();


        /// <summary>
        /// Friends count of user.
        /// </summary>
        public int FriendsCount
        {
            get
            {
                return Friends.Count;
            }
        }

        /// <summary>
        /// Publics count of user.
        /// </summary>
        public int PublicsCount
        {
            get
            {
                return Friends.Count;
            }
        }


        /// <summary>
        /// Common constructor.
        /// </summary>
        /// <param name="id">VK ID.</param>
        public VKPerson(int id, string token)
        {
            this.ID = id;

            this.Token = token;

            // Create instance of logger.
            Logger = new VKLogger(this);

            // Add message to log about create instance of VK Person.
            Logger.Add(Config.Events.VKPERSON_INIT, this.ID.ToString());

            // Create instance of data collector.
            Collector = new VKCollector(this);

        }


        /// <summary>
        /// Logger instance.
        /// </summary>
        VKLogger Logger;

        /// <summary>
        /// Logger for VK Person.
        /// </summary>
        class VKLogger
        {
            /// <summary>
            /// VK Person reference.
            /// </summary>
            VKPerson _base;

            /// <summary>
            /// Common constructor.
            /// </summary>
            /// <param name="p">VK Person reference.</param>
            public VKLogger(VKPerson p)
            {
                _base = p;
            }

            /// <summary>
            /// Add message to log.
            /// </summary>
            /// <param name="_event">Event name.</param>
            /// <param name="_data">Event data.</param>
            /// <param name="_toFile">Also save event to log-file.</param>
            public void Add(Config.Events _event, string _data, bool _toFile = Config.defualt_save_log)
            {
                // Current time.
                string time = Config.unixtime_log ? Utilities.GetUnixTime().ToString() : Utilities.GetDateTime();

                Console.WriteLine($"{time};{_event.ToString()};{_data}");

                // Save log to file...
                if (_toFile)
                {
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(LogFilename, true, Encoding.UTF8);
                    writer.WriteLine($"{time};{_event.ToString()};{_data}");
                    writer.Close();
                }
            }

            /// <summary>
            /// Log-file name.
            /// </summary>
            public string LogFilename
            {
                get
                {
                    return _base.ID + Config.log_postfix;
                }
            }
        }

        /// <summary>
        /// Config for Application
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// Interval per request.
            /// </summary>
            public const int request_interval = 60000;

            /// <summary>
            /// Base url for request to VK API.
            /// </summary>
            public const string base_api_url = "https://api.vk.com/method/";

            /// <summary>
            /// Postfix for log-file name.
            /// </summary>
            public const string log_postfix = "_report.csv";

            /// <summary>
            /// Possible events for VK person.
            /// </summary>
            public enum Events
            {
                STATUS_CHANGED, PUBLIC_JOIN, PUBLIC_LEAVE,
                FRIEND_ADD, FRIEND_REMOVE, FRIENDS_COUNT_CHANGED,
                PUBLICS_COUNT_CHANGED, COLLECT_STARTED, COLLECT_FINISH,
                VKPERSON_INIT, COLLECT_ITERATION_DONE
            }

            /// <summary>
            /// Save log to file by default.
            /// </summary>
            public const bool defualt_save_log = true;

            /// <summary>
            /// Datetime as unixtime.
            /// </summary>
            public const bool unixtime_log = true;

            /// <summary>
            /// Default VK ID (for debug).
            /// </summary>
            public const int default_id = 96994337;
        }

        /// <summary>
        /// Intance of collector.
        /// </summary>
        public VKCollector Collector;

        /// <summary>
        /// Class for collecting information about user.
        /// </summary>
        public class VKCollector
        {
            /// <summary>
            /// Collect passes.
            /// </summary>
            int iterations = 0;

            /// <summary>
            /// Timer for requests.
            /// </summary>
            Timer timer;

            /// <summary>
            /// VK Person reference.
            /// </summary>
            VKPerson _base;

            /// <summary>
            /// Common constructor.
            /// </summary>
            /// <param name="p">VK Person reference.</param>
            public VKCollector(VKPerson p)
            {
                _base = p;
                timer = new Timer(VKPerson.Config.request_interval);
                timer.Elapsed += (sender, e) => CollectAndApply();
            }

            /// <summary>
            /// Collect data and apply it.
            /// </summary>
            void CollectAndApply()
            {
                UpdateFriends();
                UpdatePublics();
                UpdateStatus();

                _base.inited = true;

                _base.Logger.Add(Config.Events.COLLECT_ITERATION_DONE, string.Format($"{_base.ID}[{++iterations}]"), false);

            }

            /// <summary>
            /// Collecting start.
            /// </summary>
            public void Start()
            {
                _base.Logger.Add(Config.Events.COLLECT_STARTED, _base.ID.ToString());

                CollectAndApply();
                timer.Start();
            }

            /// <summary>
            /// Collecting stop.
            /// </summary>
            public void Stop()
            {
                _base.Logger.Add(Config.Events.COLLECT_FINISH, _base.ID.ToString());

                timer.Stop();
            }

            /// <summary>
            /// Update friends of base VK Person.
            /// </summary>
            void UpdateFriends()
            {
                string json = Utilities.TypicalGetRequest(Config.base_api_url + "friends.get?user_id=" + _base.ID + "&version=5.52");
                dynamic data = JsonConvert.DeserializeObject(json);
                List<int> friendsList = data.response.ToObject<List<int>>();

                int friends_now = friendsList.Count(); // For friends count comparing...

                // If data for compare is exist...
                if (_base.inited)
                {

                    // Add friends checking...
                    foreach (int u in friendsList)
                    {
                        if (!_base.Friends.Contains(u))
                        {
                            _base.Logger.Add(Config.Events.FRIEND_ADD, u.ToString());
                        }
                    }

                    // Remove friends checking...
                    foreach (int u in _base.Friends)
                    {
                        if (!friendsList.Contains(u))
                        {
                            _base.Logger.Add(Config.Events.FRIEND_REMOVE, u.ToString());
                        }
                    }
                }

                // Compare friends count...
                if (_base.Friends.Count != friends_now)
                {
                    _base.Logger.Add(Config.Events.FRIENDS_COUNT_CHANGED, friends_now.ToString());
                }

                // Update current friend list.
                _base.Friends = friendsList;

            }

            /// <summary>
            /// Update publics of base VK Person.
            /// </summary>
            void UpdatePublics()
            {
                string json = Utilities.TypicalGetRequest(Config.base_api_url + "groups.get?user_id=" + _base.ID + "&access_token=" + _base.Token + "&version=3.0");
                dynamic data = JsonConvert.DeserializeObject(json);
                List<int> publicList = data.response.ToObject<List<int>>();

                int public_now = publicList.Count(); // For public count comparing...

                // If data for compare is exist...
                if (_base.inited)
                {

                    // Add publics checking...
                    foreach (int u in publicList)
                    {
                        if (!_base.Publics.Contains(u))
                        {
                            _base.Logger.Add(Config.Events.PUBLIC_JOIN, u.ToString());
                        }
                    }

                    // Remove publics checking...
                    foreach (int u in _base.Publics)
                    {
                        if (!publicList.Contains(u))
                        {
                            _base.Logger.Add(Config.Events.PUBLIC_LEAVE, u.ToString());
                        }
                    }
                }

                // Compare publics count...
                if (_base.Publics.Count != public_now)
                {
                    _base.Logger.Add(Config.Events.PUBLICS_COUNT_CHANGED, public_now.ToString());
                }

                // Update current publics list.
                _base.Publics = publicList;
            }

            /// <summary>
            /// Update status of base VK Person.
            /// </summary>
            void UpdateStatus()
            {

                string json = Utilities.TypicalGetRequest(Config.base_api_url + "users.get?user_ids=" + _base.ID + "&fields=online&version=5.52");
                dynamic data = JsonConvert.DeserializeObject(json);
                bool isOnline = data.response[0].online.ToObject<bool>();

                // Online checking and comparing...
                if (_base.IsOnline != isOnline)
                {
                    _base.IsOnline = isOnline;
                    _base.Logger.Add(Config.Events.STATUS_CHANGED, isOnline ? "ONLINE" : "OFFLINE");
                }
            }

        }

    }
}
