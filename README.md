
Recycle Bin Helper
================================

Select the files moved to recycle bin X days ago and remove them.


Usage
----------

~~~
RecycleBinHelper <days> [-s | --silent]
~~~

- `<days>`  REQUIRED. the files moved to recycle bin on the <days> before today will be removed
- [-s | --silent]  OPTIONAL. No confirm dialogs. Delete files directly.

__Examples__

+ `RecycleBinHelper 0` remove all the files
+ `RecycleBinHelper 7 -s`  remove the files one week ago
