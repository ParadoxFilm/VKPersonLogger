# VKPersonLogger
VK Person data/events collector.

# How to use
<ol>
<li>Compile (or just download .zip release) and run the program.</li>
<li>Get access token from browser (it autoruns after start the program and press any key) and paste token to the program.<br>
<i>Example result: 206fe555b22a03813d111c7578d6df6d3de01716ff694945dbd8a615e56d72af5b322b546dcf98d2bebb5</i></li>
<li>Paste VKontakte ID to the program and press [Enter].</li>
<li>Watch the result. The report(log-file) will be written in the root with the executable file.</li>
</ol>

# Available Events
<ol>
<li><b>STATUS_CHANGED</b> — The user has changed the status to online/offline (the status is logged).</li>
<li><b>PUBLIC_JOIN</b> — The user joined the public/group (the ID is logged).</li>
<li><b>PUBLIC_LEAVE</b> — The user has left the public/group (the ID is logged).</li>
<li><b>FRIEND_ADD</b> — The user has added a friend (the ID is logged).</li>
<li><b>FRIEND_REMOVE</b> — The user has deleted the friend (the ID is logged).</li>
<li><b>FRIENDS_COUNT_CHANGED</b> — Changed the number of friends (the count is logged).</li>
<li><b>PUBLICS_COUNT_CHANGED</b> — Changed the number of public/group (the count is logged).</li>
<li><b>COLLECT_STARTED</b> — Data collection started.</li>
<li><b>COLLECT_FINISH</b> — Data collection is finished.</li>
<li><b>VKPERSON_INIT</b> — The person is initialized to collect data.</li>
<li><b>COLLECT_ITERATION_DONE</b> — The next iteration of the data collection is completed (logged only to the console).</li>
</ol>

# Log example
| TIMESTAMP| EVENT | EVENT_DATA |
|----------|-------|------------|
|1509079606|COLLECT_STARTED|96994337|
|1509079606|VKPERSON_INIT|96994337|
|1509079607|COLLECT_STARTED|96994337|
|1509079608|FRIENDS_COUNT_CHANGED|495|
|1509079608|PUBLICS_COUNT_CHANGED|66|
|1509079848|STATUS_CHANGED|ONLINE|
|1509080272|STATUS_CHANGED|OFFLINE|
|1509080568|STATUS_CHANGED|ONLINE|
|1509080615|STATUS_CHANGED|OFFLINE|

# In the future it is planned
<ul>
<li>Separation online at mobile and desktop.</li>
<li>Tracking subscribers (requests for friends).</li>
<li>Simultaneous tracking of similar information from target friends.</li>
<li>Highlighting the log in the console</li>
<li>And so on</li>
</ul>

# What for?
To analyze the social network and individual profiles.
