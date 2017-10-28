# VKPersonLogger

# The purpose
This tool is used for VK social network analysis.

# How to use
<ol>
<li>Compile (or just download .zip release) and run the program.</li>
<li>Get access token from browser page that opens once you start the program and paste that token in the program.<br>
<i>Example result: 206fe555b22a03813d111c7578d6df6d3de01716ff694945dbd8a615e56d72af5b322b546dcf98d2bebb5</i></li>
<li>Paste VKontakte ID in the program and press [Enter].</li>
<li>Watch the result. A report (log-file) will be created in the executable file directory.</li>
</ol>

# Available Events
<ol>
<li><b>STATUS_CHANGED</b> — The user has changed the status to online/offline (the status is logged).</li>
<li><b>PUBLIC_JOIN</b> — The user has joined the public page/group (the ID is logged).</li>
<li><b>PUBLIC_LEAVE</b> — The user has left the public page/group (the ID is logged).</li>
<li><b>FRIEND_ADD</b> — The user has added a friend (the ID is logged).</li>
<li><b>FRIEND_REMOVE</b> — The user has deleted the friend (the ID is logged).</li>
<li><b>FRIENDS_COUNT_CHANGED</b> — The user has changed the number of friends (the count is logged).</li>
<li><b>PUBLICS_COUNT_CHANGED</b> — The user has changed the number of public pages/groups (the count is logged).</li>
<li><b>COLLECT_STARTED</b> — Data collection has started.</li>
<li><b>COLLECT_FINISH</b> — Data collection has finished.</li>
<li><b>VKPERSON_INIT</b> — The person has been initialized.</li>
<li><b>COLLECT_ITERATION_DONE</b> — The next iteration of the data collection has completed (logged only to the console).</li>
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

# Future plans:
<ul>
<li>Separate online statuses (mobile/desktop)</li>
<li>Subscribers tracking (friend requests)</li>
<li>Simultaneous tracking of similar information from target friends</li>
<li>Console logs highlighting</li>
<li>And so on</li>
</ul>


