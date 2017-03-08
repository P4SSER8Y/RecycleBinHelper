
Recycle Bin Helper
================================

Select the items older than X days in the recycle bin and remove them.

Usage
----------

~~~
RecycleBinHelper <days> [-s | --silent]
~~~

- `<days>`  REQUIRED. The items older than `<days>` days will be selected and removed.
- [-s | --silent]  OPTIONAL. No confirm dialogs. Delete files directly.

__Examples__

+ `RecycleBinHelper 0` remove all the items
+ `RecycleBinHelper 7 -s`  remove the items older than one week
