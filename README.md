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

# Log example
<pre><code>TIMESTAMP;EVENT;EVENT_DATA
1509079606;VKPERSON_INIT;96994337
1509079607;COLLECT_STARTED;96994337
1509079608;FRIENDS_COUNT_CHANGED;495
1509079608;PUBLICS_COUNT_CHANGED;66
1509079608;STATUS_CHANGED;ONLINE
1509079848;STATUS_CHANGED;OFFLINE
1509080272;STATUS_CHANGED;ONLINE
1509080568;STATUS_CHANGED;OFFLINE
1509080961;FRIEND_ADD;56514331
1509080961;FRIENDS_COUNT_CHANGED;513
1509080980;STATUS_CHANGED;ONLINE
1509081167;PUBLIC_LEAVE;165143
1509081167;PUBLICS_COUNT_CHANGED;89
</code></pre>

# What for?
To analyze the social network and individual profiles.
