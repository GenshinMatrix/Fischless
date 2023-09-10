import os

for rt, dirs, files in os.walk('.'):
    for f in files:
        fname, ext = os.path.splitext(f)
        if ext == '.png':
            print('public const string ' + fname + ' = "ms-appx:///Assets/Images/LocalAvatars/' + fname + ext + '";')
